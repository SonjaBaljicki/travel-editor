using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using TravelEditor.Database;
using TravelEditor.Export_Import.Iterfaces;
using TravelEditor.Models.dtos;
using TravelEditor.Models;

namespace TravelEditor.Export_Import.Service
{
    public class ImportService : IImportService
    {
        private readonly DatabaseContext _context;

        public ImportService(DatabaseContext context)
        {
            _context = context;
        }

        //getting entities after reading an excel file
        //used for attractions and reviews because they are added to the databse with other entities
        public List<T> GetEntities<T>(DataTable? table) where T : class, new()
        {
            var entities = new List<T>();

            if (table == null)
                return entities;

            var entityType = typeof(T);
            var properties = entityType.GetProperties()
                                       .ToDictionary(p => p.Name.ToLowerInvariant(), p => p);

            foreach (DataRow row in table.Rows)
            {
                var entity = CreateEntity(row, entityType, properties, new Dictionary<string, List<object>>());
                if (entity != null)
                {
                    entities.Add((T)entity);
                }
            }

            return entities;
        }


        //importing entites from the data table to the database
        //The optional parameter relatedEntities dictionary is used for handling relationships if needed during entity creation
        public void ImportEntities<T>(DataTable table, Dictionary<string, List<object>> relatedEntities = null) where T : class
        {
            if (table == null) return;

            var entityType = typeof(T);

            var properties = entityType.GetProperties()
                                       .ToDictionary(p => p.Name.ToLowerInvariant(), p => p);

            foreach (DataRow row in table.Rows)
            {
                var entity = CreateEntity(row, entityType, properties, relatedEntities) as T;
                if (entity != null)
                {
                    var existing = GetExistingEntity(entity, entityType) as T;
                    if (existing != null)
                    {
                        UpdateEntity(existing, entity);
                    }
                    else
                    {
                        _context.Set(entityType).Add(entity);
                    }
                }
            }

            _context.SaveChanges();
        }

        //Method that updates an existing entity, used for list, object or basic properies
        public void UpdateEntity<T>(T existingEntity, T newEntity) where T : class
        {
            if (existingEntity == null || newEntity == null) return;

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (typeof(IList).IsAssignableFrom(property.PropertyType))
                {
                    UpdateRelatedEntities(existingEntity, newEntity, property);
                }
                else
                {
                    UpdateStandardProperty(existingEntity, newEntity, property);
                }
            }
        }

        //For lists it uses reflection to get the specific ID property dynamically
        //It checks if the related entity already exists in the database if it does it updates it in the list
        //If it doesnt it adds it to the list
        public void UpdateRelatedEntities<T>(T existingEntity, T newEntity, PropertyInfo property) where T : class
        {
            var newRelatedEntitiesValue = property.GetValue(newEntity) as IList;
            var existingRelatedEntitiesValue = property.GetValue(existingEntity) as IList;

            if (newRelatedEntitiesValue != null)
            {
                if (newRelatedEntitiesValue.Count == 0)
                {
                    EmptyEntityList(existingRelatedEntitiesValue);
                }
                else
                {
                    RemoveNotPresentEntities(existingRelatedEntitiesValue, newRelatedEntitiesValue);
                    foreach (var newRelatedEntity in newRelatedEntitiesValue)
                    {
                        var idProperty = GetIdProperty(newRelatedEntity.GetType());
                        var newId = (int)(idProperty?.GetValue(newRelatedEntity));

                        var existingRelatedEntity = _context.Set(newRelatedEntity.GetType()).Find(newId);

                        if (existingRelatedEntity != null && existingRelatedEntitiesValue.Contains(existingRelatedEntity))
                        {
                            UpdateRelatedEntity(existingRelatedEntity, newRelatedEntity);
                        }
                        else
                        {
                            existingRelatedEntitiesValue.Add(newRelatedEntity);
                        }
                    }
                }
            }
        }

