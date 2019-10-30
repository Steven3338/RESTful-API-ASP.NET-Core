using System;
using System.Collections.Generic;

namespace PeopleInformation.Domain
{
    public class User : ClientChangeTracker
    {
        public User()
        {
            Addresses = new List<Address>();
            Cases = new List<Case>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        // navigation properties
        public List<Address> Addresses { get; set; }
        public List<Case> Cases { get; set; }
    }
}