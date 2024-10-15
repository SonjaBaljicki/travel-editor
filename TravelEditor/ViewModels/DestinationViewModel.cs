using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    internal class DestinationViewModel
    {
        public Destination Destination { get; set; }

        public DestinationViewModel(Destination destination)
        {
            Destination = destination;
        }
    }
}
