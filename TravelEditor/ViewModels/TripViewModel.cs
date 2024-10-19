using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Save;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    public class TripViewModel
    {
        public Trip Trip { get; set; }
        public SaveTripCommand SaveTripCommand { get; }


        public TripViewModel(Trip trip)
        {
            Trip = trip;
            SaveTripCommand = new SaveTripCommand(this);
        }
    }
}
