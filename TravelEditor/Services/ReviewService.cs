﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelEditor.Models;
using TravelEditor.Repositories;
using TravelEditor.Repositories.Interfaces;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ITripService _tripService;

        public ReviewService(IReviewRepository reviewRepository, ITripService tripService)
        {
            _reviewRepository = reviewRepository;
            _tripService = tripService;
        }

        public List<Review> LoadAll()
        {
            return _reviewRepository.LoadAll();
        }

        //method checkes if traveller has any reviews
        public bool TravellerHasReviews(Traveller? selectedTraveller)
        {
            return _reviewRepository.TravellerHasReviews(selectedTraveller);
        }

        //updating a review, different scenarios if trip has changed for the review
        public bool Update(Trip trip, Review review)
        {
            //trip stayed the same
            if (trip.Reviews.Any(r => r.ReviewId == review.ReviewId))
            {
                //update basic info for the review if traveller was on that trip
                if (trip.Travellers.Any(t => t.TravellerId == review.Traveller.TravellerId))
                {
                    _reviewRepository.Update(review);
                    return true;
                }
                else
                {
                    MessageBox.Show("Invalid traveller selected");
                }
            }
            //trip has changed
            else
            {
                if (_tripService.HasTripHappened(trip.StartDate, trip.EndDate))
                {
                    if (trip.Travellers.Any(t => t.TravellerId == review.Traveller.TravellerId))
                    {
                        _reviewRepository.Delete(review);
                        _tripService.AddTripReview(trip, review);
                        return true;
                    }
                }
            }
            return false;
        }

        //deleting a review
        public bool Delete(Review review)
        {
            return _reviewRepository.Delete(review);
        }

        //method for finding reviews based on search text that user entered
        public List<Review> FindReviews(string searchReviewsText)
        {
            return _reviewRepository.FindReviews(searchReviewsText);
        }
    }
}
