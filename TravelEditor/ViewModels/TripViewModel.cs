﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    internal class TripViewModel
    {
        public Trip Trip { get; set; }

        public TripViewModel(Trip trip)
        {
            Trip = trip;
        }
    }
}