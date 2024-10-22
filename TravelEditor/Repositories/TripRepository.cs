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
            return _context.trips.ToList();
        }
        //adding a trip to database
        public void AddTrip(Trip trip)
        {
            _context.trips.Add(trip);
            _context.SaveChanges();
        }
        //update trip basic info
        public void UpdateTrip(Trip trip)
        {
            if (_context.trips.Find(trip.TripId) != null)
            {
                Trip existingTrip = _context.trips.Find(trip.TripId);
                existingTrip.Name =trip.Name;
                existingTrip.StartDate = trip.StartDate;
                existingTrip.EndDate = trip.EndDate;
                existingTrip.Description = trip.Description;
                existingTrip.Destination= trip.Destination;
                existingTrip.DestinationId = trip.DestinationId;
                existingTrip.Travellers = trip.Travellers;
                //update reviews
                _context.SaveChanges();
            }
        }
        //delete a trip
        public void DeleteTrip(Trip trip)
        {
            if (_context.trips.Find(trip.TripId) != null)
            {
                _context.trips.Remove(trip);
                _context.SaveChanges();
            }
        }
        public List<Trip> FindTravellersTrips(Traveller? selectedTraveller)
        {
            return _context.trips
                .Where(trip => trip.Travellers.Any(t => t.TravellerId == selectedTraveller.TravellerId))
                .Select(trip => trip)
                .ToList();
        }
    }
}
