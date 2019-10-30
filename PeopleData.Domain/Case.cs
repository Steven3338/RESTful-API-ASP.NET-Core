using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleInformation.Domain
{
    public class Case
    {
        public Case()
        {
            DateTimeOfInitialMessage = DateTime.UtcNow;
            Messages = new List<Message>();
        }
        public int Id { get; set; }
        public int ClosedById { get; set; }
        public string Subject { get; set; }
        public DateTime DateTimeOfInitialMessage { get; set; }
        public DateTime? TimeToResolution { get; set; }

        // navigation properties
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Message> Messages { get; set; }
    }
}