        //if we are updating an entity that already exists in the database and not all old ones are present
        public void RemoveNotPresentEntities(IList? existingRelatedEntitiesValue, IList newRelatedEntitiesValue)
        {
            var newRelatedEntityIds = new HashSet<int>(newRelatedEntitiesValue.Cast<object>().Select(e => (int)GetIdProperty(e.GetType()).GetValue(e)));
            var entitiesToRemove = new List<object>();
            for (int i = 0; i < existingRelatedEntitiesValue.Count; i++)
            {
                var existingEntity = existingRelatedEntitiesValue[i];
                var existingId = (int)GetIdProperty(existingEntity.GetType()).GetValue(existingEntity);

                if (!newRelatedEntityIds.Contains(existingId))
                {
                    entitiesToRemove.Add(existingEntity);
                    existingRelatedEntitiesValue.Remove(existingEntity);
                }
            }
            if (entitiesToRemove.Count > 0)
            {
                if (existingRelatedEntitiesValue is List<Attraction>)
                {
                    _context.Attractions.RemoveRange(entitiesToRemove.OfType<Attraction>().ToList());
                }
                else if (existingRelatedEntitiesValue is List<Review>)
                {
                    _context.Reviews.RemoveRange(entitiesToRemove.OfType<Review>().ToList());
                }
            }
        }

        //if we are updating an entity that already exists in the database and now want its related lists to be empty
        public void EmptyEntityList(IList? existingRelatedEntitiesValue)
        {
            if (existingRelatedEntitiesValue is List<Attraction>)
            {
                _context.Attractions.RemoveRange(existingRelatedEntitiesValue as List<Attraction>);
                existingRelatedEntitiesValue.Clear();
            }
            else if (existingRelatedEntitiesValue is List<Traveller>)
            {
                existingRelatedEntitiesValue.Clear();
            }
            else if (existingRelatedEntitiesValue is List<Review>)
            {
                _context.Reviews.RemoveRange(existingRelatedEntitiesValue as List<Review>);
                existingRelatedEntitiesValue.Clear();
            }
        }

        //The basic properites are update in the last else block
        public void UpdateStandardProperty<T>(T existingEntity, T newEntity, PropertyInfo property) where T : class
        {
            var newValue = property.GetValue(newEntity);
            property.SetValue(existingEntity, newValue);
        }


