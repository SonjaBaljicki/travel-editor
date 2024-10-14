using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEditor.Models
{
    internal class Traveller
    {
        public int TravellerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

        public Traveller() { }

        public Traveller(int travellerId, string firstName, string lastName, string email, string phoneNumber, int age)
        {
            TravellerId = travellerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Age = age;
        }
    }
}
