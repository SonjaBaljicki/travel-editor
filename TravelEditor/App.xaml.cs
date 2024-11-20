using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TravelEditor.Repositories;
using TravelEditor.Services.Interfaces;
using TravelEditor.Services;
using TravelEditor.Database;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Export.Service;
using TravelEditor.Export_Import.Iterfaces;
using TravelEditor.Export_Import.Service;

namespace TravelEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ITripService TripService { get; set; }
        public IDestinationService DestinationService { get; set; }
        public IAttractionService AttractionService { get; set; }
        public IReviewService ReviewService { get; set; }
        public ITravellerService TravellerService { get; set; }
        public IExportService ExportService { get; set; }
        public IImportService ImportService { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DatabaseContext dbContext = new DatabaseContext();
            TripService = new TripService(new TripRepository(dbContext));
            DestinationService = new DestinationService(new DestinationRepository(dbContext));
            AttractionService = new AttractionService(new AttractionRepository(dbContext),DestinationService);
            ReviewService = new ReviewService(new ReviewRepository(dbContext),TripService);
            TravellerService = new TravellerService(new TravellerRepository(dbContext),TripService, ReviewService);
            ExportService = new ExportService(dbContext);
            ImportService = new ImportService(dbContext);

            MainWindow mainWindow = new MainWindow(TripService, DestinationService, AttractionService, ReviewService, TravellerService, ExportService, ImportService);
            mainWindow.Show();
        }
    }
}
