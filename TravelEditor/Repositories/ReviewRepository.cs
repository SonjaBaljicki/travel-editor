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

        //loading all reviews
        public List<Review> LoadAll()
        {
            return _context.Reviews.ToList();
        }

        //does this traveller have any reviews
        public bool TravellerHasReviews(Traveller? selectedTraveller)
        {
            foreach (Review review in _context.Reviews)
            {
                if (review.TravellerId == selectedTraveller.TravellerId)
                {
                    return true;
                }
            }
            return false;
        }

        //upating basic review info
        public bool Update(Review review)
        {
            if (_context.Reviews.Find(review.ReviewId) != null)
            {
                Review existingReview = _context.Reviews.Find(review.ReviewId);
                existingReview.Comment = review.Comment;
                existingReview.Date = review.Date;
                existingReview.Rating = review.Rating;
                existingReview.Traveller = review.Traveller;
                existingReview.TravellerId = review.TravellerId;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        //deleting a review
        public bool Delete(Review review)
        {
            Review reviewToDelete = _context.Reviews.Find(review.ReviewId);
            _context.Reviews.Remove(reviewToDelete);
            _context.SaveChanges();
            return true;
        }

        //method for finding reviews based on search text that user entered
        public List<Review> FindReviews(string searchReviewsText)
        {
            List<Review> allReviews = LoadAll();

            return allReviews
                .Where(review =>
                    review.Comment != null && review.Comment.Contains(searchReviewsText, StringComparison.OrdinalIgnoreCase) ||
                    review.Rating.ToString().Contains(searchReviewsText) ||

                    review.Traveller != null && (
                        review.Traveller.FirstName.Contains(searchReviewsText, StringComparison.OrdinalIgnoreCase) ||
                        review.Traveller.LastName.Contains(searchReviewsText, StringComparison.OrdinalIgnoreCase) ||
                        review.Traveller.Email.Contains(searchReviewsText, StringComparison.OrdinalIgnoreCase)
                    )
                )
                .ToList();
        }
    }
}
