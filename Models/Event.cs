namespace EventAPI.model
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        
        // Navigationsegenskab til Bookings
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
