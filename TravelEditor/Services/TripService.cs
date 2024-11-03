using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelEditor.Models;
using TravelEditor.Repositories;
using TravelEditor.Repositories.Interfaces;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }
        public List<Trip> LoadAll()
        {
            return _tripRepository.LoadAll();
        }
        //adding a new trip
        public bool Add(Trip trip)
        {
            bool validDates=ValidateDates(trip.StartDate, trip.EndDate);
            if (validDates)
            {
                return _tripRepository.Add(trip);
            }
            else
            {
                MessageBox.Show("Dates are not valid");
            }
            return false;
        }
        //check if dates are in the future
        public bool ValidateDates(DateTime startDate, DateTime endDate)
        {
            DateTime now = DateTime.Now;
            return startDate > now && endDate > now && startDate<endDate;
        }
        //update trip after checking dates
        public bool Update(Trip trip)
        {
            if(ValidateDates(trip.StartDate, trip.EndDate))
            {
                return _tripRepository.Update(trip);
            }
            else
            {
                MessageBox.Show("Dates are not valid");
            }
            return false;
        }
        //delete a trip
        public bool Delete(Trip trip)
        {
            if(!IsTripNow(trip.StartDate, trip.EndDate))
            {
                return _tripRepository.Delete(trip);
            }
            else
            {
                MessageBox.Show("Trip is currently in progress");
            }
            return false;
        }
        //is the trip currently in progress
        public bool IsTripNow(DateTime startDate, DateTime endDate)
        {
            DateTime now = DateTime.Now;
            return startDate <= now && endDate >= now;
        }
        //does traveller have ongoing trips
        public bool TravellerHasTrips(Traveller? selectedTraveller)
        {
            List<Trip> trips = _tripRepository.FindTravellersTrips(selectedTraveller);
            foreach(Trip trip in trips)
            {
                if (IsTripNow(trip.StartDate, trip.EndDate))
                {
                    return true;
                }
            }
            return false;
        }
        //check if trip has happened
        public bool HasTripHappened(DateTime startDate, DateTime endDate)
        {
            DateTime now = DateTime.Now;
            return endDate<now && startDate < endDate;
        }
        //first check if the trip has happened, then check if the traveller who is leaving a review
        //was on that trip
        public bool AddTripReview(Trip trip, Review review)
        {
            if (HasTripHappened(trip.StartDate, trip.EndDate))
            {
                if(trip.Travellers.Any(t=> t.TravellerId == review.Traveller.TravellerId))
                {
                    trip.Reviews.Add(review);
                    return _tripRepository.Update(trip);
                }
                else
                {
                    MessageBox.Show("Traveller wasnt on the trip");
                }
            }
            else
            {
                MessageBox.Show("Trip hasnt happened yet");
            }
            return false;
        }
        public Trip FindTripWithReview(Review review)
        {
            return _tripRepository.FindTripWithReview(review);
        }

        public List<Trip> FindTrips(string searchTripsText)
        {
            return _tripRepository.FindTrips(searchTripsText);
        }
    }
}
