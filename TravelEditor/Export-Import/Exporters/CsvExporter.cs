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

        private readonly IDataTableService _dataTableService;

        public CsvExporter(IDataTableService dataTableService)
        {
            _dataTableService = dataTableService;
        }

        public void Export(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
