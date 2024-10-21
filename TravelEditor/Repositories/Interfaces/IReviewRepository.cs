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
        List<Review> LoadAll();
        bool TravellerHasReviews(Traveller? selectedTraveller);
    }
}
