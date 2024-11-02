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
        public IExportService exportService;
        public MainViewModel viewModel;
        public event EventHandler? CanExecuteChanged;

        public ExportDataCommand(IExportService exportService, MainViewModel viewModel)
        {
            this.exportService = exportService;
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.FileName))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(viewModel.FileName);
        }

        public void Execute(object? parameter)
        {
            //based on what format user chose we create exporter and call ExportToFile
            IDataExporter exporter = new ExcelExporter(exportService);
            DataExportService dataExportService = new DataExportService(exporter);
            dataExportService.ExportToFile( "../../../Data/"+viewModel.FileName+".xlsx");
        }
    }
}
