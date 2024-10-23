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
    public class AttractionService : IAttractionService
    {
        private readonly IAttractionRepository _attractionRepository;
        private readonly IDestinationService _destinationService;

        public AttractionService(IAttractionRepository attractionRepository, IDestinationService destinationService)
        {
            _attractionRepository = attractionRepository;
            _destinationService = destinationService;
        }

        public List<Attraction> LoadAll()
        {
            return _attractionRepository.LoadAll();
        }
        public bool UpdateAttraction(Attraction attraction, Destination destination)
        {
            //update basic info for the attraction
            _attractionRepository.UpdateAttraction(attraction);

            //destination has changed, this destination doesnt have this attraction
            if (!destination.Attractions.Any(a => a.AttractionId == attraction.AttractionId))
            {
                _attractionRepository.DeleteAttraction(attraction);
                _destinationService.AddDestinationAttractions(destination, attraction);
            }
            return true;
        }
        public bool DeleteAttraction(Attraction attraction)
        {
            if (_attractionRepository.FindOne(attraction))
            {
                _attractionRepository.DeleteAttraction(attraction);
                return true;
            }
            return false;
        }

    }
}
