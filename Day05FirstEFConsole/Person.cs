using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05FirstEFConsole
{
    public class Person
    {
        // [Key]
        public int Id { get; set; }

        [Required]// means not null
        [StringLength(50)] // nvarchar(50)
        public string Name { get; set; }

        [Required]
        [Index] // not unique, speeds up lookup operations
        public int Age { get; set; }

        //TODO: find out how to setup a unique index




        /*
        [NotMapped] // in memory only (not reflected in database as a column)
        public string Comment { get; set; }

        public GenderEnum Gender { }

        */

    }
}
