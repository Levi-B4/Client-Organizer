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
    }
}
