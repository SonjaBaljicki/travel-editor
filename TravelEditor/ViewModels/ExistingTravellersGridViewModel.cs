using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Add;
using TravelEditor.Commands.View;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class ExistingTravellersGridViewModel
    {
        public List<Traveller> Travellers { get; set; }
        public Trip Trip { get; set; }  

        private readonly ITravellerService _travellerService;
        public AddTravellerToTripCommand AddTravellerToTripCommand { get; }

        private Traveller? _selectedTraveller;
        public Traveller? SelectedTraveller
        {
            get { return _selectedTraveller; }
            set
            {
                _selectedTraveller = value;
                OnPropertyChanged(nameof(SelectedTraveller));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ExistingTravellersGridViewModel(Trip trip,ITravellerService travellerService)
        {
            Trip = trip;
            _travellerService = travellerService;
            Travellers = _travellerService.LoadAll();
            AddTravellerToTripCommand = new AddTravellerToTripCommand(this, travellerService);
        }
    }
}
