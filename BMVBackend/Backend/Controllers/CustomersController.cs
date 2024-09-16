using Backend.DTO.Customer;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomersService _service;
        public CustomersController(ICustomersService usersService) 
        {
            _service = usersService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            var providers = _service.GetAllCustomers();
            if (providers == null)
            {
                return BadRequest();
            }
            return Ok(providers);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _service.GetCustomerById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] PostCustomerDTO user)
        {
            Customer newUser = new Customer();
            newUser.Mobile = user.Mobile;
            newUser.Name = user.Name;
            newUser.Email = user.Email;
            newUser.Password = user.Password;
            if (_service.AddCustomer(newUser))
            {
                return Ok(newUser);
            }
            return BadRequest();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PutCustomerDTO newUser)
        {
            Customer user = new Customer() { Email = newUser.Email, Mobile = newUser.Mobile, Name = newUser.Name, Password = newUser.Password };
            var updatedUser = _service.UpdateCustomer(id, user);
            if (updatedUser == null)
            {
                return BadRequest();
            }
            return Ok(updatedUser);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
