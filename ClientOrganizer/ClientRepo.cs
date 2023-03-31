using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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

namespace ClientOrganizer
{
    public class ClientRepo
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

        void CreateInitialClientRepoFile()
        {
            String JClients = "[\r\n  {\r\n     \"ClientName\": \"Barry Canary\",\r\n    \"DateAdded\": \"2020/04/20\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"Adam Adams\",\r\n    \"DateAdded\": \"2022/10/15\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"Code Louisville\",\r\n    \"DateAdded\": \"2021/12/02\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"Text Comapany Inc\",\r\n    \"DateAdded\": \"2019/02/19\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"Placeholder Realstate Agency\",\r\n    \"DateAdded\": \"2023/03/30\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"Levi Butler\",\r\n    \"DateAdded\": \"2000/04/01\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"The Clients\",\r\n    \"DateAdded\": \"2020/05/10\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"Mr Name\",\r\n    \"DateAdded\": \"2022/07/15\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"Brian Smith\",\r\n    \"DateAdded\": \"2021/08/23\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"Ian Klein\",\r\n    \"DateAdded\": \"2023/01/30\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"Dr Eleven\",\r\n    \"DateAdded\": \"2023/03/12\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"Andy Rue\",\r\n    \"DateAdded\": \"2023/03/31\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"Long Name aaaaaaaaaaaaaaaaaaaa\",\r\n    \"DateAdded\": \"2015/03/29\"\r\n  },\r\n  {\r\n    " +
                                               "\"ClientName\": \"New People Company\",\r\n    \"DateAdded\": \"2023/03/31\"\r\n  }\r\n]";
            File.WriteAllText(clientRepoLocation, (JClients));
        }

        public void SaveRepo()
        {
            Console.WriteLine("\n\nSaving Repository");
            Clients = Clients.OrderBy(client => client.DateAdded).ToList();

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

        public void ResetRepo()
        {
            CreateInitialClientRepoFile();
            Clients = LoadRepo();
        }
    }
}
