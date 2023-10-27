using System;
using System.Windows;
using System.Threading;

namespace RandomPWGen
{
    internal class PassGen
    {
        static void Main(string[] args)
        {

            PWGen pWGen = new PWGen(args);
            Console.WriteLine("Welcome to Password Generator!\n");

            while (true)
            {
                // Menu Selection
                Console.WriteLine
                (
                "1. Create a Unique Password using GUID Generator (36 Letters in Length, Very Secure)\n" +
                "2. Create a Unique Password using a already set Character determination (16 Letters in Length, Less Secure than GUID)\n"
                );

                // User selection
                string response = Console.ReadLine();

                // Password Generator from GUID
                if (response == "1")
                {
                    Console.WriteLine("\tPassword: {0}\n", pWGen.GeneratePWsGUID());
                    Console.WriteLine("\tWould you like to copy the password to the clipboard?\n");

                    response = Console.ReadLine();

                    if (response == "Y" || response == "y" || response == "yes" || response == "Yes")
                    {
                        Thread thread = new Thread(() => Clipboard.SetText(pWGen.GeneratePWsGUID()));
                        Console.WriteLine("\tPassword Copied to Clipboard!\n");
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                        thread.Join();
                    }

                    else if (response == "N" || response == "n" || response == "No" || response == "no")
                    {
                        Console.WriteLine("\tPassword Not Copied to Clipboard!\n");
                    }

                    else
                    {
                        Console.WriteLine("\tInvalid Response!\n");
                    }
                }

                // Password Generator from Given Set of Characters
                else if (response == "2")
                {
                    //pWGen.GeneratePWs();
                    string password = pWGen.GeneratePWs();
                    Console.WriteLine("\tPassword: {0}\n", password);
                    Console.WriteLine("\tWould you like to copy the password to the clipboard?\n");
                    response = Console.ReadLine();

                    if (response == "Y" || response == "y" || response == "yes" || response == "Yes")
                    {
                        Thread thread = new Thread(() => Clipboard.SetText(password));
                        //Clipboard.SetText(pWGen.GeneratePWs());
                        Console.WriteLine("\tPassword Copied to Clipboard!\n");
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                        thread.Join();
                    }

                    else if (response == "N" || response == "n" || response == "No" || response == "no")
                    {
                        Console.WriteLine("\tPassword Not Copied to Clipboard!\n");
                    }

                    else
                    {
                        Console.WriteLine("\tInvalid Response!\n");
                    }
                }

                // If 1 or 2 wasnt selected
                else
                {
                    Console.WriteLine("Select One of the Options Above\nExiting...");
                    Environment.Exit(0);
                }
            }
        }
    }
}
