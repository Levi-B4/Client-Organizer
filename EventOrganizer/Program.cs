using System.Linq;
using System.Security.Cryptography.X509Certificates;

//change client search so there are less nested statements

namespace EventOrganizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClientRepo clientRepo = new ClientRepo();          
            
            bool Exit = false;

            while (!Exit) {
                //UI Window
                Console.WriteLine("What would you like to do to a client.\n" +
                                    "1: Add Client\n" +
                                    "2: Edit Client\n" +
                                    "3: Remove Client\n" +
                                    "4: Exit\n");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddClient();
                        clientRepo.SaveRepo();
                        break;

                    case "2":
                        /*
                        ToDo:Client client = SearchForExistingClient();
                        editClient();
                        */
                        break;
                    
                    case "3":
                        /*
                        client = SearchForExistingClient();
                        RemoveClient(client);
                        */
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

            bool VerifyClientIsNew(string ClientName)
            {
                return true;
            }

            void AddClient()
            {
                Console.WriteLine("Client has been added");
            }

            void EditClient()
            {
                Console.WriteLine("Edit");
            }

            void RemoveClient()
            {
                Console.WriteLine($"Client has been removed");
            }
        }
    }
}