using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    public class TravellersGridViewModel
    {
        public List<Traveller> Travellers { get; set; }

        public TravellersGridViewModel(List<Traveller> travellers)
        {
            Travellers = travellers;
        }

    }
}
