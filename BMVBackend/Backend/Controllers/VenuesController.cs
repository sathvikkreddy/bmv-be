using Backend.DTO.Slot;
using Backend.DTO.Venue;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        IVenuesService _service;
        public VenuesController(IVenuesService service)
        {
            _service = service;
        }

        // GET: api/<VenueController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VenueController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VenueController>
        [HttpPost]
        public IActionResult Post([FromBody] PostVenueDTO venueWithSlotDetails)
        {
            var v = _service.AddVenue(venueWithSlotDetails);
            return Ok(v);
        }

        // PUT api/<VenueController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PutVenueDTO v)
        {
            var uv = _service.UpdateVenue(id, v);
            if (uv != null)
            {
                return Ok(uv);
            }
            return NotFound();
        }

        // DELETE api/<VenueController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
