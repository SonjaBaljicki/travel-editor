using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.Edit
{
    public class EditReviewCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel mainViewModel { get; }
        public ReviewsGridViewModel reviewsViewModel { get; }

        public IReviewService reviewService;
        public ITripService tripService;
        public ITravellerService travellerService;

        public EditReviewCommand(MainViewModel viewModel, IReviewService reviewService, ITravellerService travellerService, ITripService tripService)
        {
            mainViewModel = viewModel;
            this.reviewService = reviewService;
            this.tripService = tripService;
            this.travellerService = travellerService;
            mainViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedReview))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }
        public EditReviewCommand(ReviewsGridViewModel viewModel, IReviewService reviewService, ITravellerService travellerService, ITripService tripService)
        {
            reviewsViewModel = viewModel;
            this.reviewService = reviewService;
            this.tripService = tripService;
            this.travellerService = travellerService;
            reviewsViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(ReviewsGridViewModel.SelectedReview))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }
        public bool CanExecute(object? parameter)
        {
            return (mainViewModel != null && mainViewModel.SelectedReview != null)
               || (reviewsViewModel != null && reviewsViewModel.SelectedReview != null);
        }

        public void Execute(object? parameter)
        {
            if (mainViewModel != null && mainViewModel.SelectedReview != null)
            {

                ReviewView reviewView = new ReviewView(mainViewModel.SelectedReview, reviewService, travellerService, tripService);
                reviewView.Show();
            }
            else if (reviewsViewModel != null && reviewsViewModel.SelectedReview != null)
            {
                ReviewView reviewView = new ReviewView(reviewsViewModel.SelectedReview, reviewsViewModel.Trip, reviewService, travellerService, tripService);
                reviewView.Show();
            }
        }
    }
}
