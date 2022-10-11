using System;
using System.Data.Entity;
using System.Linq;

namespace Day05TodosEF2
{
    public class Todo2DbContext : DbContext
    {
        // Your context has been configured to use a 'Todo2DbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Day05TodosEF2.Todo2DbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Todo2DbContext' 
        // connection string in the application configuration file.
        public Todo2DbContext()
            : base("name=Todo2DbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Todo> TodoTasks { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}