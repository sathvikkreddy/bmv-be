namespace Backend.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public float Rating { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public int VenueId { get; set; }

        public string Description { get; set; }
        public bool IsAcceptingBookings { get; set; }
        public int ProviderId { get; set; }

        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Slot> Slots { get; set; }

        public ICollection<BookedSlot> BookedSlots { get; set; }
    }
}
