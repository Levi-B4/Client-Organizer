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
        public Client()
        {
            ClientName = "NO NAME";
        }

        public bool IsNew { get; set; }
        public string ClientName { get; set; }
    
        public void EditClient()
        {
            Console.WriteLine("Client editing has not yet been implimented.");
        }
    }
}
