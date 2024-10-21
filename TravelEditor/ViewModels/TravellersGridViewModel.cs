using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Add;
using TravelEditor.Commands.View;
using TravelEditor.Models;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.ViewModels
{
    public class TravellersGridViewModel
    {
        public List<Traveller> Travellers { get; set; }
        public Trip Trip { get; set; }
        public ViewTravellersCommand ViewTravellersCommand { get;}
        private readonly ITravellerService _travellerService;

        public TravellersGridViewModel(Trip trip, ITravellerService travellerService)
        {
            Trip = trip;
            Travellers = trip.Travellers;
            _travellerService = travellerService;
            ViewTravellersCommand = new ViewTravellersCommand(this, _travellerService);
        }

    }
}
