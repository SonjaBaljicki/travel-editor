using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Add;
using TravelEditor.Commands.Delete;
using TravelEditor.Commands.Edit;
using TravelEditor.Commands.View;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class TravellersGridViewModel
    {
        public ObservableCollection<Traveller> Travellers { get; set; }
        public Trip Trip { get; set; }
        public ViewTravellersCommand ViewTravellersCommand { get;}
        public EditTravellerCommand EditTravellerCommand { get; }
        public DeleteTravellerCommand DeleteTravellerCommand { get; }

        private readonly ITravellerService _travellerService;

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

        public TravellersGridViewModel(Trip trip, ITravellerService travellerService)
        {
            Travellers = new ObservableCollection<Traveller>(trip.Travellers);
            Trip = trip;
            Messenger.DataChanged+= LoadData;
            _travellerService = travellerService;
            ViewTravellersCommand = new ViewTravellersCommand(this, _travellerService);
            EditTravellerCommand = new EditTravellerCommand(this, _travellerService);
            DeleteTravellerCommand = new DeleteTravellerCommand(this, _travellerService);
        }

        private void LoadData()
        {
            Travellers.Clear();
            foreach (Traveller traveller in Trip.Travellers)
            {
                Travellers.Add(traveller);
            }
        }
        ~TravellersGridViewModel()
        {
            Messenger.DataChanged -= LoadData;
        }
    }
}
