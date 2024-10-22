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
    public class ReviewRepository : IReviewRepository
    {
        private readonly DatabaseContext _context;

        public ReviewRepository(DatabaseContext context)
        {
            _context = context;
        }
        public List<Review> LoadAll()
        {
            return _context.reviews.ToList();
        }
        public bool TravellerHasReviews(Traveller? selectedTraveller)
        {
            foreach (Review review in _context.reviews)
            {
                if (review.TravellerId == selectedTraveller.TravellerId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
