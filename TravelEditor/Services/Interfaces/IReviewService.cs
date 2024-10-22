using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Services.Interfaces
{
    public interface IReviewService
    {
        void DeleteReview(Review selectedReview);
        List<Review> LoadAll();
        bool TravellerHasReviews(Traveller? selectedTraveller);
        bool UpdateReview(Trip selectedTrip, Review review);
    }
}
