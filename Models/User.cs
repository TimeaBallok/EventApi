namespace EventAPI.model
{
    public class User
    {
        public int UserId { get; set; }  
        public string Name { get; set; } 
        

        // Navigationsegenskab til Bookings
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
