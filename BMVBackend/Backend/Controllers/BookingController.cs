using Backend.DTO;
using Backend.DTO.Booking;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        IBookingService _service;
        public BookingController(IBookingService bookingService)
        {
            _service = bookingService;
        }
        // GET: api/<BookingController>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var customerId = User.Claims.FirstOrDefault(c => c.Type == "CustomerId")?.Value;
            var providerId = User.Claims.FirstOrDefault(c => c.Type == "ProviderId")?.Value;
            List<GetBookingDTO> bookings;
            if(customerId != null)
            {
                bookings = _service.GetAllBookingsByCustomerId(Convert.ToInt32(customerId));
            }
            else
            {
                bookings = _service.GetAllBookingsByProviderId(Convert.ToInt32(providerId));
            }
            return Ok(bookings);
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookingController>
        [HttpPost]
        public IActionResult Post([FromBody] BookingDTO value)
        {
            var b = _service.AddBooking(value);
            if (b != null)
            {
                return Ok(b);
            }
            return BadRequest();

        }

        // PUT api/<BookingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
