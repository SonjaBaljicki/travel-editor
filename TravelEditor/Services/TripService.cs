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
        public void AddTrip(Trip trip)
        {
            bool validDates=ValidateDates(trip.StartDate, trip.EndDate);
            if (validDates)
            {
                _tripRepository.AddTrip(trip);
            }
            else
            {
                MessageBox.Show("Dates are not valid");
            }
        }
        //check if dates are in the future
        public bool ValidateDates(DateTime startDate, DateTime endDate)
        {
            DateTime now = DateTime.Now;
            return startDate > now && endDate > now && startDate<endDate;
        }
        //update trip after checking dates
        public void UpdateTrip(Trip trip)
        {
            if(ValidateDates(trip.StartDate, trip.EndDate))
            {
                _tripRepository.UpdateTrip(trip);
            }
            else
            {
                MessageBox.Show("Dates are not valid");
            }
        }
        //delete a trip
        public void DeleteTrip(Trip trip)
        {
            if(!IsTripNow(trip.StartDate, trip.EndDate))
            {
                _tripRepository.DeleteTrip(trip);
            }
            else
            {
                MessageBox.Show("Trip is currently in progress");
            }
        }
        //is the trip currently in progress
        public bool IsTripNow(DateTime startDate, DateTime endDate)
        {
            DateTime now = DateTime.Now;
            return startDate >= now && endDate <= now;
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
    }
}
