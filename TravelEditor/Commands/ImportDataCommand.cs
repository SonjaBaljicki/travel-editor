using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelEditor.Export.Exporters;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Export.Service;
using TravelEditor.Export_Import.Importers;
using TravelEditor.Export_Import.Iterfaces;
using TravelEditor.Export_Import.Service;
using TravelEditor.Models;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands
{
    public class ImportDataCommand : ICommand
    {
        public IImportService importService;
        public MainViewModel viewModel;
        public event EventHandler? CanExecuteChanged;

        public ImportDataCommand(IImportService importService, MainViewModel viewModel)
        {
            this.importService = importService;
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
            //based on what format user chose we create importer and call ImportFile
            IDataImporter importer = new ExcelImporter(importService);
            DataImportService dataImportService = new DataImportService(importer);
            try
            {
                dataImportService.ImportFile("../../../Data/" + viewModel.FileName + ".xlsx");
                MessageBox.Show("Data imported");
            }
            catch(FileNotFoundException ex)
            {
                MessageBox.Show("Cant find file");
            }

            Messenger.NotifyDataChanged();
        }

    }
}
