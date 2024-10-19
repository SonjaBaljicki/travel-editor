using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Database;
using TravelEditor.Models;
using TravelEditor.Repositories.Interfaces;
using TravelEditor.Services.Interfaces;

namespace TravelEditor.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _destinationRepository;

        public DestinationService(IDestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository; 
        }
        //Loading all destinations
        public List<Destination> LoadAll()
        {
            return _destinationRepository.LoadAll();
        }
        //add new destination
        public void AddDestination(Destination destination)
        {
            _destinationRepository.AddDestination(destination);
        }
        //update an existing destiantion
        public void UpdateDestination(Destination destination)
        {
            _destinationRepository.UpdateDestination(destination);   
        }
        //delete a destination if it doesnt have trips
        public void Delete(Destination destination)
        {
            if (!_destinationRepository.HasAssociatedTrips(destination))
            {
                _destinationRepository.Delete(destination);
            }
        }
    }
}
