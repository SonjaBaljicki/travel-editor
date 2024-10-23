﻿using System;
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
            return _context.Reviews.ToList();
        }
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
        //u[dating basic review info
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
    }
}