        //Method to get the ID property dynamically based on entity type
        public PropertyInfo GetIdProperty(Type entityType)
        {
            return entityType.GetProperties().FirstOrDefault(p =>
                p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) &&
                p.PropertyType == typeof(int));
        }

        //Method to update properties of the related entity of an already existing entity
        public void UpdateRelatedEntity<T>(T existingRelatedEntity, T newRelatedEntity) where T : class
        {
            var properties = newRelatedEntity.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    var newValue = property.GetValue(newRelatedEntity);
                    property.SetValue(existingRelatedEntity, newValue);
                }
            }
        }

        //Method that gets an existing entity if it exists or else returns null
        public object GetExistingEntity(object entity, Type entityType)
        {
            var idProperty = entityType.GetProperties()
                                       .FirstOrDefault(p => p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase));

            if (idProperty == null)
                throw new InvalidOperationException("Entity type does not have an identifiable ID property.");

            var idValue = idProperty.GetValue(entity);
            return idValue != null ? _context.Set(entityType).Find(idValue) : null;
        }

        //creating an entity based on its info in the row, properties and type
        //if the column is traveller the property is set differently
        //beacause review needs to get connected to a traveller from the database
        //row is not valid if if properties of the entity dont have a column name
        public object CreateEntity(DataRow row, Type entityType, Dictionary<string, PropertyInfo> properties, Dictionary<string, List<object>> relatedEntities)
        {
            var entity = Activator.CreateInstance(entityType);
            bool isValidRow = true;

            foreach (DataColumn column in row.Table.Columns)
            {
                string columnName = column.ColumnName.ToLowerInvariant();
                if (properties.ContainsKey(columnName))
                {
                    if (columnName != "traveller")
                    {
                        var property = properties[columnName];
                        isValidRow &= SetEntityProperty(entity, property, row[column], relatedEntities);
                    }
                    else
                    {
                        isValidRow &= SetTravellerProperty(entity as Review, row[column]);
                    }

                }
                else
                {
                    isValidRow = false;
                }
            }

            return isValidRow ? entity : null;
        }

        //setting traveller property, based on the json in the row the traveller is looked up in the database 
        //by that email, if the traveller is not found its not valid
        public bool SetTravellerProperty(Review review, object value)
        {
            if (value == DBNull.Value) return false;

            try
            {
                var json = value.ToString();
                var travellerDto = Newtonsoft.Json.JsonConvert.DeserializeObject<TravellerDTO>(json);

                if (travellerDto == null) return false;

                var matchingTraveller = _context.Travellers
                    .FirstOrDefault(t => t.Email.Equals(travellerDto.Email)) as Traveller;

                if (matchingTraveller == null)
                {
                    return false;
                }

                var travellerProperty = typeof(Review).GetProperty("Traveller");
                travellerProperty?.SetValue(review, matchingTraveller);

                var travellerIdProperty = typeof(Review).GetProperty("TravellerId");
                travellerIdProperty?.SetValue(review, matchingTraveller.TravellerId);

                return true;
            }
            catch
            {
                return false;
            }
        }

        //if the property is a list it get set using SetRelatedEntityList, if its Destination we set it using
        //SetRelatedDestination and the rest is in the else block
        //if it cant be converted to property type its not valid
        public bool SetEntityProperty(object entity, PropertyInfo property, object value, Dictionary<string, List<object>> relatedEntities)
        {
            if (value == DBNull.Value) return false;

            try
            {
                if (property.PropertyType == typeof(List<Attraction>) || property.PropertyType == typeof(List<Traveller>) || property.PropertyType == typeof(List<Review>))
                {
                    return SetRelatedEntityList(entity, property, value, relatedEntities);
                }
                else if (property.PropertyType == typeof(Destination))
                {
                    return SetRelatedDestinationEntity(entity, property, value);
                }
                else
                {
                    var convertedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(entity, convertedValue);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // This method sets the related entity list for a given property of an entity.
        // It first deserializes the input value, and based on the property type, it 
        // determines how to populate the related entities.
        public bool SetRelatedEntityList(object entity, PropertyInfo property, object value, Dictionary<string, List<object>> relatedEntities)
        {
            try
            {
                var dtoList = DeserializeValue(value);
                if (dtoList == null) return false;

                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var entityType = property.PropertyType.GetGenericArguments()[0];

                    if (entityType == typeof(Traveller))
                    {
                        return SetTravellerList(entity, property, dtoList);
                    }
                    else if (entityType == typeof(Attraction) || entityType == typeof(Review))
                    {
                        return SetAttractionOrReviewList(entity, property, dtoList, relatedEntities);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        // Method deserializes the value from JSON into a list of dictionaries.
        public List<Dictionary<string, object>> DeserializeValue(object value)
        {
            var json = value.ToString();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
        }

        // Method sets the list of Traveller entities based on the provided dtoList.
        public bool SetTravellerList(object entity, PropertyInfo property, List<Dictionary<string, object>> dtoList)
        {
            var matchingEntities = new List<Traveller>();
            var emails = dtoList.Select(dto => dto.ContainsKey("Email") ? dto["Email"].ToString() : null)
                                .Where(email => email != null)
                                .ToList();

            if (emails.Any())
            {
                matchingEntities = _context.Travellers
                    .Where(e => emails.Contains(e.Email))
                    .ToList();
            }

            property.SetValue(entity, matchingEntities);
            return true;
        }

        //Method sets the list of Attraction or Review entities based on matching items from relatedEntities.
        public bool SetAttractionOrReviewList(object entity, PropertyInfo property, List<Dictionary<string, object>> dtoList, Dictionary<string, List<object>> relatedEntities)
        {
            var relatedEntityList = relatedEntities[property.Name];
            var matchedEntities = Activator.CreateInstance(property.PropertyType) as IList;

            if (matchedEntities == null) return false;

            foreach (var relatedEntity in relatedEntityList)
            {
                bool taken = CheckEntityRelationship(entity, relatedEntity, property.PropertyType.GetGenericArguments()[0]);
                if (!taken && dtoList.Any(dto => IsMatchingEntity(relatedEntity, dto)))
                {
                    matchedEntities.Add(relatedEntity);
                }
            }

            property.SetValue(entity, matchedEntities);
            return true;
        }


        //check attraction/review when adding a destination or trip not to add it to two different objects
        public bool CheckEntityRelationship(object mainEntity, object relatedEntity, Type entityType)
        {
            var mainType = typeof(Trip);
            if (entityType == typeof(Attraction))
            {
                mainType = typeof(Destination);
            }
            var parentEntity = GetExistingEntity(mainEntity, mainType);
            var childEntity = GetExistingEntity(relatedEntity, entityType);
            if (parentEntity != null && childEntity != null)
            {

                var propertyInfoAttr = mainType.GetProperty("Attractions");
                var propertyInfoReview = mainType.GetProperty("Reviews");
                if (propertyInfoAttr != null)
                {
                    var attractions = propertyInfoAttr.GetValue(parentEntity) as IEnumerable<object>;
                    if (attractions != null)
                    {
                        return !attractions.Contains(childEntity);
                    }
                }
                else if (propertyInfoReview != null)
                {
                    var reviews = propertyInfoReview.GetValue(parentEntity) as IEnumerable<object>;
                    if (reviews != null)
                    {
                        return !reviews.Contains(childEntity);
                    }
                }

            }
            return false;
        }

        //when setting the destination we need to look it up in the database
        //after deserializing it from the value we try and set it, if it cant be found its not valid
        public bool SetRelatedDestinationEntity(object entity, PropertyInfo property, object value)
        {
            try
            {
                var json = value.ToString();
                var dto = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                if (dto == null) return false;

                dto.TryGetValue("City", out var cityValue);
                dto.TryGetValue("Country", out var countryValue);

                var city = cityValue?.ToString();
                var country = countryValue?.ToString();

                var matchingEntity = _context.Destinations
                    .Where(e =>
                        e.City == city &&
                        e.Country == country).FirstOrDefault();

                if (matchingEntity == null) return false;

                property.SetValue(entity, matchingEntity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Going through each key-value pair in the dto dictionary
        // Find the property on the entity that matches the dto key, ignoring case
        // If the property doesn't exist or its value doesn't match the expected value, return false
        // All properties match, so return true
        public bool IsMatchingEntity(object entity, Dictionary<string, object> dto)
        {
            foreach (var (propertyName, expectedValue) in dto)
            {
                var property = entity.GetType().GetProperty(propertyName,
                                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null) return false;

                var actualValue = property.GetValue(entity)?.ToString();
                if (!string.Equals(actualValue, expectedValue?.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        //If an invalid review was added we delete it
        public void ValidateReviews()
        {
            var trips = _context.Trips.ToList();

            foreach (var trip in trips)
            {
                var reviewsToRemove = new List<Review>();

                foreach (var review in trip.Reviews)
                {
                    bool travellerExistsOnTrip = trip.Travellers.Any(t => t.TravellerId == review.Traveller.TravellerId);

                    if (!travellerExistsOnTrip || trip.EndDate > DateTime.Today)
                    {
                        reviewsToRemove.Add(review);
                    }
                }
                foreach (var review in reviewsToRemove)
                {
                    trip.Reviews.Remove(review);
                    _context.Reviews.Remove(review);
                }
            }
            _context.SaveChanges();
        }
    }
}
