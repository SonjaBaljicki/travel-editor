using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Save;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    public class TravellerViewModel
    {
        public Traveller Traveller { get; set; }
        public SaveTravellerCommand SaveTravellerCommand { get; }


        public TravellerViewModel(Traveller traveller)
        {
            Traveller = traveller;
            SaveTravellerCommand = new SaveTravellerCommand(this);
        }
    }
}
