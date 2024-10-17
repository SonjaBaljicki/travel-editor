using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Commands;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    internal class AttractionViewModel
    {
        public Attraction Attraction { get; set; }
        public SaveAttractionCommand SaveAttractionCommand { get;}

        public AttractionViewModel(Attraction attraction)
        {
            Attraction = attraction;
            SaveAttractionCommand = new SaveAttractionCommand(this);
        }
    }
}
