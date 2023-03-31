using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizer
{
    internal class WeddingEvent : IEvent
    {
        public String EventType { get; } = "Wedding";
        public string EventDescription { get; set; }
        public string NumberOfGuests { get; set; }
        public String Spouse1LastName { get; set; }
        public String Spouse2LastName { get; set; }
        public string EventDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void EditEvent()
        {
            throw new NotImplementedException();
        }
    }
}
