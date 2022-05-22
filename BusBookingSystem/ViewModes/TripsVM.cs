namespace BusBookingSystem.ViewModes
{
    public class TripsVM
    {
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
