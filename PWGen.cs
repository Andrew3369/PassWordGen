//File:             PWGen.cs
//Programmer:       Andrew Babos
//First Version:    10-27-2023

using System;
using System.Windows;
using System.Threading;
using System.Linq;

namespace RandomPWGen
{
    internal class PWGen
    {
        private readonly string characters;

        // METHOD: PWGen
        // PURPOSE: Instantiate the PasswordGen class with the cmdline arguments
        // RETURNS: NONE
        //public PWGen(string[] args)
        public PWGen()
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
                "2. Create a Unique Password using a already set Character determination (16 Letters in Length, Less Secure)\n" +
                "3. Exit\n"
                );

                // User selection
                string response = Console.ReadLine();
                int num = Int16.Parse(response);
                string password = "";

                switch (num)
                {
                    case 1:
                        Thread doThing = new Thread(() => password = GeneratePWsGUID());
                        doThing.Start();
                        doThing.Join();

                        Console.WriteLine("\tWould you like to copy the password to the clipboard?\n\t Yes or No?\n\t");
                        CopyToClip(password);
                        break;

                    case 2:
                        // Set password
                        Thread doThingAgain = new Thread(() => password = GeneratePWs());
                        doThingAgain.Start();
                        doThingAgain.Join();
                        
                        //Console.WriteLine($"\tPassword: {password}\n");
                        Console.WriteLine("\tWould you like to copy the password to the clipboard?\n\t Yes or No?\n\t");
                        CopyToClip(password);
                        break;

                    case 3:
                        Thread doThingAGAIN = new Thread(() => password = GenerateMix());
                        doThingAGAIN.Start();
                        doThingAGAIN.Join();
                        CopyToClip(password);
                        break;

                    case 4:
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Select One of the Options Above\n");
                        break;
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

        //METHOD: GenerateBoth
        //PURPOSE: Generates the new passwords to the CMDline
        //RETURNS: string randomPassword
        public string GenerateMix()
        {
            Random random = new Random();
            Guid randomGUID = Guid.NewGuid();
            string tempHolder = randomGUID.ToString();
            string password = "";
            for (int i = 0; i < 16; i++)
            {
                password += characters[random.Next(characters.Length)];
                password += tempHolder[random.Next(tempHolder.Length)];
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

        //METHOD: CopyToClip
        //PURPOSE: Copies the password passed through onto the clipboard
        //RETURNS: none (void)
        public void CopyToClip(string password)
        {
            Console.WriteLine($"\tYour Password {password}\n");
            Console.WriteLine("\tWould you like to copy the password to the clipboard?\n\t Yes or No?\n\t");
            string response = Console.ReadLine();

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
    }
}
