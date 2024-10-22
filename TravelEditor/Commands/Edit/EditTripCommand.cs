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
    public class EditTripCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel;
        public ITripService tripService;
        public IDestinationService destinationService;
        public EditTripCommand(MainViewModel viewModel, ITripService tripService, IDestinationService destinationService)
        {
            this.viewModel = viewModel;
            this.tripService = tripService;
            this.destinationService = destinationService;
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
            return viewModel.SelectedTrip!=null;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.SelectedTrip != null) 
            {
                TripView tripView = new TripView(new Trip(viewModel.SelectedTrip), viewModel, tripService, destinationService);
                tripView.Show();
            }
           
        }
    }
}
