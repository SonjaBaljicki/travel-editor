using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Add;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class ReviewsGridViewModel
    {
        public ObservableCollection<Review> Reviews { get; set; }
        public Trip Trip { get; set; }
        public AddReviewCommand AddReviewCommand { get; }

        private readonly IReviewService _reviewService;
        private readonly ITravellerService _travellerService;
        private readonly ITripService _tripService;


        public ReviewsGridViewModel(Trip trip,IReviewService reviewService, ITravellerService travellerService, ITripService tripService)
        {
            Trip = trip;
            Reviews = new ObservableCollection<Review>(trip.Reviews);
            _reviewService = reviewService;
            _travellerService = travellerService;
            _tripService = tripService;
            AddReviewCommand = new AddReviewCommand(this, _reviewService, _travellerService, _tripService);
        }
    }
}
