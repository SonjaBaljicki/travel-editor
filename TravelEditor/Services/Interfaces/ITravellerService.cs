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
        bool Add(Traveller traveller);
        bool AddTravellerToTrip(Traveller selectedTraveller, Trip trip);
        bool Delete(Traveller? selectedTraveller);
        bool DeleteTravellerFromTrip(Trip trip, Traveller selectedTraveller);
        List<Traveller> FindTravellers(string searchTravellersText);
        List<Traveller> LoadAll();
        bool Update(Traveller traveller);
    }
}
