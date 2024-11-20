using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;
using TravelEditor.Views;

namespace TravelEditor.Commands.View
{
    public class ViewReviewsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel;
        public IReviewService reviewService;
        public ITripService tripService;
        public ITravellerService travellerService;

        public ViewReviewsCommand(MainViewModel viewModel, IReviewService reviewService, ITravellerService travellerService, ITripService tripService)
        {
            this.viewModel = viewModel;
            this.reviewService = reviewService;
            this.tripService = tripService;
            this.travellerService = travellerService;
            this.viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MainViewModel.SelectedTrip))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return viewModel.SelectedTrip != null;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.SelectedTrip != null)
            {
                ReviewsGridView reviewsGridView = new ReviewsGridView(viewModel.SelectedTrip, reviewService, travellerService, tripService);
                reviewsGridView.Show();
            }
        }
    }
}
