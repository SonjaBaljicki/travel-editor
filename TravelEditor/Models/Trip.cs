using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public int DestinationId;
        public virtual Destination Destination { get; set; }
        public virtual List<Traveller> Travellers { get; set; }
        public virtual List<Review> Reviews { get; set; }

        public Trip() { }

        public Trip(string name, DateTime startDate, DateTime endDate, string description, int destinationId, Destination destination, List<Traveller> travellers, List<Review> reviews)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            DestinationId = destinationId;
            Destination = destination;
            Travellers = travellers;
            Reviews = reviews;
        }
        public Trip(Trip trip)
        {
            TripId = trip.TripId;
            Name = trip.Name;
            StartDate = trip.StartDate;
            EndDate = trip.EndDate;
            Description = trip.Description;
            DestinationId = trip.DestinationId;
            Destination = trip.Destination;
        }
    }
}
