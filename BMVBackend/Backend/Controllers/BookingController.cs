using Backend.DTO;
using Backend.Models;
using Backend.Services;
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
        public IActionResult Get()
        {
            var x = _service.GetAllBookingsByCustomerId(1);
            return Ok(x);
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
