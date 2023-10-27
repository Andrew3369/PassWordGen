//File:             PWGen.cs
//Programmer:       Andrew Babos
//First Version:    10-27-2023

using System;
using System.Windows;
using System.Threading;

namespace RandomPWGen
{
    internal class PWGen
    {
        private readonly string characters;

        // METHOD: PWGen
        // PURPOSE: Instantiate the PasswordGen class with the cmdline arguments
        // RETURNS: NONE
        public PWGen(string[] args)
        {
            characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+=-{}[]|:;<>?,./";
        }

        //METHOD: PWGenMenu
        //PURPOSE: Displays the Menu Navigation of the Program
        //RETURNS: None (void)
        public void PWGenMenu()
        {
            while (true)
            {
                // Menu Selection
                Console.WriteLine
                (
                "1. Create a Unique Password using GUID Generator (36 Letters in Length, Very Secure)\n" +
                "2. Create a Unique Password using a already set Character determination (16 Letters in Length, Less Secure than GUID)\n" +
                "3. Exit\n"
                );

                // User selection
                string response = Console.ReadLine();

                // Password Generator from GUID
                if (response == "1")
                {
                    // Set password
                    string password = GeneratePWsGUID();
                    Console.WriteLine("\tPassword: {0}\n", password);
                    Console.WriteLine("\tWould you like to copy the password to the clipboard?\n");

                    response = Console.ReadLine();

                    // If input is yes, copy to clipboard
                    if (response == "Y" || response == "y" || response == "yes" || response == "Yes")
                    {
                        Thread thread = new Thread(() => Clipboard.SetText(password));
                        Console.WriteLine("\tPassword Copied to Clipboard!\n");
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                        thread.Join();
                    }

                    // "No" input, do not copy to clipboard
                    else if (response == "N" || response == "n" || response == "No" || response == "no")
                    {
                        Console.WriteLine("\tPassword Not Copied to Clipboard!\n");
                    }

                    // Invalid Options recieved
                    else
                    {
                        Console.WriteLine("\tInvalid Response!\n");
                    }
                }

                // Password Generator from Given Set of Characters
                else if (response == "2")
                {
                    // Set password
                    string password = GeneratePWs();
                    Console.WriteLine("\tPassword: {0}\n", password);
                    Console.WriteLine("\tWould you like to copy the password to the clipboard?\n");
                    response = Console.ReadLine();

                    // If input is yes, copy to clipboard
                    if (response == "Y" || response == "y" || response == "yes" || response == "Yes")
                    {
                        Thread thread = new Thread(() => Clipboard.SetText(password));
                        //Clipboard.SetText(pWGen.GeneratePWs());
                        Console.WriteLine("\tPassword Copied to Clipboard!\n");
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                        thread.Join();
                    }

                    // "No" input, do not copy to clipboard
                    else if (response == "N" || response == "n" || response == "No" || response == "no")
                    {
                        Console.WriteLine("\tPassword Not Copied to Clipboard!\n");
                    }

                    // Invalid Options recieved
                    else
                    {
                        Console.WriteLine("\tInvalid Response!\n");
                    }
                }

                // Exit
                else if (response == "3")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }

                // If 1 or 2 wasnt selected
                else
                {
                    Console.WriteLine("Select One of the Options Above\nExiting...");
                }
            }
        }

        //METHOD: GeneratePWs
        //PURPOSE: Generates a new password from a given set of characters
        //RETURNS: string password
        public string GeneratePWs()
        {
            string password = "";
            Random random = new Random();
            for (int i = 0; i < 16; i++)
            {
                password += characters[random.Next(characters.Length)];
            }
            return password;
        }

        //METHOD: GeneratePWsGUID
        //PURPOSE: Generates the new passwords to the CMDline
        //RETURNS: string randomPassword
        public string GeneratePWsGUID()
        {
            Guid randomPassword = Guid.NewGuid();
            return randomPassword.ToString();
        }
    }
}
