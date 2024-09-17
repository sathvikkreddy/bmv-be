using Backend.DTO.Provider;
using Backend.Helpers;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        IProviderService _service;
        public ProvidersController(IProviderService providerService) {
            _service = providerService;
        }
        // GET: api/<ProviderController>
        [HttpGet]
        public IActionResult Get()
        {
            var providers = _service.GetAllProviders();
            if(providers == null)
            {
                return BadRequest();
            }
            return Ok(providers);
        }

        // GET api/<ProviderController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var provider = _service.GetProviderById(id);
            if (provider == null) {
                return NotFound();
            }
            return Ok(provider);
        }

        // POST api/<ProviderController>
        [HttpPost]
        public IActionResult Post([FromBody] PostProviderDTO provider)
        {
            Provider newProvider = new Provider();
            newProvider.Mobile = provider.Mobile;
            newProvider.Name = provider.Name;
            newProvider.Email = provider.Email;
            newProvider.Password = provider.Password;
            if (_service.AddProvider(newProvider))
            {
                return Ok(newProvider);
            }
            return BadRequest();
        }

        // PUT api/<ProviderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PutProviderDTO newProvider)
        {
            Provider provider = new Provider() { Email=newProvider.Email, Mobile= newProvider.Mobile, Name= newProvider.Name, Password= newProvider.Password};
            var updatedProvider = _service.UpdateProvider(id, provider);
            if (updatedProvider == null)
            {
                return BadRequest();
            }
            return Ok(updatedProvider);
            
        }

        // DELETE api/<ProviderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Validate(ProviderLoginDTO value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            if (_service.ValidateProvider(value))
            {

                return Ok(new TokenResult()
                {
                    Status = "success",
                    Token = new TokenHelper().GenerateProviderToken(value)
                });
            }
            return NotFound(new TokenResult() { Status = "failed", Token = null });
        }

    }
}
