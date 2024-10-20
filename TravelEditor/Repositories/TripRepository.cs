﻿using System;
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
                //update travellers and reviews separately
                _context.SaveChanges();
            }
        }
    }
}
