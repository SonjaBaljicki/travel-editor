using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Services.Interfaces
{
    public interface IDestinationService
    {
        List<Destination> LoadAll();
        bool AddDestination(Destination destination);
        bool UpdateDestination(Destination destination);
        bool AddDestinationAttractions(Destination destination, Attraction attraction);
        bool Delete(Destination destination);
        Destination FindDestinationWithAttraction(Attraction attraction);

    }
}
