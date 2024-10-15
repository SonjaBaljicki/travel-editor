using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    internal class AttractionViewModel
    {
        public Attraction Attraction { get; set; }

        public AttractionViewModel(Attraction attraction)
        {
            Attraction = attraction;
        }
    }
}
