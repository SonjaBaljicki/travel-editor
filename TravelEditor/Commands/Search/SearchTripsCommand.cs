using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;
using TravelEditor.ViewModels;

namespace TravelEditor.Commands.Search
{
    public class SearchTripsCommand : ICommand
    {
        private MainViewModel viewModel;
        private ITripService tripService;
        public event EventHandler? CanExecuteChanged;


        public SearchTripsCommand(MainViewModel viewModel, ITripService tripService)
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
            if (viewModel.SearchTripsText == "")
            {
                viewModel.Trips.Clear();
                foreach (var trip in tripService.LoadAll())
                {
                    viewModel.Trips.Add(trip);
                }
            }
            else
            {
                viewModel.Trips.Clear();
                List<Trip> trips = tripService.FindTrips(viewModel.SearchTripsText);
                foreach (var trip in trips)
                {
                    viewModel.Trips.Add(trip);
                }
            }

        }
    }
}
