using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.Services;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.Add
{
    public class AddReviewCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel mainViewModel { get; }
        public ReviewsGridViewModel reviewsViewModel { get; }

        public IReviewService reviewService; 
        public ITripService tripService; 
        public ITravellerService travellerService; 

        public AddReviewCommand(MainViewModel viewModel, IReviewService reviewService, ITravellerService travellerService, ITripService tripService)
        {
            mainViewModel = viewModel;
            this.reviewService = reviewService;
            this.tripService = tripService;
            this.travellerService = travellerService;
        }
        public AddReviewCommand(ReviewsGridViewModel viewModel, IReviewService reviewService, ITravellerService travellerService, ITripService tripService)
        {
            reviewsViewModel = viewModel;
            this.reviewService = reviewService;
            this.tripService = tripService;
            this.travellerService = travellerService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (mainViewModel != null)
            {
                ReviewView reviewView = new ReviewView(new Review(), reviewService, travellerService, tripService);
                reviewView.Show();
            }
            else if (reviewsViewModel != null)
            {
                ReviewView reviewView = new ReviewView(new Review(),reviewsViewModel.Trip, reviewService, travellerService, tripService);
                reviewView.Show();
            }
        }
    }
}
