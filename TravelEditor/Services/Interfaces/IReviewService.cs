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
        bool Delete(Review selectedReview);
        List<Review> FindReviews(string searchReviewsText);
        List<Review> LoadAll();
        bool TravellerHasReviews(Traveller? selectedTraveller);
        bool Update(Trip selectedTrip, Review review);
    }
}
