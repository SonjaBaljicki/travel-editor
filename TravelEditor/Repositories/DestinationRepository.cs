using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TravelEditor.Database;
using TravelEditor.Models;
using TravelEditor.Repositories.Interfaces;

namespace TravelEditor.Repositories
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly DatabaseContext _context;

        public DestinationRepository(DatabaseContext context)
        {
            _context = context;
        }
        //loads all destinations from the database
        public List<Destination> LoadAll()
        {
            return _context.destinations.ToList();
        }
        //adds a new destination
        public void AddDestination(Destination destination)
        {
            _context.destinations.Add(destination);
            _context.SaveChanges();
        }
        //update a destiantion if it exists
        public void UpdateDestination(Destination destination)
        {
            if(_context.destinations.Find(destination.DestinationId)!=null)
            {
                Destination existingDestination = _context.destinations.Find(destination.DestinationId);
                existingDestination.City = destination.City;
                existingDestination.Country = destination.Country;
                existingDestination.Description = destination.Description;
                existingDestination.Climate = destination.Climate;
                _context.SaveChanges();
            }
        }
        //adding an attraction after a destinations has already been added
        public void AddDestinationAttractions(Destination destination, Attraction attraction)
        {
            if (_context.destinations.Find(destination.DestinationId) != null)
            {
                Destination existingDestination = _context.destinations.Find(destination.DestinationId);
                existingDestination.Attractions.Add(attraction);
                _context.SaveChanges();
            }
        }
        //does the destination have any associated trips
        public bool HasAssociatedTrips(Destination destination)
        {
            var trips = _context.trips
                                   .Where(t => t.Destination.DestinationId == destination.DestinationId)
                                   .Select(t => t)
                                   .ToList();

            return trips.Count > 0;
        }
        //delete a destination
        public void Delete(Destination destination)
        {
            _context.destinations.Remove(destination);
            _context.SaveChanges();
        }
        //for an attraction find which destination it belongs to
        public Destination FindDestinationWithAttraction(Attraction attraction)
        {
            Destination destination = _context.destinations
                                   .Where(d => d.Attractions.Contains(attraction))
                                   .Select(d => d) as Destination;
            return destination;
        }

        public bool FindOne(Destination destination)
        {
            return _context.destinations.Find(destination.DestinationId)!=null;
        }
    }
}

