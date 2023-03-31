using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizer
{
    interface IEvent
    {
        public string EventType { get; }
        public string EventDescription { get; set; }
        public string NumberOfGuests { get; set; }
        public string EventDate { get; set; }  //TODO: Create Date verification
        
        public String ViewEvent()
        {
            return $"{EventDate}     {EventType}";
        }
        public void ChangeEventType()
        {
            Console.WriteLine("Event type change is not implimented");
        }
        void EditEvent();

    }
}
