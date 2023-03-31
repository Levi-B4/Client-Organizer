using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizer
{
    internal class CompanyPartyEvent : IEvent
    {
        public String EventType { get; } = "comapany party";
        public string EventDescription { get; set; }
        public string NumberOfGuests { get; set; }
        public string CompanyName { get; set; }
        public string ReasonForOccasion { get; set; }
        public string EventDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void EditEvent()
        {
            throw new NotImplementedException();
        }
    }
}
