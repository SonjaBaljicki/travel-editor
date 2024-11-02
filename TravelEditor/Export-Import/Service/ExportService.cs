using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Migrations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using TravelEditor.Database;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Models;
using TravelEditor.Models.dtos;

namespace TravelEditor.Export.Service
{
    public class ExportService : IExportService
    {
        private readonly DatabaseContext _context;

        public ExportService(DatabaseContext context)
        {
            _context = context;
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
        public void AddColumns(DataTable dataTable, PropertyInfo[] properties)
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
        public void PopulateRow(DataRow row, object entity, PropertyInfo[] properties)
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

        public string SerializeTravellers(List<Traveller> travellers)
        {
            return travellers != null
                ? JsonSerializer.Serialize(travellers.Select(t => new { t.TravellerId, t.Email, t.FirstName }))
                : DBNull.Value.ToString();
        }

        public string SerializeReviews(List<Review> reviews)
        {
            return reviews != null
                ? JsonSerializer.Serialize(reviews.Select(r => new { r.ReviewId, r.Comment, r.Rating }))
                : DBNull.Value.ToString();
        }

        public string SerializeAttractions(List<Attraction> attractions)
        {
            return attractions != null
                ? JsonSerializer.Serialize(attractions.Select(a => new { a.AttractionId, a.Name }))
                : DBNull.Value.ToString();
        }

        public string SerializeDestination(Destination destination)
        {
            return destination != null
                ? JsonSerializer.Serialize(new { destination.DestinationId, destination.City, destination.Country })
                : DBNull.Value.ToString();
        }

        public string SerializeTraveller(Traveller traveller)
        {
            return traveller != null
                ? JsonSerializer.Serialize(new { traveller.TravellerId, traveller.Email, traveller.FirstName })
                : DBNull.Value.ToString();
        }
    }
}
