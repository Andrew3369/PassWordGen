//File:             PassGen.cs
//Programmer:       Andrew Babos
//First Version:    10-27-2023

using System;

namespace RandomPWGen
{
    internal class PassGen
    {
        static void Main(string[] args)
        {
            PWGen pWGen = new PWGen(args);
            Console.WriteLine("Welcome to Password Generator!\n");

            // Start Program
            pWGen.PWGenMenu();
        }
    }
}
