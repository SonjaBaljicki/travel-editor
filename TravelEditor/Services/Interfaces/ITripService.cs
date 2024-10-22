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
        void AddTrip(Trip trip);
        void AddTripReview(Trip selectedTrip, Review review);
        void DeleteTrip(Trip trip);
        List<Trip> LoadAll();
        bool TravellerHasTrips(Traveller? selectedTraveller);
        void UpdateTrip(Trip trip);
        bool ValidateDates(DateTime startDate, DateTime endDate);


    }
}
