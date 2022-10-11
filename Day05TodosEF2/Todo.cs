using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Windows;

namespace Day05TodosEF2
{
    public class Todo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [MaxLength(100)]
        [MinLength(1)]
        [RegularExpression(@"([A-Za-z0-9\s./,-;+)(*!])+")]
        public string Task { get; set; }

        public int Difficulty { get; set; }

        [Required]
        // [Column(TypeName = "date")]
        // [Range(typeof(DateTime), "01/01/1900", "31/12/2099")]
        // [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime DueDate { get; set; }

        [Required]
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }
        public enum StatusEnum { Pending, Done, Delegated }

        /*
        public Todo(int id, string task, int difficulty, DateTime dueDate, StatusEnum status)
        {
            Id = id;
            Task = task;
            Difficulty = difficulty;
            DueDate = dueDate;
            Status = status;
        }
        */


        public static bool IsTaskValid(string task, out string error)
        {
            if (task.Length < 1 || task.Length > 100)
            {
                error = "Task must be 1-100 charcarters long";
                return false;
            }
            // If you declare a variable as output variable it MUST be assigned on all paths. So have to put it as something (null).
            // because there is no error here and it still needs to be assinged somehting it gets NULL
            error = null;
            return true;
        }

        public static bool IsDateValid(DateTime dueDate, out string error)
        {
            if (dueDate.Year < 1900 || dueDate.Year > 2099)
            {
                error = "Date must be between years 1900 - 2099";
                return false;
            }
            error = null;
            return true;
        }




    }
}
