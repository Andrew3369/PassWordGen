//File:             PWGen.cs
//Programmer:       Andrew Babos
//First Version:    10-27-2023

using System;
using System.Windows;
using System.Threading;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.IO;

namespace RandomPWGen
{
    internal class PWGen
    {
        // string of char
        private readonly string characters;

        // Documents Folder
        private readonly string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private readonly string fileName;
        private readonly string filePath;

        // Cryptography Related Fields
        //private const string encryptionKey = "";
        //private const string encryptionIV = "";
        //private const string filePath = "";

        // Data structure to hold private information
        private Dictionary<string, string> privateInfo = new Dictionary<string, string>();


        // METHOD: PWGen
        // PURPOSE: Instantiate the PasswordGen class with the cmdline arguments
        // RETURNS: NONE
        public PWGen()
        {
            characters = "abcdefghijklmnopqrstuvwxyz" +
                         "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                         "0123456789!@#$%^&*()_+=-{}" +
                         "[]|:;<>?,./";
            fileName = "passwords.txt";
            filePath = documentsFolder + "\\" + fileName;
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
                "3. Create a Unique Password using a mixture of both GUID and Characters (32 Letters in Length, Secure)\n" +
                "4. Create a Unique Password using Cryptography (16 Letters in Length, Very Secure)\n" +
                "5. Exit\n"
                );

                // User selection
                string response = Console.ReadLine();
                int num = Int16.Parse(response);
                string password = "";

                switch (num)
                {
                    case 1:
                        Thread strongPW = new Thread(() => password = GeneratePWsGUID());
                        strongPW.Start();
                        strongPW.Join();
                        CopyToClip(password);
                        break;

                    case 2:
                        Thread strongerPW = new Thread(() => password = GeneratePWs());
                        strongerPW.Start();
                        strongerPW.Join();
                        CopyToClip(password);
                        break;

                    case 3:
                        Thread mixture = new Thread(() => password = GenerateMix());
                        mixture.Start();
                        mixture.Join();
                        CopyToClip(password);
                        break;

                    case 4:
                        Thread Cryptic = new Thread(() => password = GeneratePWsCrypt());
                        Cryptic.Start();
                        Cryptic.Join();
                        CopyToClip(password);
                        break;

                    case 5:
                        Console.WriteLine("Exiting...");
                        return;

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
            try
            {
                string password = "";
                Random random = new Random();
                for (int i = 0; i < 16; i++)
                {
                    password += characters[random.Next(characters.Length)];
                }
                return password;
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }

        //METHOD: GenerateBoth
        //PURPOSE: Generates the new passwords to the CMDline
        //RETURNS: string randomPassword
        public string GenerateMix()
        {
            try
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
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        //METHOD: GeneratePWsGUID
        //PURPOSE: Generates the new passwords to the CMDline
        //RETURNS: string randomPassword
        public string GeneratePWsGUID()
        {
            return Guid.NewGuid().ToString();
        }

        //METHOD: CopyToClip
        //PURPOSE: Copies the password and passed through onto the clipboard
        //RETURNS: none (void)
        public void CopyToClip(string password)
        {
            Console.WriteLine($"\tYour Password {password}\n");
            Console.WriteLine("\tWould you like to copy the password to the clipboard?\n\t Yes or No?\n\t");
            string response = Console.ReadLine();

            // If input is yes, copy to clipboard
            switch (response)
            {
                case "Y":
                case "y":
                case "yes":
                case "Yes":
                    try
                    {
                        Thread thread = new Thread(() => Clipboard.SetText(password));
                        Console.WriteLine("\tPassword Copied to Clipboard!\n");
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                        thread.Join();
                    }
                    catch
                    {
                        Console.WriteLine("Error copying to clipboard\n");
                    }
                    break;

                case "N":
                case "n":
                case "No":
                case "no":
                    Console.WriteLine("\tPassword Not Copied to Clipboard!\n");
                    break;
            }
        }

        //METHOD: GeneratePWsCrypt
        //PURPOSE: Generates an Encrypted Password
        //RETURNS: string password
        private string GeneratePWsCrypt()
        {
            try
            {
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    byte[] tokenData = new byte[16];
                    rng.GetBytes(tokenData);
                    string password = Convert.ToBase64String(tokenData);
                    Console.WriteLine(password);
                    return password;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }


        private class CryptographyClass
        {
            private readonly string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            private readonly string fileName;
            private readonly string filePath;

            // Cryptography Related Fields
            //private const string encryptionKey = "";
            //private const string encryptionIV = "";
            //private const string filePath = "";

            // Data structure to hold private information
            private Dictionary<string, string> privateInfo = new Dictionary<string, string>();

            public CryptographyClass()
            {
                filePath = documentsFolder + "\\" + fileName;
            }
            public void WritePasswordsToFile(Dictionary<string, string> passwords)
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var pair in passwords)
                    {
                        string encryptedPassword = EncryptString(pair.Value);
                        writer.WriteLine($"Username or email: {pair.Key}");
                        writer.WriteLine($"PW: {encryptedPassword}");
                    }
                }
            }

            public Dictionary<string, string> ReadPasswordsFromFile()
            {
                Dictionary<string, string> decryptedPasswords = new Dictionary<string, string>();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    while (!reader.EndOfStream)
                    {
                        string usernameOrEmail = reader.ReadLine().Substring("Username or email: ".Length);
                        string encryptedPassword = reader.ReadLine().Substring("PW: ".Length);
                        string decryptedPassword = DecryptString(encryptedPassword);
                        decryptedPasswords.Add(usernameOrEmail, decryptedPassword);
                    }
                }
                return decryptedPasswords;
            }

            private string EncryptString(string plainText)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    byte[] key = aesAlg.Key;
                    byte[] iv = aesAlg.IV;
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, iv);

                    byte[] encryptedBytes;
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            encryptedBytes = msEncrypt.ToArray();
                        }
                    }
                    return Convert.ToBase64String(encryptedBytes);
                }
            }

            private string DecryptString(string encryptedText)
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                using (Aes aesAlg = Aes.Create())
                {
                    byte[] key = aesAlg.Key;
                    byte[] iv = aesAlg.IV;
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, iv);

                    using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }
}
