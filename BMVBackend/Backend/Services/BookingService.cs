using Backend.DTO;
using Backend.Models;

namespace Backend.Services
{
    public class BookingService : IBookingService
    {
        private readonly BmvContext _bmvContext = new BmvContext();
        public List<Booking> GetAllBookings()
        {
            return _bmvContext.Bookings.ToList();
        }
        public Booking GetBookingById(int id)
        {
            return _bmvContext.Bookings.Find(id);
        }
        public bool AddBooking(BookingDTO value)
        {
            Booking b = new Booking();
            b.UserId = value.UserId;
            b.Date = DateOnly.ParseExact(value.Date, "dd-MM-yyyy") ;
            b.Status = "upcoming";
            List<Slot> slots = new List<Slot>();
            foreach (var s in value.SlotIds)
            {
                var slot = _bmvContext.Slots.Find(s);
                if (slot != null)
                {
                    slots.Add(slot);
                    b.ProviderId = _bmvContext.Venues.Find(slot.VenueId).ProviderId;
                    b.VenueId = slot.VenueId;
                }
            }
            _bmvContext.SaveChanges();
            Console.WriteLine(b.Id);
            return true;
        }
        public bool UpdateBooking(int id, Booking b)
        {
            var ub = _bmvContext.Bookings.Find(id);
            if (ub != null)
            {
                ub.CreatedAt = b.CreatedAt;
                ub.Status = b.Status;
                ub.End = b.End;
                ub.Date = b.Date;
                ub.Start = b.Start;
                ub.Amount = b.Amount;
                _bmvContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteBooking(int id)
        {
            var db = _bmvContext.Bookings.Find(id);
            if (db != null)
            {
                _bmvContext.Bookings.Remove(db);
                _bmvContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
