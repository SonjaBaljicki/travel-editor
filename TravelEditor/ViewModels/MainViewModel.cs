using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Commands;
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

        private Trip? _selectedTrip;
        public Trip? SelectedTrip
        {
            get { return _selectedTrip; }
            set
            {
                _selectedTrip = value;
                OnPropertyChanged(nameof(SelectedTrip));
            }
        }
        private Destination? _selectedDestination;
        public Destination? SelectedDestination
        {
            get { return _selectedDestination; }
            set
            {
                _selectedDestination = value;
                OnPropertyChanged(nameof(SelectedDestination));
            }
        }
        private Attraction? _selectedAttraction;
        public Attraction? SelectedAttraction
        {
            get { return _selectedAttraction; }
            set
            {
                _selectedAttraction = value;
                OnPropertyChanged(nameof(SelectedAttraction));
            }
        }
        private Traveller? _selectedTraveller;
        public Traveller? SelectedTraveller
        {
            get { return _selectedTraveller; }
            set
            {
                _selectedTraveller = value;
                OnPropertyChanged(nameof(SelectedTraveller));
            }
        }
        private Review? _selectedReview;
        public Review? SelectedReview
        {
            get { return _selectedReview; }
            set
            {
                _selectedReview = value;
                OnPropertyChanged(nameof(SelectedReview));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Trip> Trips { get; set; }
        public ObservableCollection<Destination> Destinations { get; set; }
        public ObservableCollection<Attraction> Attractions { get; set; }
        public ObservableCollection<Review> Reviews { get; set; }
        public ObservableCollection<Traveller> Travellers { get; set; }

        public DeleteTripCommand DeleteTripCommand { get; }
        public DeleteDestinationCommand DeleteDestinationCommand { get; }
        public DeleteAttractionCommand DeleteAttractionCommand { get; set; }
        public DeleteTravellerCommand DeleteTravellerCommand { get; }
        public DeleteReviewCommand DeleteReviewCommand { get; }

        public MainViewModel(ITripService tripService, IDestinationService destinationService, IAttractionService attractionService, IReviewService reviewService, ITravellerService travellerService)
        {
            _tripService = tripService;
            _destinationService = destinationService;
            _attractionService = attractionService;
            _reviewService = reviewService;
            _travellerService = travellerService;
            LoadData();
            DeleteTripCommand = new DeleteTripCommand(this);
            DeleteDestinationCommand = new DeleteDestinationCommand(this);
            DeleteAttractionCommand = new DeleteAttractionCommand(this);
            DeleteTravellerCommand = new DeleteTravellerCommand(this);
            DeleteReviewCommand = new DeleteReviewCommand(this);

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
