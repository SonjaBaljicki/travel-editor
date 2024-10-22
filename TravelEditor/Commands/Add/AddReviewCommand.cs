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

namespace TravelEditor.Commands.Add
{
    public class AddReviewCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel { get; }
        public IReviewService reviewService; 
        public ITripService tripService; 
        public ITravellerService travellerService; 

        public AddReviewCommand(MainViewModel viewModel, IReviewService reviewService, ITravellerService travellerService, ITripService tripService)
        {
            this.viewModel = viewModel;
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
            ReviewView reviewView = new ReviewView(new Review(), reviewService, travellerService, tripService);
            reviewView.Show();
        }
    }
}
