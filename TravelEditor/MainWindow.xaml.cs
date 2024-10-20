using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelEditor.Database;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ITripService tripService;
        private IDestinationService destinationService;
        private IAttractionService attractionService;
        private IReviewService reviewService;
        private ITravellerService travellerService;

        public MainWindow()
        {
        }

        public MainWindow(ITripService tripService, IDestinationService destinationService, IAttractionService attractionService, IReviewService reviewService, ITravellerService travellerService)
        {
            InitializeComponent();

            this.tripService = tripService;
            this.destinationService = destinationService;
            this.attractionService = attractionService;
            this.reviewService = reviewService;
            this.travellerService = travellerService;

            MainViewModel mainViewModel = new MainViewModel(tripService, destinationService, attractionService, reviewService, travellerService);
            this.DataContext = mainViewModel;

        }
    }
}
