using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Add;
using TravelEditor.Commands.Delete;
using TravelEditor.Commands.Edit;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class ReviewsGridViewModel
    {
        public ObservableCollection<Review> Reviews { get; set; }
        public Trip Trip { get; set; }
        public AddReviewCommand AddReviewCommand { get; }
        public EditReviewCommand EditReviewCommand { get; }
        public DeleteReviewCommand DeleteReviewCommand { get; }

        private readonly IReviewService _reviewService;
        private readonly ITravellerService _travellerService;
        private readonly ITripService _tripService;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public ReviewsGridViewModel(Trip trip,IReviewService reviewService, ITravellerService travellerService, ITripService tripService)
        {
            Trip = trip;
            Reviews = new ObservableCollection<Review>(trip.Reviews);
            _reviewService = reviewService;
            _travellerService = travellerService;
            _tripService = tripService;
            AddReviewCommand = new AddReviewCommand(this, _reviewService, _travellerService, _tripService);
            EditReviewCommand = new EditReviewCommand(this, _reviewService, _travellerService, _tripService);
            DeleteReviewCommand = new DeleteReviewCommand(this, _reviewService);
        }
    }
}
