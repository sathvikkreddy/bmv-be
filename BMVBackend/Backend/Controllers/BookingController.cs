﻿using Backend.DTO;
using Backend.DTO.Booking;
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
        ICustomersService _customerService;
        IVenuesService _venueService;
        IProvidersService _providerService;
        public BookingController(IBookingService bookingService, ICustomersService customerService, IVenuesService venueService, IProvidersService providerService)
        {
            _service = bookingService;
            _customerService = customerService;
            _venueService = venueService;
            _providerService = providerService;
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

            var detailedBookings = bookings.Select(b => new
            {
                b.Id,
                b.CreatedAt,
                b.Status,
                b.CustomerId,
                b.ProviderId,
                b.VenueId,
                b.Amount,
                b.Date,
                b.Start,
                b.End,
                b.BookedSlots,
                CustomerName = _customerService.GetCustomerById(b.CustomerId)?.Name,
                ProviderName = _providerService.GetProviderById(b.ProviderId)?.Name,
                VenueName = _venueService.GetVenueById(b.VenueId)?.Name
            }).ToList();

            return Ok(detailedBookings);
        }

        
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
    }
}
