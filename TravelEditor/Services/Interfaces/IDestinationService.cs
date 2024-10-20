﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.Services.Interfaces
{
    public interface IDestinationService
    {
        List<Destination> LoadAll();
        void AddDestination(Destination destination);
        void UpdateDestination(Destination destination);
        void AddDestinationAttractions(Destination destination, Attraction attraction);

        void Delete(Destination destination);
    }
}
