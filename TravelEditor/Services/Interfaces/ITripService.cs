using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Services.Interfaces
{
    public interface ITripService
    {
        void AddTrip(Trip trip);
        List<Trip> LoadAll();
        void UpdateTrip(Trip trip);
        bool ValidateDates(DateTime startDate, DateTime endDate);


    }
}
