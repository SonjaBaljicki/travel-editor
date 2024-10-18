﻿using System;
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

        private void addTripButton_Click(object sender, RoutedEventArgs e)
        {
            TripView tripView = new TripView(null);
            tripView.Show();
        }

        private void addDestinationButton_Click(object sender, RoutedEventArgs e)
        {
            DestinationView destinationView = new DestinationView(null);
            destinationView.Show();
        }

        private void addAttractionButton_Click(object sender, RoutedEventArgs e)
        {
            AttractionView attractionView = new AttractionView(null);
            attractionView.Show();
        }

        private void addTravellerButton_Click(object sender, RoutedEventArgs e)
        {
            TravellerView travellerView = new TravellerView(null);
            travellerView.Show();
        }

        private void addReviewButton_Click(object sender, RoutedEventArgs e)
        {
            ReviewView reviewView = new ReviewView(null);
            reviewView.Show();
        }
        private void editTripButton_Click(object sender, RoutedEventArgs e)
        {
            if (tripsGrid.SelectedItem != null)
            {
                TripView tripView = new TripView((Trip)tripsGrid.SelectedItem);
                tripView.Show();
            }
            else
            {
                MessageBox.Show("Pleas select a trip you want to edit.");
            }
        }
        private void editDestinationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            //if we the editing is done from the destinations tab
            if (button!=null && button.Name== "editDestinationButton")
            {
                if (destinationsGrid.SelectedItem != null)
                {
                    DestinationView destinationView = new DestinationView((Destination)destinationsGrid.SelectedItem);
                    destinationView.Show();
                }
            }
            //if the destination is referenced from the trips tab
            if (button != null && button.Name == "viewDestinationButton")
            {
                if (tripsGrid.SelectedItem != null)
                {
                    Trip trip = (Trip)tripsGrid.SelectedItem;
                    DestinationView destinationView = new DestinationView(trip.Destination);
                    destinationView.Show();
                }
            }
        }
        private void editAttractionButton_Click(object sender, RoutedEventArgs e)
        {
            if (attractionsGrid.SelectedItem != null)
            {
                AttractionView attractioView = new AttractionView((Attraction)attractionsGrid.SelectedItem);
                attractioView.Show();
            }
            else
            {
                MessageBox.Show("Pleas select an attraction you want to edit.");
            }
        }
        private void editTravellerButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            //if we the editing is done from the travellers tab
            if (button != null && button.Name == "editTravellerButton")
            {
                if (travellersGrid.SelectedItem != null)
                {
                    TravellerView travellerView = new TravellerView((Traveller)travellersGrid.SelectedItem);
                    travellerView.Show();
                }
            }
            //if the traveller is referenced from the reviews tab
            if (button != null && button.Name == "viewTravellerButton")
            {
                if (reviewsGrid.SelectedItem != null)
                {
                    Review review = (Review)reviewsGrid.SelectedItem;
                    TravellerView travellerView = new TravellerView((Traveller)review.Traveller);
                    travellerView.Show();
                }
            }
        }
        private void editReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (reviewsGrid.SelectedItem != null)
            {
                ReviewView reviewView = new ReviewView((Review)reviewsGrid.SelectedItem);
                reviewView.Show();
            }
            else
            {
                MessageBox.Show("Pleas select a review you want to edit.");
            }
        }

        private void viewAttractionsButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Destination destination = button.DataContext as Destination;
            if (destination != null)
            {
                AttractionsGridView attractionsGridView = new AttractionsGridView(destination.Attractions);
                attractionsGridView.Show();
            }
         
        }

        private void viewTravellersButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Trip trip = button.DataContext as Trip;
            if (trip != null)
            {
                TravellersGridView travellersGridView = new TravellersGridView(trip.Travellers);
                travellersGridView.Show();
            }
        }

        private void viewReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Trip trip = button.DataContext as Trip;
            if (trip != null)
            {
                ReviewsGridView reviewsGridView = new ReviewsGridView(trip.Reviews);
                reviewsGridView.Show();
            }
        }
    }
}
