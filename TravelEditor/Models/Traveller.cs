using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Models
{
    public class Traveller
    {
        [Key]
        public int TravellerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

        public Traveller() { }

        public Traveller(string firstName, string lastName, string email, string phoneNumber, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Age = age;
        }
    }
}
