namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5).AddMinutes(30);
        public ICollection<Booking> Bookings { get; set; }
    }
}
