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
using TravelEditor.Views;

namespace TravelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var db = new DatabaseContext())
            {
                destinationsGrid.ItemsSource = db.destinations.ToList();
                attractionsGrid.ItemsSource = db.attractions.ToList();
            }
        }

        private void addTripButton_Click(object sender, RoutedEventArgs e)
        {
            TripView tripView = new TripView();
            tripView.Show();
        }

        private void addDestinationButton_Click(object sender, RoutedEventArgs e)
        {
            DestinationView destinationView = new DestinationView();
            destinationView.Show();
        }

        private void addAttractionButton_Click(object sender, RoutedEventArgs e)
        {
            AttractionView attractionView = new AttractionView();
            attractionView.Show();
        }

        private void addTravellerButton_Click(object sender, RoutedEventArgs e)
        {
            TravellerView travellerView = new TravellerView();
            travellerView.Show();
        }

        private void addReviewButton_Click(object sender, RoutedEventArgs e)
        {
            ReviewView reviewView = new ReviewView();
            reviewView.Show();
        }
    }
}
