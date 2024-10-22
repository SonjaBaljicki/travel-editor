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
    public class SaveTripCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public TripViewModel viewModel;
        public MainViewModel mainViewModel;
        public ITripService tripService;
        public List<Trip> Trips;

        public SaveTripCommand(TripViewModel viewModel, MainViewModel mainViewModel, ITripService tripService)
        {
            this.viewModel = viewModel;
            this.mainViewModel = mainViewModel;
            this.tripService = tripService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.Trip.TripId == 0)
            {
                string name = viewModel.Trip.Name;
                DateTime startDate  = viewModel.Trip.StartDate;
                DateTime endDate  = viewModel.Trip.EndDate;
                string description = viewModel.Trip.Description;
                Destination destination = viewModel.SelectedDestination;
                Trip trip = new Trip(name, startDate, endDate, description, destination.DestinationId, destination,
                    new List<Traveller>(), new List<Review>());
                bool success = tripService.AddTrip(trip);
                if (success)
                {
                    mainViewModel.Trips.Add(trip);
                }
                MessageBox.Show("Saving add");
            }
            else
            {
                Destination destination = viewModel.SelectedDestination;
                viewModel.Trip.DestinationId = destination.DestinationId;
                viewModel.Trip.Destination = destination;
                //tripService.UpdateTrip(viewModel.Trip);
                //saljemo kopiju
                bool success = tripService.UpdateTrip(viewModel.Trip);
                if (success)
                {
                    int index = mainViewModel.Trips.IndexOf(mainViewModel.Trips.First(t => t.TripId == viewModel.Trip.TripId));
                    if (index >= 0)
                    {
                        mainViewModel.Trips[index] = viewModel.Trip;
                    }
                    MessageBox.Show("Saving edit");
                }
            }
        }
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
