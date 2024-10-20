using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Save;
using TravelEditor.Models;
using TravelEditor.Services;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class AttractionViewModel
    {
        public Attraction Attraction { get; set; }
        public SaveAttractionCommand SaveAttractionCommand { get;}
        public DestinationViewModel DestinationViewModel { get; }
        public ObservableCollection<Destination> Destinations { get; set; }


        private Destination? _selectedDestination;
        private readonly IDestinationService _destinationService;
        private readonly IAttractionService _attractionService;

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

        public AttractionViewModel(Attraction attraction, DestinationViewModel destinationViewModel, IDestinationService destinationService,
            IAttractionService attractionService)
        {
            Attraction = attraction;
            DestinationViewModel = destinationViewModel;
            _destinationService = destinationService;
            _attractionService = attractionService;
            SaveAttractionCommand = new SaveAttractionCommand(this,_destinationService,_attractionService);
            //if adding attraction, view all destinations
            if (attraction.AttractionId == 0) 
            {
                Destinations = new ObservableCollection<Destination>(_destinationService.LoadAll());
            }

        }
    }
}
