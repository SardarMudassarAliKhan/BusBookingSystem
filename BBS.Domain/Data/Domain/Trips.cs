using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Domain.Data.Domain
{
    public class Trips
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string DurationMinHour { get; set; }
        public string DurationMaxHour { get; set; }
        public string Rate13Seat { get; set; }
        public string Rate23Seat { get; set; }
        public string Rate35Seat { get; set; }
        public string Rate53Seat { get; set; }
    }
}
