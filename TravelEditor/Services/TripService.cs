using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;
using TravelEditor.Repositories.Interfaces;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }
        public List<Trip> LoadAll()
        {
            return _tripRepository.LoadAll();
        }
    }
}
