using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Services.Interfaces
{
    public interface ITravellerService
    {
        void AddTraveller(Traveller traveller);
        void AddTravellerToTrip(Traveller selectedTraveller, Trip trip);
        void DeleteTraveller(Traveller? selectedTraveller);
        void DeleteTravellerFromTrip(Trip trip, Traveller selectedTraveller);
        List<Traveller> LoadAll();
        void UpdateTraveller(Traveller traveller);
    }
}
