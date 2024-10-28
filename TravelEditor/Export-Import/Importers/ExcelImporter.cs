using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Export_Import.Iterfaces;
using TravelEditor.Models;

namespace TravelEditor.Export_Import.Importers
{
    internal class ExcelImporter : IDataImporter
    {
        private IDataTableService dataTableService;

        public ExcelImporter(IDataTableService dataTableService)
        {
            this.dataTableService = dataTableService;
        }

        public void Import(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });

                    List<Attraction> attractions = dataTableService.GetAttractions(result.Tables["Attractions"], typeof(Attraction));
                    dataTableService.ImportDestinations(result.Tables["Destinations"], typeof(Destination), attractions);
                    dataTableService.ImportTravellers(result.Tables["Travellers"], typeof(Traveller));
                    List<Review> reviews = dataTableService.GetReviews(result.Tables["Reviews"], typeof(Review));
                    dataTableService.ImportTrips(result.Tables["Trips"], typeof(Trip), reviews);
                }
            }
        }
    }
}
