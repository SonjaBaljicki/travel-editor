using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Models
{
    internal class Attraction
    {
        public int AttractionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }

        public Attraction() { }

        public Attraction(int attractionId, string name, string description, double price, string location)
        {
            AttractionId = attractionId;
            Name = name;
            Description = description;
            Price = price;
            Location = location;
        }
    }
}
