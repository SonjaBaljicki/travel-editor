using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Export.Iterfaces;

namespace TravelEditor.Export.Service
{
    public class DataExportService
    {
        private readonly IDataExporter _dataExporter;

        public DataExportService(IDataExporter dataExporter)
        {
            _dataExporter = dataExporter;
        }
        public void ExportToFile(string filePath)
        {
            _dataExporter.Export(filePath);
        }
    }
}
