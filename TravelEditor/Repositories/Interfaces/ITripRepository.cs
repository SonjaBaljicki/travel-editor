using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Repositories.Interfaces
{
    public interface ITripRepository
    {
        void AddTrip(Trip trip);
        List<Trip> LoadAll();
        void UpdateTrip(Trip trip);
    }
}
