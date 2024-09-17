using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly BmvContext _bmvContext = new BmvContext();
        [HttpGet]
        public IActionResult Get()
        {
            var bookings = _bmvContext.Bookings.Where(b=>b.ProviderId==1).ToList();
            var totalEarnings = bookings.Sum(b => b.Amount);
            var totalBookings = bookings.Count();
            var venues = _bmvContext.Venues.ToList();
            var ratingSum = venues.Sum(v => v.Rating);
            var overallRating = ratingSum/venues.Count();
            var recentBookings = bookings.OrderByDescending(b=>b.CreatedAt).Take(5);
            var chartData = new ChartData(bookings);
            var today = DateOnly.FromDateTime(DateTime.Now);
            return Ok(new DashboardDTO() {TotalEarnings=totalEarnings, TotalBookings=totalBookings, OverallRating=overallRating, CData=chartData });
        }
    }
    public class DashboardDTO
    {
        public double TotalEarnings { get; set; }
        public double TotalBookings { get; set; }
        public float OverallRating { get; set; }
        public ChartData CData { get; set; }
    }
    public class ChartData
    {
        public int[] days { get; set; }
        public double[] earnings { get; set; }
        public ChartData(List<Booking> bookings)
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            var daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);

            days = Enumerable.Range(1, daysInMonth).ToArray();
            earnings = new double[daysInMonth];

            foreach (var booking in bookings)
            {
                if (booking.CreatedAt.Month == currentMonth && booking.CreatedAt.Year == currentYear)
                {
                    var day = booking.CreatedAt.Day;
                    earnings[day - 1] += (double)booking.Amount;
                }
            }
        }
    }
}
