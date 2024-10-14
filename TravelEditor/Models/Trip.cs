using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Models
{
    internal class Trip
    {
        public int TripId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public Destination Destination;
        public List<Traveller> Travellers { get; set; } = new List<Traveller>();
        public List<Review> Reviews { get; set; } = new List<Review>();

        public Trip() { }

        public Trip(int tripId, string name, DateTime startDate, DateTime endDate, string description, Destination destination, List<Traveller> travellers, List<Review> reviews)
        {
            TripId = tripId;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            Destination = destination;
            Travellers = travellers;
            Reviews = reviews;
        }
    }
}
