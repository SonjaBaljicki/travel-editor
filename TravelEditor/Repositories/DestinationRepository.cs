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
    public class DestinationRepository : IDestinationRepository
    {
        private readonly DatabaseContext _context;

        public DestinationRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<Destination> LoadAll()
        {
            return _context.destinations.ToList();
        }
        public void AddDestination(Destination destination)
        {
            _context.destinations.Add(destination);
            _context.SaveChanges();
        }
    }
}
