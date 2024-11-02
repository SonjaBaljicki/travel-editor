using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Commands;
using TravelEditor.Commands.Add;
using TravelEditor.Commands.Delete;
using TravelEditor.Commands.Edit;
using TravelEditor.Commands.View;
using TravelEditor.Database;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Export.Service;
using TravelEditor.Export_Import.Iterfaces;
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
        private readonly IExportService _exportService;
        private readonly IImportService _importService;

        private string _fileName="";
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

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
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //grid views lists
        public ObservableCollection<Trip> Trips { get; set; }=new ObservableCollection<Trip>();
        public ObservableCollection<Destination> Destinations { get; set; }= new ObservableCollection<Destination>();
        public ObservableCollection<Attraction> Attractions { get; set; } = new ObservableCollection<Attraction>();
        public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();
        public ObservableCollection<Traveller> Travellers { get; set; } = new ObservableCollection<Traveller>();


        //trips commands
        public AddTripCommand AddTripCommand {get;}
        public EditTripCommand EditTripCommand { get; }
        public DeleteTripCommand DeleteTripCommand { get; }
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

        //travellers commands
        public AddTravellerCommand AddTravellerCommand { get; }
        public EditTravellerCommand EditTravellerCommand { get; }
        public DeleteTravellerCommand DeleteTravellerCommand { get; }

        //reviews command
        public AddReviewCommand AddReviewCommand { get; }
        public EditReviewCommand EditReviewCommand { get; }
        public DeleteReviewCommand DeleteReviewCommand { get; }
        public ViewTravellerCommand ViewTravellerCommand { get; }

        //export and import commmands
        public ExportDataCommand ExportDataCommand { get; }
        public ImportDataCommand ImportDataCommand { get; }

        public MainViewModel(ITripService tripService, IDestinationService destinationService, IAttractionService attractionService,
            IReviewService reviewService, ITravellerService travellerService, IExportService exportService, IImportService importService)
        {
            _tripService = tripService;
            _destinationService = destinationService;
            _attractionService = attractionService;
            _reviewService = reviewService;
            _travellerService = travellerService;
            _exportService = exportService;
            _importService = importService;

            LoadData();
            Messenger.DataChanged += LoadData;

            AddTripCommand = new AddTripCommand(this, _tripService, _destinationService);
            EditTripCommand = new EditTripCommand(this, _tripService, _destinationService);
            DeleteTripCommand = new DeleteTripCommand(this, _tripService);
            ViewDestinationCommand = new ViewDestinationCommand(this, _destinationService);
            ViewTravellersCommand = new ViewTravellersCommand(this, _travellerService);
            ViewReviewsCommand = new ViewReviewsCommand(this, _reviewService, _travellerService, _tripService);

            AddDestinationCommand = new AddDestinationCommand(this, _destinationService);
            EditDestinationCommand = new EditDestinationCommand(this, _destinationService);
            DeleteDestinationCommand = new DeleteDestinationCommand(this, _destinationService);
            ViewAttractionsCommand = new ViewAttractionsCommand(this, _destinationService, _attractionService);

            AddAttractionCommand = new AddAttractionCommand(this, _destinationService, _attractionService);
            EditAttractionCommand = new EditAttractionCommand(this, _destinationService, _attractionService);
            DeleteAttractionCommand = new DeleteAttractionCommand(this,_attractionService);

            AddTravellerCommand = new AddTravellerCommand(this, _travellerService);
            EditTravellerCommand = new EditTravellerCommand(this, _travellerService);
            DeleteTravellerCommand = new DeleteTravellerCommand(this,_travellerService);

            AddReviewCommand = new AddReviewCommand(this, _reviewService, _travellerService, _tripService);
            EditReviewCommand = new EditReviewCommand(this, _reviewService, _travellerService,_tripService);
            DeleteReviewCommand = new DeleteReviewCommand(this, _reviewService);
            ViewTravellerCommand = new ViewTravellerCommand(this, _travellerService);

            ExportDataCommand = new ExportDataCommand(_exportService,this);
            ImportDataCommand = new ImportDataCommand(_importService,this);
        }
        ~MainViewModel()
        {
            Messenger.DataChanged -= LoadData;
        }

        public void LoadData()
        {
            Trips.Clear();
            Destinations.Clear();
            Attractions.Clear();
            Reviews.Clear();
            Travellers.Clear();

            foreach (var trip in _tripService.LoadAll())
            {
                Trips.Add(trip);
            }

            foreach (var destination in _destinationService.LoadAll())
            {
                Destinations.Add(destination);
            }

            foreach (var attraction in _attractionService.LoadAll())
            {
                Attractions.Add(attraction);
            }

            foreach (var review in _reviewService.LoadAll())
            {
                Reviews.Add(review);
            }

            foreach (var traveller in _travellerService.LoadAll())
            {
                Travellers.Add(traveller);
            }
        }
    }
}
