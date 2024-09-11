namespace Backend.DTO
{
    public class VenueDTO
    {
        public DateTime CreatedAt { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public bool IsAcceptingBookings { get; set; }
        public int ProviderId { get; set; }
    }
}
