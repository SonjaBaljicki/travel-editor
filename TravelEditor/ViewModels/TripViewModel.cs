using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Save;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class TripViewModel
    {
        public Trip Trip { get; set; }
        public MainViewModel MainViewModel { get; set; }
        public ObservableCollection<Destination> Destinations { get; set; }
        public SaveTripCommand SaveTripCommand { get; }

        private readonly ITripService _tripService;
        private readonly IDestinationService _destinationService;

        private Destination? _selectedDestination;
        public Destination? SelectedDestination
        {
            get { return _selectedDestination; }
            set
            {
                _selectedDestination = value;
                OnPropertyChanged(nameof(SelectedDestination));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TripViewModel(Trip trip, MainViewModel mainViewModel, ITripService tripService, IDestinationService destinationService)
        {
            Trip = trip;
            MainViewModel = mainViewModel;
            _tripService = tripService;
            _destinationService = destinationService;
            Destinations = new ObservableCollection<Destination>(_destinationService.LoadAll());           
            //if editing initiali select the destination
            if (Trip.TripId != 0)
            {
                SelectedDestination = Trip.Destination;
            }
            SaveTripCommand = new SaveTripCommand(this,MainViewModel,_tripService);
        }
    }
}
