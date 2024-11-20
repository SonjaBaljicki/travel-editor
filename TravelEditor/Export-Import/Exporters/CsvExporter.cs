using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Export.Service;

namespace TravelEditor.Export.Exporters
{
    public class CsvExporter : IDataExporter
    {

        private readonly IExportService _exportService;

        public CsvExporter(IExportService exportService)
        {
            _exportService = exportService;
        }

        public void Export(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
