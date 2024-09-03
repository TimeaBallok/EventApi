namespace EventAPI.model
{
    public class Booking
    {
        public int BookingId { get; set; }  
        public DateTime BookingDate { get; set; }  

        // Fremmednøgler
        public int EventId { get; set; }  
        public Event Event { get; set; }  // Navigationsegenskab til Event

        public int UserId { get; set; }  
        public User User { get; set; }  // Navigationsegenskab til User
    }
}
