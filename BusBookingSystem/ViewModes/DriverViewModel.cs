using BBS.Domain.Data.Domain;
using CVBank.Domain.Data.Domain;
using System.Collections.Generic;

namespace BusBookingSystem.ViewModes
{
    public class DriverViewModel
    {
        public Driver Driver { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
