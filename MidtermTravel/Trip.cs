using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermTravel
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [MaxLength(30)]
        [MinLength(2)]
        public string Destination { get; set; }

        [Required]
        [StringLength(30)]
        [MaxLength(30)]
        [MinLength(2)]
        public string TravellerName { get; set; }

        [Required]
 //       [Index(IsUnique = false)]
        [RegularExpression(@"[A-Z]{2}[0-9]{6}")]
        public string TravellerPassport { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }


        /*
        public Trip(int id, string destination, string travellerName, string travellerPassport, DateTime tepartureDate, DateTime returnDate)
        {
            Id = id;
            Destination = destination;
            TravellerName = travellerName;
            TravellerPassport = travellerPassport;
            DepartureDate = tepartureDate;
            ReturnDate = returnDate;
        }
        */


        public static bool IsDestinationValid(string destination, out string error)
        {
            if (destination.Length < 2 || destination.Length > 30)
            {
                error = "Destination must be 2-30 characters long";
                return false;
            }
            error = null;
            return true;
        }

        public static bool IsNameValid(string name, out string error)
        {
            if (name.Length < 2 || name.Length > 30)
            {
                error = "Name must be 2-30 characters long";
                return false;
            }
            error = null;
            return true;
        }

        public static bool IsDepDateValid(DateTime depDate, out string error)
        {
            if (depDate < DateTime.Now)
            {
                error = "Date can not before today";
                return false;
            }
            error = null;
            return true;
        }

        public static bool IsReturnDateValid(DateTime depDate, DateTime retDate, out string error)
        {
            if (retDate < depDate)
            {
                error = "Return date can not be set before departure date";
                return false;
            }
            error = null;
            return true;
        }

    }
}
