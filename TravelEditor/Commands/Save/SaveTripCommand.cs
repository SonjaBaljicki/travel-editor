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
        public ITripService tripService;

        public SaveTripCommand(TripViewModel viewModel, ITripService tripService)
        {
            this.viewModel = viewModel;
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
                tripService.AddTrip(trip);
                MessageBox.Show("Saving add");
            }
            else
            {
                MessageBox.Show("Saving edit");
            }
        }
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
