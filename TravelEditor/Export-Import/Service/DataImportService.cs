using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Export_Import.Iterfaces;

namespace TravelEditor.Export_Import.Service
{
    public class DataImportService
    {
        private readonly IDataImporter _dataImproter;

        public DataImportService(IDataImporter dataImporter)
        {
            _dataImproter = dataImporter;
        }
        public void ImportFile(string filePath)
        {
            _dataImproter.Import(filePath);
        }
    }
}
