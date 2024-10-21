using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelEditor.Models;
using TravelEditor.Repositories.Interfaces;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.Services
{
    public class TravellerService : ITravellerService
    {
        private readonly ITravellerRepository _travellerRepository;
        private readonly ITripService _tripService;

        public TravellerService(ITravellerRepository travellerRepository, ITripService tripService)
        {
            _travellerRepository = travellerRepository;
            _tripService = tripService;
        }
        //loads all travellers
        public List<Traveller> LoadAll()
        {
            return _travellerRepository.LoadAll();
        }
        //add new traveller
        public void AddTraveller(Traveller traveller)
        {
            _travellerRepository.AddTraveller(traveller);
        }
        //adds a traveller to trip then updates it
        public void AddTravellerToTrip(Traveller selectedTraveller, Trip trip)
        {
            if (!trip.Travellers.Any(t => t.TravellerId == selectedTraveller.TravellerId))
            {
                trip.Travellers.Add(selectedTraveller);
                _tripService.UpdateTrip(trip);
            }
            else
            {
                MessageBox.Show("Already has this traveller");
            }
         
        }
    }
}
