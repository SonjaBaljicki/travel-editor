using System;
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

        public DataTable GetAsDataTable<T>() where T : class
        {
            DataTable dataTable = new DataTable();
            var entities = _context.Set<T>().ToList();

            if (entities.Any())
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (var prop in properties)
                {
                    if (prop.PropertyType == typeof(List<Traveller>) ||
                        prop.PropertyType == typeof(List<Review>) ||
                        prop.PropertyType == typeof(List<Attraction>))
                    {
                        dataTable.Columns.Add(prop.Name, typeof(string));
                    }
                    else if (prop.PropertyType == typeof(Destination) || prop.PropertyType == typeof(Traveller))
                    {
                        dataTable.Columns.Add(prop.Name, typeof(string));
                    }
                    else
                    {
                        dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }
                }

                foreach (var entity in entities)
                {
                    DataRow row = dataTable.NewRow();

                    foreach (var prop in properties)
                    {
                        if (prop.PropertyType == typeof(List<Traveller>))
                        {
                            var travellers = prop.GetValue(entity) as List<Traveller>;
                            row[prop.Name] = travellers != null
                                ? JsonSerializer.Serialize(travellers.Select(t => new { t.TravellerId, t.Email, t.FirstName }))
                                : DBNull.Value;
                        }
                        else if (prop.PropertyType == typeof(List<Review>))
                        {
                            var reviews = prop.GetValue(entity) as List<Review>;
                            row[prop.Name] = reviews != null
                                ? JsonSerializer.Serialize(reviews.Select(r => new { r.ReviewId, r.Comment, r.Rating }))
                                : DBNull.Value;
                        }
                        else if (prop.PropertyType == typeof(List<Attraction>))
                        {
                            var attractions = prop.GetValue(entity) as List<Attraction>;
                            row[prop.Name] = attractions != null
                                ? JsonSerializer.Serialize(attractions.Select(a => new { a.AttractionId, a.Name }))
                                : DBNull.Value;
                        }
                        else if (prop.PropertyType == typeof(Destination))
                        {
                            var destination = prop.GetValue(entity) as Destination;
                            row[prop.Name] = destination != null
                                ? JsonSerializer.Serialize(new { destination.DestinationId, destination.City, destination.Country })
                                : DBNull.Value;
                        }
                        else if (prop.PropertyType == typeof(Traveller))
                        {
                            var traveller = prop.GetValue(entity) as Traveller;
                            row[prop.Name] = traveller != null
                                ? JsonSerializer.Serialize(new { traveller.TravellerId, traveller.Email, traveller.FirstName })
                                : DBNull.Value;
                        }
                        else
                        {
                            row[prop.Name] = prop.GetValue(entity) ?? DBNull.Value;
                        }
                    }

                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

        public void ImportTravellers(DataTable table, Type entityType)
        {
            if (table == null) return;

            foreach (DataRow row in table.Rows)
            {
                var entity = Activator.CreateInstance(entityType);

                foreach (DataColumn column in table.Columns)
                {
                    var property = entityType.GetProperty(column.ColumnName);
                    if (property != null && row[column] != DBNull.Value && column.ColumnName != "Attractions")
                    {
                        property.SetValue(entity, Convert.ChangeType(row[column], property.PropertyType));
                    }
                }

                _context.Set(entityType).Add(entity);
            }
            _context.SaveChanges();
        }

        public List<Attraction> GetAttractions(DataTable? table, Type entityType)
        {
            List<Attraction> attractions = new List<Attraction>();

            if (table == null || entityType != typeof(Attraction))
                return attractions;

            foreach (DataRow row in table.Rows)
            {
                var entity = Activator.CreateInstance(entityType) as Attraction;

                if (entity == null)
                    continue;

                foreach (DataColumn column in table.Columns)
                {
                    var property = entityType.GetProperty(column.ColumnName);
                    if (property != null && row[column] != DBNull.Value)
                    {
                        property.SetValue(entity, Convert.ChangeType(row[column], property.PropertyType));
                    }
                }

                attractions.Add(entity);
            }

            return attractions;
        }


        public void ImportDestinations(DataTable? table, Type entityType, List<Attraction> attractions)
        {
            if (table == null) return;

            foreach (DataRow row in table.Rows)
            {
                var entity = Activator.CreateInstance(entityType);

                foreach (DataColumn column in table.Columns)
                {
                    var property = entityType.GetProperty(column.ColumnName);
                    if (property != null && row[column] != DBNull.Value && column.ColumnName != "Attractions")
                    {
                        property.SetValue(entity, Convert.ChangeType(row[column], property.PropertyType));
                    }
                    else if (column.ColumnName == "Attractions" && row[column] != DBNull.Value)

                    {
                        var json = row[column].ToString();

                        var attractionDTOs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AttractionDTO>>(json);

                        var attractionIds = attractionDTOs.Select(a => a.AttractionId).ToList();

                        var matchingAttractions = attractions
                            .Where(a => attractionIds.Contains(a.AttractionId))
                            .ToList();

                        var attractionsProperty = entityType.GetProperty("Attractions");
                        if (attractionsProperty != null)
                        {
                            attractionsProperty.SetValue(entity, matchingAttractions);
                        }

                    }
                }

                _context.Set(entityType).Add(entity);
            }
            _context.SaveChanges();
        }

        public List<Review> GetReviews(DataTable? table, Type entityType)
        {
            List<Review> reviews = new List<Review>();

            if (table == null || entityType != typeof(Review))
                return reviews;

            foreach (DataRow row in table.Rows)
            {
                var entity = Activator.CreateInstance(entityType) as Review;

                if (entity == null)
                    continue;

                foreach (DataColumn column in table.Columns)
                {
                    var property = entityType.GetProperty(column.ColumnName);
                    if (property != null && row[column] != DBNull.Value && column.ColumnName!="TravellerId" 
                        && column.ColumnName!="Traveller")
                    {
                        property.SetValue(entity, Convert.ChangeType(row[column], property.PropertyType));
                    }
                    else if (column.ColumnName == "Traveller" && row[column] != DBNull.Value)
                    {
                        var json = row[column].ToString();

                        var travellerDto = Newtonsoft.Json.JsonConvert.DeserializeObject<TravellerDTO>(json);

                        var matchingTraveller = _context.Travellers
                            .Where(t => t.Email.Equals(travellerDto.Email))
                            .FirstOrDefault() as Traveller;

                        var travellerProperty = entityType.GetProperty("Traveller");
                        if (travellerProperty != null)
                        {
                            travellerProperty.SetValue(entity, matchingTraveller);
                        }
                        var travellerIdProperty = entityType.GetProperty("TravellerId");
                        if (travellerIdProperty != null)
                        {
                            travellerIdProperty.SetValue(entity, matchingTraveller.TravellerId);
                        }

                    }
                }

                reviews.Add(entity);
            }

            return reviews;
        }

        public void ImportTrips(DataTable? table, Type entityType, List<Review> reviews)
        {
            if (table == null) return;

            foreach (DataRow row in table.Rows)
            {
                var entity = Activator.CreateInstance(entityType);

                foreach (DataColumn column in table.Columns)
                {
                    var property = entityType.GetProperty(column.ColumnName);
                    if (property != null && row[column] != DBNull.Value && column.ColumnName != "Travellers" && 
                        column.ColumnName!="Reviews" && column.ColumnName!="Destination")
                    {
                        property.SetValue(entity, Convert.ChangeType(row[column], property.PropertyType));
                    }
                    else if (column.ColumnName == "Reviews" && row[column] != DBNull.Value)
                    {
                        var json = row[column].ToString();

                        var reviewDTOs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReviewDTO>>(json);

                        var reviewIds = reviewDTOs.Select(r => r.ReviewId).ToList();

                        var matchingReviews = reviews
                            .Where(r => reviewIds.Contains(r.ReviewId))
                            .ToList();

                        var reviewsProperty = entityType.GetProperty("Reviews");
                        if (reviewsProperty != null)
                        {
                            reviewsProperty.SetValue(entity, matchingReviews);
                        }

                    }
                    else if (column.ColumnName == "Travellers" && row[column] != DBNull.Value)
                    {
                        var json = row[column].ToString();

                        var travellerDTOs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TravellerDTO>>(json);
                        var travellerEmails = travellerDTOs.Select(t => t.Email).ToList();


                        var matchingTravellers = _context.Travellers
                          .Where(t => travellerEmails.Contains(t.Email))
                          .ToList();

                        var travellersProperty = entityType.GetProperty("Travellers");
                        if (travellersProperty != null)
                        {
                            travellersProperty.SetValue(entity, matchingTravellers);
                        }
                    }
                    else if(column.ColumnName == "Destination" && row[column] != DBNull.Value)
                    {
                        var json = row[column].ToString();

                        var destinationDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<DestinationDTO>(json);

                        var matchingDestination = _context.Destinations
                          .Where(d => d.City.Equals(destinationDTO.City) && d.Country.Equals(destinationDTO.Country))
                          .FirstOrDefault() as Destination;

                        var destinationProperty = entityType.GetProperty("Destination");
                        if (destinationProperty != null)
                        {
                            destinationProperty.SetValue(entity, matchingDestination);
                        }
                    }
                }

                _context.Set(entityType).Add(entity);
            }
            _context.SaveChanges();
        }
    }
}
