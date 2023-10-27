using System;

namespace RandomPWGen
{
    internal class PWGen
    {
        private readonly int NumOfPWs;
        private readonly string characters;

        // METHOD: PWGen
        // PURPOSE: Instantiate the PasswordGen class with the cmdline arguments
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
            characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+=-{}[]|:;<>?,./";
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
        //RETURNS: NONE
        public string GeneratePWsGUID()
        {
            Guid randomPassword = Guid.NewGuid();
            return randomPassword.ToString();
        }
    }
}
