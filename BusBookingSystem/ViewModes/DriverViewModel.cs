using BBS.Domain.Data.Domain;
using CVBank.Domain.Data.Domain;
using System.Collections.Generic;

namespace BusBookingSystem.ViewModes
{
    public class DriverViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public string DriverType { get; set; }
        public string Name { get; set; }
    }
}
