using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace EventOrganizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Client> clients = new List<Client>();
            
            clients.Add(new Client("Barry"));
            clients.Add(new Client("Adam"));

            bool Exit = false;

            while (!Exit) {
                //UI Window
                Console.WriteLine("What would you like to do to an event.\n" +
                                    "1: Add\n" +
                                    "2: Edit\n" +
                                    "3: Remove\n" +
                                    "4: Exit\n");
                string input = Console.ReadLine();
                Client client;

                switch (input)
                {
                    case "1": case "2":
                        //add or edit depending on if the client exists
                        client = CheckForExistingClient();
                        if (client.ClientName == "NO NAME")
                        {
                            AddEvent(client);
                        }else EditEvent(client);
                        break;
                    case "3":
                        //remove
                        client = CheckForExistingClient();
                        RemoveEvent(client); //make sure to confirm if client name isnt "no name"
                        break;
                    case "4":
                        //Exit
                        Exit = true;
                        break;

                    default:
                        Console.WriteLine("Please enter the number corresponding to your desired option.");
                        break;
                }
            }

            Client CheckForExistingClient()
            {
                Console.WriteLine("What is the client's name?");
                string input = Console.ReadLine();

                List<Client> matchingClients = new List<Client>();
                int counter = 0;

                foreach (Client client in clients)
                {
                    if (client.ClientName.ToLower().Contains(input))
                    {
                        matchingClients.Add(client);
                        counter += 1;
                        Console.WriteLine($"{counter}: {client.ClientName}");
                    }
                }

                Client selectedClient = new Client();
                
                if (matchingClients.Any())
                {
                    Console.WriteLine("\nAre any of the above clients the client you wish to add an event to?\n");

                    bool selected = false;
                    while (!selected)
                    {
                        Console.WriteLine("Enter the number of the corresponding client, or enter 0 if none correspond");
                        input = Console.ReadLine();
                        bool validInput = int.TryParse(input, out int selectNum);

                        if(validInput && selectNum >= 0 && selectNum <= matchingClients.Count)
                        {
                            if (selectNum != 0)
                            {
                                selectedClient = matchingClients[selectNum - 1];
                            }
                            selected = true;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid number");
                        }
                    }
                    matchingClients.Clear();
                }

                return selectedClient;
            }

            void AddEvent(Client client)
            {
                Console.WriteLine("Add");
            }

            void EditEvent(Client client)
            {
                Console.WriteLine("Edit");
            }

            void RemoveEvent(Client client)
            {
                Console.WriteLine("Add");
            }
        }
    }
}