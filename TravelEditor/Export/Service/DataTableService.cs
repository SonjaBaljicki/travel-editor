using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Database;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Models;

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
                    if (prop.PropertyType == typeof(Destination) || prop.PropertyType == typeof(Traveller) ||
                        prop.PropertyType == typeof(List<Traveller>) || prop.PropertyType == typeof(List<Review>)
                        || prop.PropertyType == typeof(List<Attraction>))
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
                                ? string.Join(", ", travellers.Select(t => t.Email))
                                : DBNull.Value;
                        }
                        else if (prop.PropertyType == typeof(List<Review>))
                        {
                            var reviews = prop.GetValue(entity) as List<Review>;
                            row[prop.Name] = reviews != null
                                ? string.Join(", ", reviews.Select(r => r.Comment))
                                : DBNull.Value;
                        }
                        else if (prop.PropertyType == typeof(Destination))
                        {
                            var destination = prop.GetValue(entity) as Destination;
                            row[prop.Name] = destination != null
                                ? $"{destination.City}, {destination.Country}"
                                : DBNull.Value;
                        }
                        else if (prop.PropertyType == typeof(Traveller))
                        {
                            var traveller = prop.GetValue(entity) as Traveller;
                            row[prop.Name] = traveller != null
                                ? traveller.Email
                                : DBNull.Value;
                        }
                        else if (prop.PropertyType == typeof(List<Attraction>))
                        {
                            var attractions = prop.GetValue(entity) as List<Attraction>;
                            row[prop.Name] = attractions != null
                                ? string.Join(", ", attractions.Select(a => a.Name))
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
    }
}
