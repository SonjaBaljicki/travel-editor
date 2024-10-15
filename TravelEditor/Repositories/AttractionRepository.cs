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
            return _context.attractions.ToList();
        }
    }
}
