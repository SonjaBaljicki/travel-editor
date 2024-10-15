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
    public class TravellerService : ITravellerService
    {
        private readonly ITravellerRepository _travellerRepository;

        public TravellerService(ITravellerRepository travellerRepository)
        {
            _travellerRepository = travellerRepository;
        }
        public List<Traveller> LoadAll()
        {
            return _travellerRepository.LoadAll();
        }
    }
}
