namespace Backend.Models
{
    public class BookedSlot
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public int SlotId { get; set; }
        public Slot Slot { get; set; }
    }
}
