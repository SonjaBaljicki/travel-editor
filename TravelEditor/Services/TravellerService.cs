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
        public bool AddTraveller(Traveller traveller)
        {
            return _travellerRepository.AddTraveller(traveller);
        }
        //adds a traveller to trip then updates it
        public bool AddTravellerToTrip(Traveller selectedTraveller, Trip trip)
        {
            if (!trip.Travellers.Any(t => t.TravellerId == selectedTraveller.TravellerId)
                && _tripService.ValidateDates(trip.StartDate,trip.EndDate))
            {
                trip.Travellers.Add(selectedTraveller);
                return _tripService.UpdateTrip(trip);
            }
            else
            {
                MessageBox.Show("Already has this traveller or dates are not valid");
            }
            return false;

        }
        public bool UpdateTraveller(Traveller traveller)
        {
            Traveller travellerByEmail = _travellerRepository.FindTravellerByEmail(traveller.Email);
            if (travellerByEmail == null || travellerByEmail.TravellerId == traveller.TravellerId)
            {
                return _travellerRepository.UpdateTraveller(traveller);
            }
            else
            {
                MessageBox.Show("Email not unique");
            }
            return false;
        }
        //removes traveller from chosen trip
        public bool DeleteTravellerFromTrip(Trip trip, Traveller selectedTraveller)
        {
            if(_tripService.ValidateDates(trip.StartDate, trip.EndDate))
            {
                trip.Travellers.Remove(selectedTraveller);
                return _tripService.UpdateTrip(trip);
            }
            else
            {
                MessageBox.Show("Can't delete");
            }
            return false;
        }
        //delete a traveller
        public bool DeleteTraveller(Traveller? selectedTraveller)
        {
            //does he have any reviews left? if yes- cant delete
            //does he have any ongoing trips? if yes- cant delete
            if (!_reviewService.TravellerHasReviews(selectedTraveller) 
                && !_tripService.TravellerHasTrips(selectedTraveller))
            {
               return  _travellerRepository.DeleteTraveller(selectedTraveller);
            }
            else
            {
                MessageBox.Show("Cant delete traveller");
            }
            return false;
        }
    }
}
