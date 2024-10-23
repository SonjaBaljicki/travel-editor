using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Services.Interfaces
{
    public interface ITripService
    {
        bool Add(Trip trip);
        bool AddTripReview(Trip selectedTrip, Review review);
        bool Delete(Trip trip);
        List<Trip> LoadAll();
        bool TravellerHasTrips(Traveller? selectedTraveller);
        bool Update(Trip trip);
        bool ValidateDates(DateTime startDate, DateTime endDate);
        Trip FindTripWithReview(Review review);
        bool HasTripHappened(DateTime startDate, DateTime endDate);

    }
}
