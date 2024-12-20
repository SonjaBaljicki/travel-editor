﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelEditor.Database;
using TravelEditor.Models;
using TravelEditor.Repositories;
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
        public bool Add(Destination destination)
        {
            return _destinationRepository.Add(destination);
            
        }

        //update an existing destianation
        public bool Update(Destination destination)
        {
            return _destinationRepository.Update(destination);   
        }

        //adds an attractio to an extisting destination
        public bool AddDestinationAttraction(Destination destination, Attraction attraction)
        {
            if (attraction.Price != 0)
            {
                return _destinationRepository.AddDestinationAttraction(destination, attraction);
            }
            else
            {
                MessageBox.Show("Invalid price input");
                return false;
            }
        }

        //delete a destination if it doesnt have trips
        public bool Delete(Destination destination)
        {
            if (_destinationRepository.FindOne(destination))
            {
                if (!_destinationRepository.HasAssociatedTrips(destination))
                {
                    return _destinationRepository.Delete(destination);
                }
                else
                {
                    MessageBox.Show("Cant delete destination");
                }
            }
            return false;
        }

        //finding a destination with this attraction
        public Destination FindDestinationWithAttraction(Attraction attraction)
        {
            return _destinationRepository.FindDestinationWithAttraction(attraction);
        }

        //method for finding destinations based on search text that user entered
        public List<Destination> FindDestinations(string searchDestinationsText)
        {
            return _destinationRepository.FindDestinations(searchDestinationsText);
        }
    }
}
