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
        private readonly IReviewService _reviewService;

        public TravellerService(ITravellerRepository travellerRepository, ITripService tripService, IReviewService reviewService)
        {
            _travellerRepository = travellerRepository;
            _tripService = tripService;
            _reviewService = reviewService;
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
        public void UpdateTraveller(Traveller traveller)
        {
            Traveller travellerByEmail = _travellerRepository.FindTravellerByEmail(traveller.Email);
            if (travellerByEmail == null || travellerByEmail.TravellerId == traveller.TravellerId)
            {
                _travellerRepository.UpdateTraveller(traveller);
            }
            else
            {
                MessageBox.Show("Email not unique");
            }
        }
        //removes traveller from chosen trip
        public void DeleteTravellerFromTrip(Trip trip, Traveller selectedTraveller)
        {
            trip.Travellers.Remove(selectedTraveller);
            _tripService.UpdateTrip(trip);
        }
        //delete a traveller
        public void DeleteTraveller(Traveller? selectedTraveller)
        {
            //does he have any reviews left? if yes- cant delete
            //does he have any ongoing trips? if yes- cant delete
            if (!_reviewService.TravellerHasReviews(selectedTraveller) 
                && !_tripService.TravellerHasTrips(selectedTraveller))
            {
                _travellerRepository.DeleteTraveller(selectedTraveller);
            }
            else
            {
                MessageBox.Show("Cant delete traveller");
            }
        }
    }
}
