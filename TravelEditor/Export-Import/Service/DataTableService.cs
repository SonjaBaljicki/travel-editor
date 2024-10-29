using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents;
using TravelEditor.Database;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Models;
using TravelEditor.Models.dtos;

namespace TravelEditor.Export.Service
{
    public class DataTableService : IDataTableService
    {
        private readonly DatabaseContext _context;

        public DataTableService(DatabaseContext context)
        {
            _context = context;
        }

        public void ClearDatabase()
        {
            var trips = _context.Trips.ToList();
            _context.Trips.RemoveRange(trips);
            var destinations = _context.Destinations.ToList();
            _context.Destinations.RemoveRange(destinations);
            var trevellers = _context.Travellers.ToList();
            _context.Travellers.RemoveRange(trevellers);

            _context.SaveChanges();
        }

        //getting records from the database for each entity
        //then making columns based on properties and adding each record as a row 
        public DataTable GetAsDataTable<T>() where T : class
        {
            DataTable dataTable = new DataTable();
            var entities = _context.Set<T>().ToList();

            if (entities.Any())
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                AddColumns(dataTable, properties);

                foreach (var entity in entities)
                {
                    DataRow row = dataTable.NewRow();
                    PopulateRow(row, entity, properties);
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

        //for every property set column type and add columns
        private void AddColumns(DataTable dataTable, PropertyInfo[] properties)
        {
            foreach (var prop in properties)
            {
                Type columnType = prop.PropertyType == typeof(List<Traveller>) ||
                                  prop.PropertyType == typeof(List<Review>) ||
                                  prop.PropertyType == typeof(List<Attraction>) ||
                                  prop.PropertyType == typeof(Destination) ||
                                  prop.PropertyType == typeof(Traveller)
                    ? typeof(string)
                    : Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                dataTable.Columns.Add(prop.Name, columnType);
            }
        }

        //adding data to rows, serializing if the property type is an object
        private void PopulateRow(DataRow row, object entity, PropertyInfo[] properties)
        {
            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(List<Traveller>))
                {
                    row[prop.Name] = SerializeTravellers(prop.GetValue(entity) as List<Traveller>);
                }
                else if (prop.PropertyType == typeof(List<Review>))
                {
                    row[prop.Name] = SerializeReviews(prop.GetValue(entity) as List<Review>);
                }
                else if (prop.PropertyType == typeof(List<Attraction>))
                {
                    row[prop.Name] = SerializeAttractions(prop.GetValue(entity) as List<Attraction>);
                }
                else if (prop.PropertyType == typeof(Destination))
                {
                    row[prop.Name] = SerializeDestination(prop.GetValue(entity) as Destination);
                }
                else if (prop.PropertyType == typeof(Traveller))
                {
                    row[prop.Name] = SerializeTraveller(prop.GetValue(entity) as Traveller);
                }
                else
                {
                    row[prop.Name] = prop.GetValue(entity) ?? DBNull.Value;
                }
            }
        }

        private string SerializeTravellers(List<Traveller> travellers)
        {
            return travellers != null
                ? JsonSerializer.Serialize(travellers.Select(t => new { t.TravellerId, t.Email, t.FirstName }))
                : DBNull.Value.ToString();
        }

        private string SerializeReviews(List<Review> reviews)
        {
            return reviews != null
                ? JsonSerializer.Serialize(reviews.Select(r => new { r.ReviewId, r.Comment, r.Rating }))
                : DBNull.Value.ToString();
        }

        private string SerializeAttractions(List<Attraction> attractions)
        {
            return attractions != null
                ? JsonSerializer.Serialize(attractions.Select(a => new { a.AttractionId, a.Name }))
                : DBNull.Value.ToString();
        }

        private string SerializeDestination(Destination destination)
        {
            return destination != null
                ? JsonSerializer.Serialize(new { destination.DestinationId, destination.City, destination.Country })
                : DBNull.Value.ToString();
        }

        private string SerializeTraveller(Traveller traveller)
        {
            return traveller != null
                ? JsonSerializer.Serialize(new { traveller.TravellerId, traveller.Email, traveller.FirstName })
                : DBNull.Value.ToString();
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
        public void ImportEntities(DataTable table, Type entityType, Dictionary<string, List<object>> relatedEntities = null)
        {
            if (table == null) return;

            var properties = entityType.GetProperties()
                                       .ToDictionary(p => p.Name.ToLowerInvariant(), p => p);

            foreach (DataRow row in table.Rows)
            {
                var entity = CreateEntity(row, entityType, properties, relatedEntities);
                if (entity != null)
                {
                    _context.Set(entityType).Add(entity);
                }
            }

            _context.SaveChanges();
        }

        //creating an entity based on its info in the row, properties and type
        //if the column is traveller the property is set differently
        //beacause review needs to get connected to a traveller from the database
        //row is not valid if if properties of the entity dont have a column name
        private object CreateEntity(DataRow row, Type entityType, Dictionary<string, PropertyInfo> properties, Dictionary<string, List<object>> relatedEntities)
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
        private bool SetTravellerProperty(Review review, object value)
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
        private bool SetEntityProperty(object entity, PropertyInfo property, object value, Dictionary<string, List<object>> relatedEntities)
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

        //first the value gets deserialized
        //if the list is of type Traveller we lookup the travellers in the database and set them
        //if the list if of type Attractions or Reviews it means we passed all of them from the data table (param relatedEntites)
        //we look what items in the dtoList match items in relatedEntities
        private bool SetRelatedEntityList(object entity, PropertyInfo property, object value, Dictionary<string, List<object>> relatedEntities)
        {
            try
            {
                var json = value.ToString();
                var dtoList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

                if (dtoList == null)
                    return false;

                if (property.PropertyType.IsGenericType &&
                    property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var entityType = property.PropertyType.GetGenericArguments()[0];

                    if (entityType == typeof(Traveller))
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
                    else if (entityType == typeof(Attraction) || entityType == typeof(Review))
                    {
                        var relatedEntityList = relatedEntities[property.Name];
                        var matchedEntities = Activator.CreateInstance(property.PropertyType) as IList;

                        if (matchedEntities == null)
                            return false;

                        foreach (var relatedEntity in relatedEntityList)
                        {
                            if (dtoList.Any(dto => IsMatchingEntity(relatedEntity, dto)))
                            {
                                matchedEntities.Add(relatedEntity);
                            }
                        }
                        property.SetValue(entity, matchedEntities);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        //when setting the destination we need to look it up in the database
        //after deserializing it from the value we try and set it, if it cant be found its not valid
        private bool SetRelatedDestinationEntity(object entity, PropertyInfo property, object value)
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
        private bool IsMatchingEntity(object entity, Dictionary<string, object> dto)
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
    }
}
