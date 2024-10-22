using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;

namespace TravelEditor.Views
{
    /// <summary>
    /// Interaction logic for TripView.xaml
    /// </summary>
    public partial class TripView : Window
    {
        public TripView(Trip trip,MainViewModel mainViewModel, ITripService tripService, IDestinationService destinationService)
        {
            InitializeComponent();
            TripViewModel tripViewModel = new TripViewModel(trip, mainViewModel, tripService, destinationService);
            this.DataContext = tripViewModel;
        }
    }
}
