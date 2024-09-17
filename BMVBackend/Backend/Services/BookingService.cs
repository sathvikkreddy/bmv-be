using Backend.DTO;
using Backend.DTO.Booking;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BookingService : IBookingService
    {
        private readonly BmvContext _bmvContext = new BmvContext();
        public List<Booking> GetAllBookings()
        {
            return _bmvContext.Bookings.Include("BookedSlots").ToList();
        }
        public List<GetBookingDTO> GetAllBookingsByProviderId(int id)
        {
            List<GetBookingDTO> bookings = new List<GetBookingDTO>();
            var pBookings = _bmvContext.Bookings.Include("BookedSlots").Where(p => p.ProviderId == id).ToList();
            foreach (var item in pBookings)
            {
                var gBooking = new GetBookingDTO();
                gBooking.Start = item.Start;
                gBooking.CreatedAt = item.CreatedAt;
                gBooking.End = item.End;
                gBooking.Id = item.Id;
                gBooking.CustomerId = item.CustomerId;
                gBooking.ProviderId = item.ProviderId;
                gBooking.VenueId = item.VenueId;
                gBooking.Amount = item.Amount;
                gBooking.Date = item.Date;
                gBooking.BookedSlots = item.BookedSlots;
                gBooking.Status = item.End > TimeOnly.FromDateTime(DateTime.Now) ? "done" : "upcoming";
                bookings.Add(gBooking);
            }
            return bookings;
        }
        public List<GetBookingDTO> GetAllBookingsByCustomerId(int id)
        {
            List<GetBookingDTO> bookings = new List<GetBookingDTO>();
            var pBookings = _bmvContext.Bookings.Include("BookedSlots").Where(p => p.CustomerId == id).ToList();
            foreach (var item in pBookings)
            {
                var gBooking = new GetBookingDTO();
                gBooking.Start = item.Start;
                gBooking.CreatedAt = item.CreatedAt;
                gBooking.End = item.End;
                gBooking.Id = item.Id;
                gBooking.CustomerId = item.CustomerId;
                gBooking.ProviderId = item.ProviderId;
                gBooking.VenueId = item.VenueId;
                gBooking.Amount = item.Amount;
                gBooking.Date = item.Date;
                gBooking.BookedSlots = item.BookedSlots;
                gBooking.Status = item.End > TimeOnly.FromDateTime(DateTime.Now) ? "done" : "upcoming";
                bookings.Add(gBooking);
            }
            return bookings;
        }
        public Booking GetBookingById(int id)
        {
            return _bmvContext.Bookings.Find(id);
        }
        public Booking AddBooking(BookingDTO value)
        {
            if (value.SlotIds.Length < 1)
            {
                return null;
            }
            var vId = _bmvContext.Slots.Find(value.SlotIds[0]).VenueId;
            var pId = _bmvContext.Venues.Find(vId).ProviderId;
            Booking b = new Booking();
            b.CustomerId = value.CustomerId;
            b.VenueId = vId;
            b.ProviderId = pId;
            var bDate = DateOnly.ParseExact(value.Date, "dd-MM-yyyy");
            b.Date = bDate;
            _bmvContext.Bookings.Add(b);
            b.Start = TimeOnly.MaxValue;
            b.End = TimeOnly.MinValue;
            _bmvContext.SaveChanges();
            double price = 0;
            foreach (var s in value.SlotIds)
            {
                var slot = _bmvContext.Slots.Find(s);
                if (slot != null)
                {
                    _bmvContext.BookedSlots.Add(new BookedSlot() { VenueId = vId, BookingId = b.Id, Date = b.Date, SlotId = s });
                    if (slot.Start < b.Start)
                    {
                        b.Start = slot.Start;
                    }
                    if (slot.End > b.End)
                    {
                        b.End = slot.End;
                    }
                    if((int)bDate.DayOfWeek==0 || (int)bDate.DayOfWeek == 6)
                    {
                        Console.WriteLine("hi" + slot.WeekendPrice);
                        price += slot.WeekendPrice;
                    }
                    else
                    {
                        Console.WriteLine(slot.WeekdayPrice);
                        price += slot.WeekdayPrice;
                    }
                }
            }
            b.Amount = price +( price > 1000 ? 100 : 50);
            Console.WriteLine(b.Amount);
            _bmvContext.SaveChanges();

            return b;
        }
        public bool UpdateBooking(int id, Booking b)
        {
            var ub = _bmvContext.Bookings.Find(id);
            if (ub != null)
            {
                ub.CreatedAt = b.CreatedAt;
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
