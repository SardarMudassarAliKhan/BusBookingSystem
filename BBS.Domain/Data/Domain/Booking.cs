using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Domain.Data.Domain
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EnddateTim { get; set; }
        public string PickUpLocation { get; set; }
        public string DropLoaction { get; set; }
        public string TripId { get; set; }
        public string CreatedBy { get; set; }
        public string BookingStatus { get; set; }
        public string BookingType { get; set; }
        public string BookingCompany { get; set; }

    }
}
