using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using static System.Formats.Asn1.AsnWriter;

namespace EventOrganizer
{
    internal class ClientRepo
    {

        public List<Client> Clients { get; set; }
        private string clientRepoLocation = Path.Combine(Environment.CurrentDirectory, @"ClientRepo.json");

        public ClientRepo()
        {
            Clients = LoadRepo();
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

            Console.WriteLine("Repository loaded\n\n");

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
            Console.WriteLine("\n\nSaving Repository");

            string JClients = JsonConvert.SerializeObject(Clients, Formatting.Indented);
            File.WriteAllText(clientRepoLocation, (JClients));
            
            Console.WriteLine("Repository Saved");
        }

        public Client SearchForExistingClient(string clientName)
        {
            Console.WriteLine($"Searching for {clientName}");
            IEnumerable<Client> matches = Clients.Where(client => client.ClientName.Contains(clientName));

            bool selectingClient;
            for (int i = 0; i < matches.Count(); i++)
            {
                Console.WriteLine($"{i + 1}: {matches.ElementAt(i).ClientName}");

                if ((i + 1) % 5 == 0 || i + 1 == matches.Count())
                {
                    selectingClient = true;
                    while (selectingClient)
                    {
                        Console.WriteLine("Enter the number of the corresponding client " +
                                          "or type \"Next\" to view more matching clients. " +
                                          "Type \"Exit\" to exit search");
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out int SelectedClientInd) && SelectedClientInd >= 1 && SelectedClientInd <= 5 && SelectedClientInd <= matches.Count())
                        {
                            return matches.ElementAt((i / 5) + int.Parse(input) - 1);
                        }
                        else if (input.ToLower() == "exit")
                        {
                            Console.WriteLine("\nExiting search\n");
                            return new Client("exit search");
                        }
                        else if (input.ToLower() == "next")
                        {
                            selectingClient = false;
                        }
                        Console.WriteLine("\nPlease enter a valid input.\n");
                    }
                }
            }
            Console.WriteLine("\nClient was not found, exiting search.\n");
            return new Client("exit search");
        }

        public bool VerifyIfClientExists(String clientName)
        {
            IEnumerable<Client> matchingClients = Clients.Where(i => i.ClientName == clientName);
            return matchingClients.Count() != 0;
        }

        public void AddClientToTempMemory(Client client)
        {
            Clients.Add(client);
        }

        public void RemoveClientFromTempMemory(String clientName)
        {
            Clients = Clients.Where(i => i.ClientName != clientName).ToList();
            Console.WriteLine($"{clientName} has been removed.");
        }

        public void ViewClients()
        {
            Console.WriteLine("\n\n");
            Client clientToList;
            for (int i = 0; i < Clients.Count(); i++)
            {
                clientToList = Clients.ElementAt(i);
                Console.WriteLine($"{String.Format("{0:000}", i + 1)}: {clientToList.ListClient()}");
            }
            Console.WriteLine("Press Enter to exit to the Main Menu");
            Console.ReadLine();
        }

        public void UpdateClientInTempMemory(Client newClient)
        {
            Client clientToUpdate = Clients.Where(i => i.ClientName == newClient.ClientName).First();
            var index = Clients.IndexOf(clientToUpdate);

            if (index != -1)
                Clients[index] = newClient;
        }
    }
}
