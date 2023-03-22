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
        public List<Event> ClientEvents { get; set; }
    
        public void EditClient()
        {
            Console.WriteLine("Client editing has not yet been implimented.");

            ClientEvents.Add(new Event());
            ClientEvents.Add(new Event());
        }
    }
}
