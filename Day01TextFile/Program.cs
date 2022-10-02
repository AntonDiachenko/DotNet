using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day01TextFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {


                const string filePath = @"..\..\data.txt";

                Console.Write("Wrat is your name?");
                string name = Console.ReadLine();

                try
                {
                    { // write an array of strings
                        string[] namesArray = { name, name, name };
                        File.WriteAllLines(filePath, namesArray);
                    }

                    {
                        string fileContents = $"{name}\n{name}\n{name}\n";
                        File.WriteAllText(filePath, fileContents);
                    }
                    // NOTE: using Streamwriter is not recommended for small amount of data

                }
                catch (SystemException ex)
                {
                    Console.WriteLine("Error writing to file: " + ex.Message);
                }



                /*
                try 
                {
                    using (sw = new StreamWriter(filePath))
                    {
                        sw.WriteLine(name);
                        sw.WriteLine(name);
                        sw.WriteLine(name);
                        // Closes file automatically when leaving this block because 'using' is used.
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally 
                {
                    Console.WriteLine("Executing finally block.");
                    Console.ReadKey();
                }
                */

                // Reading from file
                try
                {
                    string[] linesArray = File.ReadAllLines(filePath);
                    foreach (string line in linesArray)
                    {
                        Console.WriteLine(line);
                    }

                }
                catch (SystemException ex)
                {
                    Console.WriteLine("Error writing to file: " + ex.Message);
                }
            }
            finally 
            { 
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            
            }


        }
    }
}
