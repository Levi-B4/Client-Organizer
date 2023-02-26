using System.Security.Cryptography.X509Certificates;

namespace EventOrganizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Client> clients = new List<Client>();

            bool Exit = false;

            while (!Exit) {
                //add edit remove exit
                Console.WriteLine("What would you like to do to an event:" +
                                    "1: Add" +
                                    "1: Edit" +
                                    "1: Remove" +
                                    "1: Exit");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        //add
                        AddEvent();
                        break;
                    case "2":
                        //edit
                        EditEvent();
                        break;
                    case "3":
                        //remove
                        RemoveEvent();
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

            void AddEvent()
            {
                Console.WriteLine("Add");
            }

            void EditEvent()
            {
                Console.WriteLine("Edit");
            }

            void RemoveEvent()
            {
                Console.WriteLine("Add");
            }
        }
    }
}