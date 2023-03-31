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

            bool exitApplication = false;
            Action[] mainMenu = {AddClient,
                                 EditClient,
                                 RemoveClient,
                                 clientRepo.SaveRepo,
                                 () => exitApplication = true};
            while (!exitApplication) {

                Console.WriteLine("Enter the number corresponding to what you would like to do\n" +
                                    "1: Add Client\n" +
                                    "2: Edit Client\n" +
                                    "3: Remove Client\n" +
                                    "4: Save Repository\n" +
                                    "5: Exit\n");

                string input = Console.ReadLine();
                if (int.TryParse(input, out int intInput) && intInput >= 1 && intInput <= mainMenu.Length)
                {
                    mainMenu[intInput - 1]();
                }
                else Console.WriteLine("Please enter a valid number.");
            }
            clientRepo.SaveRepo();


            void AddClient()
            {
                Console.WriteLine("Adding Client is not yet implimented");
            }

            void EditClient()
            {
                Console.WriteLine("Editing Client");

                Client clientToEdit = PromptUserToSearchForClient();
                if (clientToEdit.ClientName == "exit search")
                {
                    return;
                }

                bool stillEditing = true;

                Action saveClientEdits = () =>
                {
                    clientRepo.UpdateClientInTempMemory(clientToEdit);
                    clientRepo.SaveRepo();
                };

                Action[] editorMenu = {clientToEdit.RenameClient,
                                       clientToEdit.AddEvent,
                                       clientToEdit.RemoveEvent,
                                       clientToEdit.EditEvent,
                                       saveClientEdits,
                                       () => stillEditing = false};

                while (stillEditing)
                {
                    Console.WriteLine($"Enter the number corresponding to what you would like to edit for {clientToEdit.ClientName}.\n" +
                                       "1: Change Client Name\n" +
                                       "2: Add Event\n" +
                                       "3: Remove Event\n" +
                                       "4: Edit Event\n" +
                                       "5: Save Edits\n" +
                                       "6: Return to Main Menu");

                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int intInput) && intInput >= 1 && intInput <= editorMenu.Length)
                    {
                        editorMenu[intInput - 1]();
                    }
                    else Console.WriteLine("Please enter a valid number");
                }
            }


            void RemoveClient()
            {
                Console.WriteLine($"Remove Client has not been implimented");
            }

            Client PromptUserToSearchForClient()
            {
                string input;
                do
                {
                    Console.WriteLine("Enter the client's name or type \"Exit Search\" to exit to main menu. (30 characters or less)");
                    input = Console.ReadLine();
                    if(input.ToLower() == "Exit Search")
                    {
                        return new Client("Exit Search");
                    }
                } while (string.IsNullOrWhiteSpace(input) || input.Length > 30);

                Client selectedClient = clientRepo.SearchForExistingClient(input);

                return selectedClient;
            }
        }
    }
}
