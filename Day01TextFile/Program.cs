using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01TextFile
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Write("What is your name?");
            string name = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(@"C:\Users\anton\Desktop\send\data.txt"))
            {
                for (int i = 0; i < 3; i++) { 
                    writer.WriteLine(name);
                }
            }

            /*
            // Read a file  
            string readText = File.ReadAllText(@"C:\Users\anton\Desktop\send\data.txt");
            Console.WriteLine(readText);
            */

            FileStream fs = new FileStream("C:\\Users\\anton\\Desktop\\send\\data.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            Console.WriteLine("The lines from file are:");
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string str = sr.ReadLine();
            while (str != null)
            {
                Console.WriteLine(str);
                str = sr.ReadLine();
            }
            Console.ReadLine();
            sr.Close();
            fs.Close();



        }
    }
}
