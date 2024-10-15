using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    internal class TravellerViewModel
    {
        public Traveller Traveller { get; set; }

        public TravellerViewModel(Traveller traveller)
        {
            Traveller = traveller;
        }
    }
}
