﻿using System;
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
            return _context.Destinations.ToList();
        }

        //adds a new destination
        public bool Add(Destination destination)
        {
            _context.Destinations.Add(destination);
            _context.SaveChanges();
            return true;
        }

        //update a destiantion if it exists
        public bool Update(Destination destination)
        {
            if(_context.Destinations.Find(destination.DestinationId)!=null)
            {
                Destination existingDestination = _context.Destinations.Find(destination.DestinationId);
                existingDestination.City = destination.City;
                existingDestination.Country = destination.Country;
                existingDestination.Description = destination.Description;
                existingDestination.Climate = destination.Climate;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        //adding an attraction after a destinations has already been added
        public bool AddDestinationAttraction(Destination destination, Attraction attraction)
        {
            if (_context.Destinations.Find(destination.DestinationId) != null)
            {
                Destination existingDestination = _context.Destinations.Find(destination.DestinationId);
                existingDestination.Attractions.Add(attraction);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        //does the destination have any associated trips
        public bool HasAssociatedTrips(Destination destination)
        {
            var trips = _context.Trips
                                   .Where(t => t.Destination.DestinationId == destination.DestinationId)
                                   .Select(t => t)
                                   .ToList();

            return trips.Count > 0;
        }

        //delete a destination
        public bool Delete(Destination destination)
        {
            _context.Destinations.Remove(destination);
            _context.SaveChanges();
            return true;
        }

        //for an attraction find which destination it belongs to
        public Destination FindDestinationWithAttraction(Attraction attraction)
        {
            Destination destination = _context.Destinations
                                   .Where(d => d.Attractions.Any(a => a.AttractionId==attraction.AttractionId))
                                   .Select(d => d).FirstOrDefault();
            return destination;
        }

        //method for finding one destination based on id
        public bool FindOne(Destination destination)
        {
            return _context.Destinations.Find(destination.DestinationId)!=null;
        }

        //method for finding destinations based on search text that user entered
        public List<Destination> FindDestinations(string searchDestinationsText)
        {
            List<Destination> allDestinations = LoadAll();

            return allDestinations
                .Where(destination =>
                    destination.City.Contains(searchDestinationsText, StringComparison.OrdinalIgnoreCase) ||
                    destination.Country.Contains(searchDestinationsText, StringComparison.OrdinalIgnoreCase) ||
                    destination.Description.Contains(searchDestinationsText, StringComparison.OrdinalIgnoreCase) ||
                    destination.Climate.Contains(searchDestinationsText, StringComparison.OrdinalIgnoreCase) ||

                    destination.Attractions.Any(attraction =>
                        attraction.Name.Contains(searchDestinationsText, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();
        }
    }
}

