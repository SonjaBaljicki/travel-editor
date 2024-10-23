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
        bool Add(Traveller traveller);
        List<Traveller> LoadAll();
        bool Update(Traveller traveller);
        Traveller FindTravellerByEmail(string email);
        bool Delete(Traveller? selectedTraveller);
    }
}
