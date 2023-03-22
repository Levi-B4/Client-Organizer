using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using static System.Formats.Asn1.AsnWriter;

namespace EventOrganizer
{
    internal class ClientRepo
    {

        public List<Client> Clients { get; set; }
        //private string ClientRepoLocation = @"~\DataBase\ClientRepo.json";
        private string clientRepoLocation = Path.Combine(Environment.CurrentDirectory, @"ClientRepo.json");

        public ClientRepo()
        {
            Clients = LoadRepo();
        }

        public Client SearchForExistingClient()
        {
            Console.WriteLine("Searching for Client");
            return new Client("Placeholder");
        }

        public List<Client> LoadRepo()
        {
            Console.WriteLine("Loading repository");
            List<Client> clients = null;

            
            try
            {
                if (!File.Exists(clientRepoLocation))
                {
                    CreateInitialClientRepoFile();
                }
                string JClients = File.ReadAllText(clientRepoLocation);
                clients = JsonConvert.DeserializeObject<List<Client>>(JClients);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                //ToDo: Debug Log
                System.Environment.Exit(1);
            }

            Console.WriteLine("Repository loaded");

            return clients;
        }

        private void CreateInitialClientRepoFile()
        {
            var clients = new List<Client>()
            {
                new Client("Barry Canary"),
                new Client("Adam Adams"),
                new Client("Code Louisville")
            };
            
            string JClients = JsonConvert.SerializeObject(clients, Formatting.Indented);

            File.WriteAllText(clientRepoLocation, (JClients));
        }

        public void SaveRepo()
        {
            string JClients = JsonConvert.SerializeObject(Clients, Formatting.Indented);
            
            File.WriteAllText(clientRepoLocation, (JClients));
            
            Console.WriteLine(JClients);
        }
    }
}
