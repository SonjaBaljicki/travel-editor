using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands.Save
{
    public class SaveReviewCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public ReviewViewModel viewModel;
        public IReviewService reviewService;
        public ITripService tripService;

        public SaveReviewCommand(ReviewViewModel viewModel, IReviewService reviewService, ITripService tripService)
        {
            this.viewModel = viewModel;
            this.reviewService = reviewService;
            this.tripService = tripService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.Review.ReviewId == 0)
            {
                if(viewModel.SelectedTraveller!=null && viewModel.SelectedTrip != null)
                {
                    viewModel.Review.Traveller = viewModel.SelectedTraveller;
                    viewModel.Review.TravellerId = viewModel.SelectedTraveller.TravellerId;
                    bool success=tripService.AddTripReview(viewModel.SelectedTrip, viewModel.Review);
                    if (success)
                    {
                        Messenger.NotifyDataChanged();
                    }
                    MessageBox.Show("Saving add");
                }
            }
            else
            {
                if (viewModel.SelectedTraveller != null && viewModel.SelectedTrip != null)
                {
                    viewModel.Review.Traveller = viewModel.SelectedTraveller;
                    bool success=reviewService.Update(viewModel.SelectedTrip, viewModel.Review);
                    if (success)
                    {
                        Messenger.NotifyDataChanged();
                        MessageBox.Show("Saving edit");
                    }
                }
            }
        }
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
