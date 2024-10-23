using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Repositories.Interfaces
{
    public interface ITripRepository
    {
        bool Add(Trip trip);
        bool Delete(Trip trip);
        List<Trip> FindTravellersTrips(Traveller? selectedTraveller);
        Trip FindTripWithReview(Review review);
        List<Trip> LoadAll();
        bool Update(Trip trip);
    }
}
