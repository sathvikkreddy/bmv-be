namespace Backend.DTO
{
    public class BookingDTO
    {
        public int UserId { get; set; }
        public string Date { get; set; }
        public int[] SlotIds { get; set; }
        
    }
}
