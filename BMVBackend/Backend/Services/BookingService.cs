using Backend.Models;

namespace Backend.Services
{
    public class BookingService
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
        public bool AddBooking(Booking b)
        {
            try
            {
                _bmvContext.Bookings.Add(b);
                _bmvContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateBooking(int id, Booking b)
        {
            var ub = _bmvContext.Bookings.Find(id);
            if (ub != null)
            {
                ub.CreatedAt = b.CreatedAt;
                ub.Status = b.Status;
                ub.ProviderId = b.ProviderId;
                ub.UserId = b.UserId;
                ub.End = b.End;
                ub.Date = b.Date;
                ub.Start = b.Start;
                ub.Amount = b.Amount;
                ub.VenueId = b.VenueId;
                _bmvContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteBooking(int id)
        {
            var db = _bmvContext.Bookings.Find(id);
            if(db!=null)
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
