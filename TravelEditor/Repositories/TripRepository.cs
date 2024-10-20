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
    }
}
