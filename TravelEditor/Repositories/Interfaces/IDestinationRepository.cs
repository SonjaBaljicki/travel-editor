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
        void AddDestination(Destination destination);
        List<Destination> LoadAll();
        void UpdateDestination(Destination destination);
        void Delete(Destination destination);
        bool HasAssociatedTrips(Destination destination);


    }
}
