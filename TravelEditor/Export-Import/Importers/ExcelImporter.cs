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
        private IImportService importService;

        public ExcelImporter(IImportService importService)
        {
            this.importService = importService;
        }

        //Reading data from excel file
        //Getting attractions from the data table and then creating destiantions with the related attractions
        //Creating travellers from the data table, then doing the same with reviews as with attractions
        //After getting reviews creating trips
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

                    List<Attraction> attractions = importService.GetEntities<Attraction>(result.Tables["Attractions"]);

                    var attractionDictionary = new Dictionary<string, List<object>>
                    {
                        { nameof(Destination.Attractions), attractions.Cast<object>().ToList() }
                    };

                    importService.ImportEntities<Destination>(result.Tables["Destinations"], attractionDictionary);

                    importService.ImportEntities<Traveller>(result.Tables["Travellers"]);

                    List<Review> reviews = importService.GetEntities<Review>(result.Tables["Reviews"]);

                    var tripDictionary = new Dictionary<string, List<object>>
                    {
                        { nameof(Trip.Reviews), reviews.Cast<object>().ToList() }
                    };

                    importService.ImportEntities<Trip>(result.Tables["Trips"], tripDictionary);

                    importService.ValidateReviews();

                }
            }
        }
    }
}
