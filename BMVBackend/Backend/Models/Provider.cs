namespace Backend.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Venue> Venues {get;set;}
    }
}
