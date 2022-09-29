using System;

namespace Day01HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("What is your name?");
                string name = Console.ReadLine();
                Console.Write("What is your age?");
                string ageStr = Console.ReadLine();
                if (!int.TryParse(ageStr, out int age))
                {
                    Console.WriteLine("Invalid input");
                    return;
                }
                Console.WriteLine($"Hello {name}! You are {age} y/o");

            }
            finally
            {
                Console.ReadKey();
            }


            /*
             *  Console.Write("What is your name?");
                string name = Console.ReadLine();
                Console.Write("What is your age?");
                try
                {
                    string ageStr = Console.ReadLine();
                    int age = int.Parse(ageStr);

                    Console.WriteLine($"Hello {name}! You are {age} y/o");
                }catch (FormatException ex)
                {
                    Console.WriteLine("Invalid input");
                }

                Console.ReadKey();
             */

        }



    }
}
