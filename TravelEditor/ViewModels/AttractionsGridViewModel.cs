using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    internal class AttractionsGridViewModel
    {
        public List<Attraction> Attractions { get; set; }

        public AttractionsGridViewModel(List<Attraction> attractions)
        {
            Attractions = attractions;
        }
    }
}
