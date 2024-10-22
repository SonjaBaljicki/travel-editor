using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Save;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class ReviewViewModel
    {
        public Review Review { get; set; }
        public Trip Trip { get; set; }
        public ObservableCollection<Trip> Trips { get; set; }
        public ObservableCollection<Traveller> Travellers { get; set; }
        public SaveReviewCommand SaveReviewCommand { get; }

        private readonly IReviewService _reviewService;
        private readonly ITripService _tripService;
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
  
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //add and edit review from main view
        public ReviewViewModel(Review review, IReviewService reviewService, ITravellerService travellerService, ITripService tripService)
        {
            Review = review;
            _reviewService = reviewService;
            _tripService = tripService;
            _travellerService = travellerService;
            SaveReviewCommand = new SaveReviewCommand(this, _reviewService, _tripService);
            Trips = new ObservableCollection<Trip>(_tripService.LoadAll());
            LoadTravellers();
            if (Review.ReviewId != 0)  //if editing
            {
                SelectedTraveller = Review.Traveller;
                //SelectedTrip = tripService.FindTripForReview();
            }
        }
        //add and edit review from trips reviews grid
        public ReviewViewModel(Review review,Trip trip, IReviewService reviewService, ITravellerService travellerService, ITripService tripService)
        {
            Review = review;
            Trip = trip;
            _reviewService = reviewService;
            _tripService = tripService;
            _travellerService = travellerService;
            SaveReviewCommand = new SaveReviewCommand(this, _reviewService, _tripService);
            Trips = new ObservableCollection<Trip> { Trip };
            SelectedTrip = Trip;
            LoadTravellers();
            if (Review.ReviewId != 0) //ako se edituje
            {
                SelectedTraveller = Review.Traveller;
            }
        }

        private void LoadTravellers()
        {
            Travellers = new ObservableCollection<Traveller>(_travellerService.LoadAll());
        }
    }
}
