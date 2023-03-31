using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

//change client search so there are less nested statements

namespace EventOrganizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClientRepo clientRepo = new ClientRepo();
            int maxClientNameSize = 30;

            bool exitApplication = false;
            Action[] mainMenu = {AddClient,
                                 RenameClient,
                                 RemoveClient,
                                 clientRepo.ViewClients,
                                 clientRepo.SaveRepo,
                                 () => exitApplication = ConfirmExitApplication()};
            while (!exitApplication) {
                Console.WriteLine("\nEnter the number corresponding to what you would like to do\n" +
                                    "1: Add Client\n" +
                                    "2: Rename Client\n" +
                                    "3: Remove Client\n" +
                                    "4: View Clients\n" +
                                    "5: Save Repository\n" +
                                    "6: Exit\n");

                string input = Console.ReadLine();
                if (int.TryParse(input, out int intInput) && intInput >= 1 && intInput <= mainMenu.Length)
                {
                    mainMenu[intInput - 1]();
                }
                else Console.WriteLine("Please enter a valid number.");
            }


            void AddClient()
            {
                Client clientToAdd = new Client(PromptUserForClientName());

                if (clientToAdd.ClientName == "exit")
                {
                    return;
                }

                if (clientRepo.VerifyIfClientExists(clientToAdd.ClientName))
                {
                    Console.WriteLine("\nThere is already a client by that name, returning to main menu.\n");
                    return;
                }

                clientRepo.AddClientToTempMemory(clientToAdd);
                clientRepo.SaveRepo();

                return;

            }

            void RenameClient()
            {
                Console.WriteLine("Renaming Client");

                String clientToRename = PromptUserForClientName();

                if (clientToRename == "exit")
                {
                    return;
                }

                Client client = clientRepo.SearchForExistingClient(clientToRename); ;

                if (client.ClientName == "exit search")
                {
                    return;
                }
                client.Rename(maxClientNameSize);

                clientRepo.SaveRepo();
            }


            void RemoveClient()
            {
                String clientToRemoveName = PromptUserForClientName();

                if (clientToRemoveName == "exit")
                {
                    return;
                }

                Client clientToRemove = clientRepo.SearchForExistingClient(clientToRemoveName); ;

                if (clientToRemove.ClientName == "exit search")
                {
                    return;
                }

                bool confirmDeletion = PromptUserWithYesOrNoQuestion($"Are you sure you want to remove {clientToRemove.ClientName}?");
                if (confirmDeletion)
                {
                    clientRepo.RemoveClientFromTempMemory(clientToRemoveName);
                }
                else
                {
                    Console.WriteLine("Client not removed, returning to Main Menu.");
                    return;
                }

                clientRepo.SaveRepo();
            }

            bool ConfirmExitApplication()
            {
                return PromptUserWithYesOrNoQuestion("Are you sure you want to exit? All unsaved changes will be lost.");
            }

            String PromptUserForClientName()
            {
                string input;
                do
                {
                    Console.WriteLine($"Enter the client's name or type \"Exit\" to exit to main menu. ({maxClientNameSize} characters or less)");
                    input = Console.ReadLine();
                    if (input.ToLower() == "exit")
                    {
                        return "exit";
                    }
                } while (string.IsNullOrWhiteSpace(input) || input.Length > maxClientNameSize);
                return input;
            }

            bool PromptUserWithYesOrNoQuestion(String Question)
            {
                bool validInput;
                String input;

                do
                {
                    Console.WriteLine($"{Question} (y/n)");
                    input = Console.ReadLine();
                    validInput = input.ToLower() == "y" || input.ToLower() == "n";
                    if (!validInput)
                    {
                        Console.WriteLine("Please enter \"y\" for yes or \"n\" for no");
                    }
                } while (!validInput);

                if (input == "y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
