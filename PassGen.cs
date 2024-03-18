//File:             PassGen.cs
//Programmer:       Andrew Babos
//First Version:    10-27-2023

using System;

namespace RandomPWGen
{
    internal class PassGen
    {
        //static void Main(string[] args)
        static void Main()
        {
            //PWGen pWGen = new PWGen(args);
            PWGen pWGen = new PWGen();
            Console.WriteLine("Welcome to Password Generator!\n");

            // Start Program
            pWGen.PWGenMenu();
        }
    }
}
