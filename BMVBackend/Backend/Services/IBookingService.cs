using Backend.DTO;
using Backend.Models;

namespace Backend.Services
{
    public interface IBookingService
    {
        bool AddBooking(BookingDTO value);
        bool DeleteBooking(int id);
        List<Booking> GetAllBookings();
        Booking GetBookingById(int id);
        bool UpdateBooking(int id, Booking b);
    }
}