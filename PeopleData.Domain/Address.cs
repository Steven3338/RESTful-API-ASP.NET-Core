using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleInformation.Domain
{
    public class Address : ClientChangeTracker
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public string MoveInDate { get; set; }
        public string MoveOutDate { get; set; }

        // navigation properties
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
