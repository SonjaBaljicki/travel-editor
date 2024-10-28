using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Database;
using TravelEditor.Export.Exporters;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Export.Service;
using TravelEditor.Models;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands
{
    public class ExportDataCommand : ICommand
    {
        public IDataTableService dataTableService;
        public ExportDataCommand(IDataTableService dataTableService)
        {
            this.dataTableService = dataTableService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //based on what format user chose we create exporter and call ExportToFile
            IDataExporter exporter = new ExcelExporter(dataTableService);
            DataExportService exportService = new DataExportService(exporter);
            exportService.ExportToFile( "../../../Data/exported_data.xlsx");
        }
    }
}
