using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Repositories.Interfaces
{
    public interface IDestinationRepository
    {
        bool Add(Destination destination);
        List<Destination> LoadAll();
        bool Update(Destination destination);
        bool Delete(Destination destination);
        bool HasAssociatedTrips(Destination destination);
        bool AddDestinationAttraction(Destination destination, Attraction attraction);
        Destination FindDestinationWithAttraction(Attraction attraction);
        bool FindOne(Destination destination);
    }
}
