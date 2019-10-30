using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleInformation.Domain
{
    public class Message : ClientChangeTracker
    {
        public Message()
        {
            DateTimeOfMessage = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime DateTimeOfMessage { get; set; }
        public string MessageText { get; set; }
        public int OriginatorId { get; set; }

        // navigation properties
        public int CaseId { get; set; }
        public Case Case { get; set; }
    }
}
