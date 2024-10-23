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
        public ExportDataCommand()
        {
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            IDataExporter exporter= new ExcelExporter();
            DataExportService exportService = new DataExportService(exporter);
            exportService.ExportToFile( "../../../Data/exported_data.xlsx");
        }
    }
}
