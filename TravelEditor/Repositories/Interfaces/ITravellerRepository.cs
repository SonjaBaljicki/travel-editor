using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Repositories.Interfaces
{
    public interface ITravellerRepository
    {
        void AddTraveller(Traveller traveller);
        List<Traveller> LoadAll();
        void UpdateTraveller(Traveller traveller);
        Traveller FindTravellerByEmail(string email);
        void DeleteTraveller(Traveller? selectedTraveller);
    }
}
