using System;

namespace RandomPWGen
{
    internal class PWGen
    {
        private readonly int NumOfPWs;

        // METHOD: ThreadsTasks
        // PURPOSE: Instantiate the Threads and Tasks class with the cmd arguments
        // RETURNS: NONE
        public PWGen(string[] args)
        {
            if (args.Length == 0)
            {
                NumOfPWs = 1;
            }
            else
            {
                NumOfPWs = int.Parse(args[0]);
            }
        }

        // METHOD: GeneratePWs
        // PURPOSE: Generates the new passwords to the CMDline
        // RETURNS: NONE
        public void GeneratePWs()
        {
            Console.WriteLine("Potential Passwords: \n");
            for (int i = 0; i < NumOfPWs; i++)
            {
                Guid randomPassword = Guid.NewGuid();
                Console.WriteLine("\tPassword #{0}: {1}", i, randomPassword.ToString());
            }
        }
    }
}
