using System;
using RandomPWGen;

namespace RandomPWGen
{
    internal class PassGen
    {
        static void Main(string[] args)
        {
            PWGen pWGen = new PWGen(args);
            pWGen.GeneratePWs();
        }
    }
}
