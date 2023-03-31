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
            DateAdded = DateTime.Now.ToString("MM/dd/yyyy");
        }
        public string ClientName { get; set; }
        public string DateAdded { get; set; }

        public void Rename(int maxClientNameSize)
        {
            bool invalidInput = true;
            String input;
            do
            {
                Console.WriteLine($"Enter the client's new name or type \"Exit\" to exit to main menu. ({maxClientNameSize} characters or less)");
                input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    return;
                }
            } while (string.IsNullOrWhiteSpace(input) || input.Length > maxClientNameSize);
        }
        public String ListClient()
        {
            int maxClientNameSize = 30;
            int nameSize = ClientName.Length;
            String spacingForCongruence = new string(' ', maxClientNameSize - nameSize + 1);
            return $"{ClientName}{spacingForCongruence}{DateAdded}";
        }
    }
}
