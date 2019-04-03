using System;


namespace GSMTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GsmModem.Initialize("COM5");
            
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
