using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Add;
using TravelEditor.Commands.Save;
using TravelEditor.Models;
using TravelEditor.Services;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class DestinationViewModel
    {
        public Destination Destination { get; set; }
        public SaveDestinationCommand SaveDestinationCommand { get; }
        public AddAttractionCommand AddAttractionCommand { get; }

        public DestinationViewModel(Destination destination, IDestinationService destinationService)
        {
            Destination = destination;
            SaveDestinationCommand = new SaveDestinationCommand(this,destinationService);
            AddAttractionCommand = new AddAttractionCommand(this, destinationService);
        }
    }
}
