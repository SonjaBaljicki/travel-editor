using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;
using TravelEditor.Repositories.Interfaces;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public List<Review> LoadAll()
        {
            return _reviewRepository.LoadAll();
        }

        public bool TravellerHasReviews(Traveller? selectedTraveller)
        {
            return _reviewRepository.TravellerHasReviews(selectedTraveller);
        }
    }
}
