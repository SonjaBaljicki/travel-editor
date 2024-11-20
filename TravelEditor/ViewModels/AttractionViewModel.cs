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
        public Destination Destination { get; set; }
        public ObservableCollection<Destination> Destinations { get; set; }
        public SaveAttractionCommand SaveAttractionCommand { get;}


        private readonly IDestinationService _destinationService;
        private readonly IAttractionService _attractionService;

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

        //add and edit of attraction from destination attractions grid view
        public AttractionViewModel(Attraction attraction, Destination destination, IDestinationService destinationService,
           IAttractionService attractionService)
        {
            Attraction = attraction;
            Destination = destination;
            _destinationService = destinationService;
            _attractionService = attractionService;
            SaveAttractionCommand = new SaveAttractionCommand(this, _destinationService, _attractionService);
            //since its opend from destination tab have only that destination in the grid
            Destinations = new ObservableCollection<Destination>{Destination};
            SelectedDestination = Destination;
        }
        //add and edit separately from main view
        public AttractionViewModel(Attraction attraction, IDestinationService destinationService, IAttractionService attractionService)
        {
            Attraction = attraction;
            _destinationService = destinationService;
            _attractionService = attractionService;
            SaveAttractionCommand = new SaveAttractionCommand(this, _destinationService, _attractionService);
            Destinations = new ObservableCollection<Destination>(_destinationService.LoadAll());
            SelectedDestination=_destinationService.FindDestinationWithAttraction(attraction);
        }
    }
}
