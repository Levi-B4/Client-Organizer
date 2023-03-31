using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizer
{
    internal class Client
    {
        public Client(string clientName)
        {
            ClientName = clientName;
        }
        public string ClientName { get; set; }
        public int MaxClientNameSize { get; set; } = 30;
        public List<IEvent> ClientEvents { get; set; }


        public void RenameClient()
        {
            Console.WriteLine("Rename Client has not yet been implimented.");
        }
        public void AddEvent()
        {
            Console.WriteLine("Add Event has not yet been implimented.");
        }
        public void EditEvent()
        {
            Console.WriteLine("Edit Event has not yet been implimented.");
        }
        public void RemoveEvent()
        {
            Console.WriteLine("Remove Event has not yet been implimented.");
        }
        public void ViewEvents()
        {
            Console.WriteLine("View not yet implimented");
        }
        public void VerifyNewEvent() 
        {
            Console.WriteLine("Verification for new events not yet implimented");
        }
    }
}
