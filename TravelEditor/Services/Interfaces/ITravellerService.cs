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
        bool AddTraveller(Traveller traveller);
        bool AddTravellerToTrip(Traveller selectedTraveller, Trip trip);
        bool DeleteTraveller(Traveller? selectedTraveller);
        bool DeleteTravellerFromTrip(Trip trip, Traveller selectedTraveller);
        List<Traveller> LoadAll();
        bool UpdateTraveller(Traveller traveller);
    }
}
