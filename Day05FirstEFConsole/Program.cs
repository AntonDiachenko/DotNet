using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05FirstEFConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                SocietyDbContext ctx = new SocietyDbContext();
                Random random = new Random();
                // INSERT: equivalent of insert
                Person p1 = new Person { Name = "Jerry " + random.Next(100), Age = random.Next(100) };
                ctx.People.Add(p1); // insert operation is scheduled but NOT executed yet
                ctx.SaveChanges(); // synchronize objects in memory with database
                Console.WriteLine("record added");
            }
            catch (SystemException ex) // catch-all for EF, SQL and many other exceptions
            {
                Console.WriteLine("Database operation failed: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }


        }
    }
}
