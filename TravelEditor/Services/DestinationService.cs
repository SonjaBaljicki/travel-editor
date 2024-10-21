using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        //update an existing destianation
        public void UpdateDestination(Destination destination)
        {
            _destinationRepository.UpdateDestination(destination);   
        }
        //adds an attractio to an extisting destination
        public void AddDestinationAttractions(Destination destination, Attraction attraction)
        {
            _destinationRepository.AddDestinationAttractions(destination, attraction);
        }
        //delete a destination if it doesnt have trips
        public void Delete(Destination destination)
        {
            if (_destinationRepository.FindOne(destination))
            {
                if (!_destinationRepository.HasAssociatedTrips(destination))
                {
                    _destinationRepository.Delete(destination);
                }
                else
                {
                    MessageBox.Show("Cant delete destination");
                }
            }
        }
        public Destination FindDestinationWithAttraction(Attraction attraction)
        {
            return _destinationRepository.FindDestinationWithAttraction(attraction);
        }
    }
}
