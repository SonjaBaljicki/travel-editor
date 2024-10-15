using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Database;
using TravelEditor.Models;
using TravelEditor.Services;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    internal class MainViewModel
    {
        private readonly ITripService _tripService;
        private readonly IDestinationService _destinationService;
        private readonly IAttractionService _attractionService;
        private readonly IReviewService _reviewService;
        private readonly ITravellerService _travellerService;

        public ObservableCollection<Trip> Trips { get; set; }
        public ObservableCollection<Destination> Destinations { get; set; }
        public ObservableCollection<Attraction> Attractions { get; set; }
        public ObservableCollection<Review> Reviews { get; set; }
        public ObservableCollection<Traveller> Travellers { get; set; }

        public MainViewModel(ITripService tripService, IDestinationService destinationService, IAttractionService attractionService, IReviewService reviewService, ITravellerService travellerService)
        {
            _tripService = tripService;
            _destinationService = destinationService;
            _attractionService = attractionService;
            _reviewService = reviewService;
            _travellerService = travellerService;
            LoadData();

        }

        private void LoadData()
        {
            Trips = new ObservableCollection<Trip>(_tripService.LoadAll());
            Destinations =new ObservableCollection<Destination>(_destinationService.LoadAll());
            Attractions = new ObservableCollection<Attraction>(_attractionService.LoadAll());
            Reviews = new ObservableCollection<Review>(_reviewService.LoadAll());
            Travellers = new ObservableCollection<Traveller>(_travellerService.LoadAll());
        }
    }
}
