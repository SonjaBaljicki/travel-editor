using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Delete;
using TravelEditor.Commands.Edit;
using TravelEditor.Models;
using TravelEditor.Services;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class AttractionsGridViewModel
    {
        public ObservableCollection<Attraction> Attractions { get; set; }
        public EditAttractionCommand EditAttractionCommand { get; }
        public DeleteAttractionCommand DeleteAttractionCommand { get; set; }

        private Attraction? _selectedAttraction;
        public Attraction? SelectedAttraction
        {
            get { return _selectedAttraction; }
            set
            {
                _selectedAttraction = value;
                OnPropertyChanged(nameof(SelectedAttraction));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AttractionsGridViewModel(List<Attraction> attractions, IDestinationService destinationService, IAttractionService attractionService)
        {
            Attractions = new ObservableCollection<Attraction>(attractions);
            EditAttractionCommand = new EditAttractionCommand(this, destinationService, attractionService);
            DeleteAttractionCommand = new DeleteAttractionCommand(this, attractionService);
        }
    }
}
