using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public bool Update(Attraction attraction, Destination destination)
        {
            if(attraction.Price != 0)
            {
                //update basic info for the attraction
                _attractionRepository.Update(attraction);

                //destination has changed, this destination doesnt have this attraction
                if (!destination.Attractions.Any(a => a.AttractionId == attraction.AttractionId))
                {
                    _attractionRepository.Delete(attraction);
                    _destinationService.AddDestinationAttraction(destination, attraction);
                }
                return true;
            }
            else
            {
                MessageBox.Show("Invalid price input");
                return false;
            }
           
        }
        public bool Delete(Attraction attraction)
        {
            if (_attractionRepository.FindOne(attraction))
            {
                return _attractionRepository.Delete(attraction);
            }
            return false;
        }

    }
}
