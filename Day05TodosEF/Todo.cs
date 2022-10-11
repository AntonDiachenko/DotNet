using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05TodosEF
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
        //       [Column(TypeName = "date")]
        [Range(typeof(DateTime), "01/01/1900", "31/12/2099")]
        public DateTime DueDate { get; set; }

        [Required]
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }
        public enum StatusEnum { Pending = 1, Done = 2, Delegated = 3 }

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
    }
}
