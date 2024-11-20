using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Models
{
    public class Attraction
    {
        [Key]
        public int AttractionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }

        public Attraction() { }

        public Attraction(string name, string description, double price, string location)
        {
            Name = name;
            Description = description;
            Price = price;
            Location = location;
        }
        public Attraction(Attraction attraction)
        {
            AttractionId = attraction.AttractionId;
            Name = attraction.Name;
            Description = attraction.Description;
            Price = attraction.Price;
            Location = attraction.Location;
        }
    }
}
