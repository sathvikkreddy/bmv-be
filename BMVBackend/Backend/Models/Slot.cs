namespace Backend.Models
{
    public class Slot
    {
        public int Id { get; set; }
        public int VenueId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ICollection<BookedSlot> BookedSlots { get; set; }
    }
}
