using Spire.Xls;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using TravelEditor.Database;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Models;

namespace TravelEditor.Export.Exporters
{
    public class ExcelExporter : IDataExporter
    {
        public void Export(string filePath)
        {
            Workbook workbook = new Workbook();

            DataTable tripsDataTable = GetAsDataTable<Trip>();
            Worksheet tripSheet = workbook.Worksheets[0];
            tripSheet.Name = "Trips";
            tripSheet.InsertDataTable(tripsDataTable, true, 1, 1);

            DataTable destinationDataTable = GetAsDataTable<Destination>();
            Worksheet destinationSheet = workbook.Worksheets.Add("Destinations");
            destinationSheet.InsertDataTable(destinationDataTable, true, 1, 1);

            DataTable attractionsDataTable = GetAsDataTable<Attraction>();
            Worksheet attractionSheet = workbook.Worksheets.Add("Attraction");
            attractionSheet.InsertDataTable(attractionsDataTable, true, 1, 1);

            DataTable travellersDataTable = GetAsDataTable<Traveller>();
            Worksheet travellerSheet = workbook.Worksheets.Add("Travellers");
            travellerSheet.InsertDataTable(travellersDataTable, true, 1, 1);

            DataTable reviewsDataTable = GetAsDataTable<Review>();
            Worksheet reviewSheet = workbook.Worksheets.Add("Reviews");
            reviewSheet.InsertDataTable(reviewsDataTable, true, 1, 1);

            foreach (Worksheet sheet in workbook.Worksheets)
            {
                sheet.AllocatedRange.AutoFitColumns();
            }
            workbook.SaveToFile(filePath, ExcelVersion.Version2016);
        }

        public DataTable GetAsDataTable<T>() where T : class
        {
            DataTable dataTable = new DataTable();

            using (var context = new DatabaseContext())
            {
                var entities = context.Set<T>().ToList();

                if (entities.Any())
                {
                    PropertyInfo[] properties = typeof(T).GetProperties();
                    foreach (var prop in properties)
                    {
                        dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }
                    foreach (var entity in entities)
                    {
                        DataRow row = dataTable.NewRow();
                        foreach (var prop in properties)
                        {
                            row[prop.Name] = prop.GetValue(entity) ?? DBNull.Value;
                        }
                        dataTable.Rows.Add(row);
                    }
                }
            }
            return dataTable;
        }

    }
}
