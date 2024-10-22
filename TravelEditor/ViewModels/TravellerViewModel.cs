using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Save;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class TravellerViewModel
    {
        public Traveller Traveller { get; set; }
        public SaveTravellerCommand SaveTravellerCommand { get; }
        private readonly ITravellerService _travellerService;


        public TravellerViewModel(Traveller traveller, ITravellerService travellerService)
        {
            Traveller = traveller;
            _travellerService = travellerService;
            SaveTravellerCommand = new SaveTravellerCommand(this, travellerService);
        }
    }
}
