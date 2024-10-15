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
        public List<Traveller> LoadAll()
        {
            return _context.travellers.ToList();
        }
    }
}
