using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEditor.Database;
using TravelEditor.Models;

namespace TravelEditor.ViewModels
{
    internal class MainViewModel
    {
        public ObservableCollection<Destination> Destinations { get; set; }
        public ObservableCollection<Attraction> Attractions { get; set; }

        public MainViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            using (var db = new DatabaseContext())
            {
                Destinations = new ObservableCollection<Destination>(db.destinations);
                Attractions = new ObservableCollection<Attraction>(db.attractions);
            }
        }
    }
}
