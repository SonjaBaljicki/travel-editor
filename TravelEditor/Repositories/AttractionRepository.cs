using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Database;
using TravelEditor.Models;
using TravelEditor.Repositories.Interfaces;

namespace TravelEditor.Repositories
{
    public class AttractionRepository : IAttractionRepository
    {
        private readonly DatabaseContext _context;

        public AttractionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<Attraction> LoadAll()
        {
            return _context.Attractions.ToList();
        }

        //update an attraction
        public bool Update(Attraction attraction)
        {
            if (_context.Attractions.Find(attraction.AttractionId) != null)
            {
                Attraction existingAttraction = _context.Attractions.Find(attraction.AttractionId);
                existingAttraction.Name = attraction.Name;
                existingAttraction.Description = attraction.Description;
                existingAttraction.Location = attraction.Location;
                existingAttraction.Price = attraction.Price;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        //deleting a existing attraction
        public bool Delete(Attraction attraction)
        {
            Attraction attractionToDelete = _context.Attractions.Find(attraction.AttractionId);
            if(attractionToDelete != null)
            {
                _context.Attractions.Remove(attractionToDelete);
                _context.SaveChanges();
                return true;
            }
            return false;
        
        }

        //method for getting one attraction based on id
        public bool FindOne(Attraction attraction)
        {
            return _context.Attractions.Find(attraction.AttractionId) != null;
        }

        //method for finding attractions based on search text that user entered
        public List<Attraction> FindAttractions(string searchAttractionsText)
        {
            List<Attraction> allAttractions = LoadAll();

            return allAttractions
                .Where(attraction =>
                    attraction.Name.Contains(searchAttractionsText, StringComparison.OrdinalIgnoreCase) ||
                    attraction.Description.Contains(searchAttractionsText, StringComparison.OrdinalIgnoreCase) ||
                    attraction.Location.Contains(searchAttractionsText, StringComparison.OrdinalIgnoreCase) ||
                    attraction.Price.ToString().Contains(searchAttractionsText)
                )
                .ToList();
        }
    }
}
