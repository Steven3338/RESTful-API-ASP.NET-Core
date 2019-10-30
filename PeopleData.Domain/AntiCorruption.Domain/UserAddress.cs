using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleInformation.Domain.AntiCorruption.Domain
{
    public class UserAddress
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
    }
}
