using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    public class AttractionsGridViewModel
    {
        public ObservableCollection<Attraction> Attractions { get; set; }

        public AttractionsGridViewModel(List<Attraction> attractions)
        {
            Attractions = new ObservableCollection<Attraction>(attractions);
        }
    }
}
