using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands.Save;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    public class AttractionViewModel
    {
        public Attraction Attraction { get; set; }
        public SaveAttractionCommand SaveAttractionCommand { get;}
        public DestinationViewModel DestinationViewModel { get; }


        public AttractionViewModel(Attraction attraction, DestinationViewModel destinationViewModel)
        {
            Attraction = attraction;
            DestinationViewModel = destinationViewModel;
            SaveAttractionCommand = new SaveAttractionCommand(this);
        }
    }
}
