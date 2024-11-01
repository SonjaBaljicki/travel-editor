using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Export.Exporters;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Export.Service;
using TravelEditor.Export_Import.Importers;
using TravelEditor.Export_Import.Iterfaces;
using TravelEditor.Export_Import.Service;
using TravelEditor.Models;

namespace TravelEditor.Commands
{
    public class ImportDataCommand : ICommand
    {
        public IImportService importService;
        public ImportDataCommand(IImportService importService )
        {
            this.importService = importService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //based on what format user chose we create importer and call ImportFile
            IDataImporter importer = new ExcelImporter(importService);
            DataImportService dataImportService = new DataImportService(importer);
            dataImportService.ImportFile("../../../Data/imported_data.xlsx");

            Messenger.NotifyDataChanged();

        }
    }
}
