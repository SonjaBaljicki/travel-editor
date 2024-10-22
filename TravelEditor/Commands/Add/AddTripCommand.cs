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
    public class AddTripCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MainViewModel viewModel;
        public ITripService tripService;
        public IDestinationService destinationService;

        public AddTripCommand(MainViewModel viewModel, ITripService tripService, IDestinationService destinationService)
        {
            this.viewModel = viewModel;
            this.tripService = tripService;
            this.destinationService = destinationService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            TripView tripView = new TripView(new Trip(), viewModel, tripService, destinationService);
            tripView.Show();
        }
    }
}
