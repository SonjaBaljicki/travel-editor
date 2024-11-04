using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Database;
using TravelEditor.Models;
using TravelEditor.Repositories.Interfaces;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.Repositories
{
    public class TravellerRepository : ITravellerRepository
    {
        private readonly DatabaseContext _context;

        public TravellerRepository(DatabaseContext context)
        {
            _context = context;
        }

        //loads all travellers
        public List<Traveller> LoadAll()
        {
            return _context.Travellers.ToList();
        }

        //adds a new traveller
        public bool Add(Traveller traveller)
        {
            _context.Travellers.Add(traveller);
            _context.SaveChanges();
            return true;
        }

        //updates traveller info
        public bool Update(Traveller traveller)
        {
            if (_context.Travellers.Find(traveller.TravellerId) != null)
            {
                Traveller existingTraveller=_context.Travellers.Find(traveller.TravellerId);
                existingTraveller.FirstName = traveller.FirstName;
                existingTraveller.LastName = traveller.LastName;
                existingTraveller.Email = traveller.Email;
                existingTraveller.PhoneNumber = traveller.PhoneNumber;
                existingTraveller.Age = traveller.Age;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        //finidng a traveller by email
        public Traveller FindTravellerByEmail(string email)
        {
            return _context.Travellers.FirstOrDefault(t => t.Email == email);
        }

        //deleting a traveller
        public bool Delete(Traveller? selectedTraveller)
        {
            _context.Travellers.Remove(selectedTraveller);
            _context.SaveChanges();
            return true;
        }

        //method for finding travellers based on search text that user entered
        public List<Traveller> FindTravellers(string searchTravellersText)
        {
            List<Traveller> allTravellers = LoadAll();

            return allTravellers
                .Where(traveller =>
                    traveller.FirstName.Contains(searchTravellersText, StringComparison.OrdinalIgnoreCase) ||
                    traveller.LastName.Contains(searchTravellersText, StringComparison.OrdinalIgnoreCase) ||
                    traveller.Email.Contains(searchTravellersText, StringComparison.OrdinalIgnoreCase) ||
                    traveller.PhoneNumber.Contains(searchTravellersText, StringComparison.OrdinalIgnoreCase) ||
                    traveller.Age.ToString().Contains(searchTravellersText)
                )
                .ToList();
        }
    }
}
