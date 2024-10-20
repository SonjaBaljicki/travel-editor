using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Commands.Add;
using TravelEditor.Commands.Delete;
using TravelEditor.Commands.Edit;
using TravelEditor.Commands.View;
using TravelEditor.Database;
using TravelEditor.Models;
using TravelEditor.Services;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class MainViewModel
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

        //grid views lists
        public ObservableCollection<Trip> Trips { get; set; }
        public ObservableCollection<Destination> Destinations { get; set; }
        public ObservableCollection<Attraction> Attractions { get; set; }
        public ObservableCollection<Review> Reviews { get; set; }
        public ObservableCollection<Traveller> Travellers { get; set; }


        //trips commands
        public AddTripCommand AddTripCommand {get;}
        public EditTripCommand EditTripCommand { get; }
        //public DeleteTripCommand DeleteTripCommand { get; }
        public ViewDestinationCommand ViewDestinationCommand { get; }
        public ViewTravellersCommand ViewTravellersCommand { get; }
        public ViewReviewsCommand ViewReviewsCommand { get; }

        //destinations commands
        public AddDestinationCommand AddDestinationCommand { get; }
        public EditDestinationCommand EditDestinationCommand { get; }
        public DeleteDestinationCommand DeleteDestinationCommand { get; }
        public ViewAttractionsCommand ViewAttractionsCommand { get; }

        //attractions commands
        public AddAttractionCommand AddAttractionCommand { get; }
        public EditAttractionCommand EditAttractionCommand { get; }
        public DeleteAttractionCommand DeleteAttractionCommand { get; set; }

        ////travellers commands
        public AddTravellerCommand AddTravellerCommand { get; }
        public EditTravellerCommand EditTravellerCommand { get; }
        //public DeleteTravellerCommand DeleteTravellerCommand { get; }

        ////reviews command
        public AddReviewCommand AddReviewCommand { get; }
        public EditReviewCommand EditReviewCommand { get; }
        //public DeleteReviewCommand DeleteReviewCommand { get; }
        public ViewTravellerCommand ViewTravellerCommand { get; }


        public MainViewModel(ITripService tripService, IDestinationService destinationService, IAttractionService attractionService, IReviewService reviewService, ITravellerService travellerService)
        {
            _tripService = tripService;
            _destinationService = destinationService;
            _attractionService = attractionService;
            _reviewService = reviewService;
            _travellerService = travellerService;

            LoadData();
           
            AddTripCommand = new AddTripCommand(this, _tripService, _destinationService);
            EditTripCommand = new EditTripCommand(this);
            //DeleteTripCommand = new DeleteTripCommand(this);
            ViewDestinationCommand = new ViewDestinationCommand(this, _destinationService);
            ViewTravellersCommand = new ViewTravellersCommand(this);
            ViewReviewsCommand = new ViewReviewsCommand(this);

            AddDestinationCommand = new AddDestinationCommand(this, _destinationService);
            EditDestinationCommand = new EditDestinationCommand(this, _destinationService);
            DeleteDestinationCommand = new DeleteDestinationCommand(this, _destinationService);
            ViewAttractionsCommand = new ViewAttractionsCommand(this, _destinationService, _attractionService);

            AddAttractionCommand = new AddAttractionCommand(this, _destinationService, _attractionService);
            EditAttractionCommand = new EditAttractionCommand(this, _destinationService, _attractionService);
            DeleteAttractionCommand = new DeleteAttractionCommand(this,_attractionService);

            AddTravellerCommand = new AddTravellerCommand(this);
            EditTravellerCommand = new EditTravellerCommand(this);
            //DeleteTravellerCommand = new DeleteTravellerCommand(this);

            AddReviewCommand = new AddReviewCommand(this);
            EditReviewCommand = new EditReviewCommand(this);
            //DeleteReviewCommand = new DeleteReviewCommand(this);
            ViewTravellerCommand = new ViewTravellerCommand(this);

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
