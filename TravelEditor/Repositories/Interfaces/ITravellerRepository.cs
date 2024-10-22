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
        bool AddTraveller(Traveller traveller);
        List<Traveller> LoadAll();
        bool UpdateTraveller(Traveller traveller);
        Traveller FindTravellerByEmail(string email);
        bool DeleteTraveller(Traveller? selectedTraveller);
    }
}
