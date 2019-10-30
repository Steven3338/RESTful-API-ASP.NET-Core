using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleInformation.Domain.AntiCorruption.Domain
{
    public class UserCase
    {
        public int Id { get; set; }
        public int ClosedById { get; set; }
        public string Subject { get; set; }
        public string DateTimeOfInitialMessage { get; set; }
        public string TimeToResolution { get; set; }
        public int UserId { get; set; }
    }
}
