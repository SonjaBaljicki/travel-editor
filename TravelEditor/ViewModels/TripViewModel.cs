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
        public TripViewModel(Trip trip, ITripService tripService, IDestinationService destinationService)
        {
            Trip = trip;
            _tripService = tripService;
            _destinationService = destinationService;
            Destinations = new ObservableCollection<Destination>(_destinationService.LoadAll());
            SaveTripCommand = new SaveTripCommand(this, _tripService);
        }
    }
}
