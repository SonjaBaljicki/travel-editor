using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Database;
using TravelEditor.Models;
using TravelEditor.Repositories.Interfaces;

namespace TravelEditor.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly DatabaseContext _context;

        public TripRepository(DatabaseContext context)
        {
            _context = context;
        }
        //loads all trips from the database
        public List<Trip> LoadAll()
        {
            return _context.Trips.ToList();
        }
        //adding a trip to database
        public bool Add(Trip trip)
        {
            _context.Trips.Add(trip);
            _context.SaveChanges();
            return true;
        }
        //update trip basic info
        public bool Update(Trip trip)
        {
            if (_context.Trips.Find(trip.TripId) != null)
            {
                Trip existingTrip = _context.Trips.Find(trip.TripId);
                existingTrip.Name =trip.Name;
                existingTrip.StartDate = trip.StartDate;
                existingTrip.EndDate = trip.EndDate;
                existingTrip.Description = trip.Description;
                existingTrip.Destination= trip.Destination;
                existingTrip.DestinationId = trip.DestinationId;
                existingTrip.Travellers = trip.Travellers;
                existingTrip.Reviews = trip.Reviews;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        //delete a trip
        public bool Delete(Trip trip)
        {
            if (_context.Trips.Find(trip.TripId) != null)
            {
                _context.Trips.Remove(trip);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public List<Trip> FindTravellersTrips(Traveller? selectedTraveller)
        {
            return _context.Trips
                .Where(trip => trip.Travellers.Any(t => t.TravellerId == selectedTraveller.TravellerId))
                .Select(trip => trip)
                .ToList();
        }

        public Trip FindTripWithReview(Review review)
        {
            Trip trip = _context.Trips
                                  .Where(t => t.Reviews.Any(r => r.ReviewId == review.ReviewId))
                                  .Select(d => d).FirstOrDefault();
            return trip;
        }
        public List<Trip> FindTrips(string searchTripsText)
        {
            var allTrips = LoadAll();

            return allTrips
                .Where(trip =>
                    trip.Name.Contains(searchTripsText, StringComparison.OrdinalIgnoreCase) ||
                    trip.Description.Contains(searchTripsText, StringComparison.OrdinalIgnoreCase) ||
                    trip.StartDate.ToString("yyyy-MM-dd").Contains(searchTripsText) ||
                    trip.EndDate.ToString("yyyy-MM-dd").Contains(searchTripsText) ||

                    (trip.Destination != null && (
                        trip.Destination.City.Contains(searchTripsText, StringComparison.OrdinalIgnoreCase) ||
                        trip.Destination.Country.Contains(searchTripsText, StringComparison.OrdinalIgnoreCase))) ||

                    trip.Travellers.Any(traveller =>
                        traveller.FirstName.Contains(searchTripsText, StringComparison.OrdinalIgnoreCase) ||
                        traveller.LastName.Contains(searchTripsText, StringComparison.OrdinalIgnoreCase) ||
                        traveller.Email.Contains(searchTripsText, StringComparison.OrdinalIgnoreCase)) ||

                    trip.Reviews.Any(review =>
                        review.Rating.ToString().Contains(searchTripsText) ||
                        (review.Comment != null && review.Comment.Contains(searchTripsText, StringComparison.OrdinalIgnoreCase))
                    )
                )
                .ToList();
        }

    }
}
