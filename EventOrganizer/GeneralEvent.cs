using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizer
{
    internal class GeneralEvent : IEvent
    {
        public String EventType { get; } = "Networking Event";
        public string EventDescription { get; set; }
        public string NumberOfGuests { get; set; }
        public string EventDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void EditEvent()
        {
            throw new NotImplementedException();
        }
    }
}
