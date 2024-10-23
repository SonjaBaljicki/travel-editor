using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        bool Delete(Review review);
        List<Review> LoadAll();
        bool TravellerHasReviews(Traveller? selectedTraveller);
        bool Update(Review review);
    }
}
