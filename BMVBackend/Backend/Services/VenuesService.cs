using Backend.DTO.Venue;
using Backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class VenuesService : IVenuesService
    {
        private readonly BmvContext _bmvContext = new BmvContext();
        public List<Venue> GetAllVenues()
        {
            return _bmvContext.Venues.ToList();
        }
        public List<Venue> GetTopRatedVenues()
        {
            var topRatedVenues = _bmvContext.Venues.OrderByDescending(v => v.Rating).Take(5).ToList();
            return topRatedVenues;
        }
        public List<Venue> GetTopBookedVenues()
        {
            var topBookedVenues = _bmvContext.Venues.Include(v => v.Bookings).OrderByDescending(v => v.Bookings.Count).Take(5).ToList();
            return topBookedVenues;
        }
        public Venue GetVenueById(int id)
        {
            return _bmvContext.Venues.Find(id);
        }
        public Venue AddVenue(int id, PostVenueDTO venueWithSlotDetails)
        {
            if (venueWithSlotDetails.slotDetails.DurationInMinutes < 15)
            {
                return null;
            }
            List<SlotDetails> slots = new List<SlotDetails>();

            var openingTime = TimeOnly.Parse(venueWithSlotDetails.slotDetails.OpeningTime);
            var closingTime = TimeOnly.Parse(venueWithSlotDetails.slotDetails.ClosingTime);

            var currentTime = openingTime;

            while (currentTime.AddMinutes(venueWithSlotDetails.slotDetails.DurationInMinutes - 1) <= closingTime)
            {
                TimeOnly endTime = currentTime.AddMinutes(venueWithSlotDetails.slotDetails.DurationInMinutes - 1);
                var newSlot = new SlotDetails() { Start = currentTime, End = endTime, WeekdayPrice = venueWithSlotDetails.slotDetails.WeekdayPrice, WeekendPrice = venueWithSlotDetails.slotDetails.WeekendPrice };

                slots.Add(newSlot);
                currentTime = endTime.AddMinutes(1);
            }

            Venue v = new Venue();

            v.Name = venueWithSlotDetails.Name;
            v.Description = venueWithSlotDetails.Description;
            v.Address = venueWithSlotDetails.Address;
            v.City = venueWithSlotDetails.City;
            v.Latitude = venueWithSlotDetails.Latitude;
            v.Longitude = venueWithSlotDetails.Longitude;
            v.ProviderId = 1;
            v.Image1 = "";
            v.Image2 = "";
            v.Image3 = "";
            Category c = _bmvContext.Categories.Where(c => c.Name == venueWithSlotDetails.Category).FirstOrDefault();
            if (c == null)
            {
                c = new Category() { Name = venueWithSlotDetails.Category };
                _bmvContext.Categories.Add(c);
            }
            //c = _bmvContext.Categories.Where(c => c.Name == venueWithSlotDetails.Category).FirstOrDefault();
            try
            {
            _bmvContext.SaveChanges();
            }
            catch
            {
                return null;
            }
            v.CategoryId = c.Id;
            _bmvContext.Venues.Add(v);
            try
            {
                _bmvContext.SaveChanges();
            }
            catch
            {
                return null;
            }
            foreach (var slot in slots)
            {
                _bmvContext.Slots.Add(new Slot() { Start = slot.Start, End = slot.End, VenueId = v.Id, WeekdayPrice = slot.WeekdayPrice, WeekendPrice = slot.WeekendPrice });
            }
            try
            {
                _bmvContext.SaveChanges();
            }
            catch
            {
                return null;
            }
            return v;
        }
        public Venue UpdateVenue(int id, PutVenueDTO v)
        {
            var uv = _bmvContext.Venues.Find(id);
            if (uv != null)
            {
                uv.City = v.City == null ? uv.City : v.City;
                uv.Address = v.Address == null ? uv.Address : v.Address;
                uv.Description = v.Description == null ? uv.Address : v.Description;
                uv.Name = v.Name == null ? uv.Name : v.Name;
                Console.WriteLine("hi" + v.Rating.ToString());
                uv.Rating = v.Rating == null ? uv.Rating : (float)v.Rating;
                uv.Latitude = v.Latitude == null ? uv.Latitude : (float)uv.Latitude;
                uv.Longitude = v.Longitude == null ? uv.Longitude : (float)uv.Longitude;
                uv.Image1 = v.Image1 == null ? uv.Image1 : v.Image1;
                uv.Image2 = v.Image2 == null ? uv.Image2 : v.Image2;
                uv.Image3 = v.Image3 == null ? uv.Image3 : v.Image3;
                _bmvContext.SaveChanges();
                return uv;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteVenue(int id)
        {
            var dv = _bmvContext.Venues.Find(id);
            if (dv != null)
            {
                _bmvContext.Venues.Remove(dv);
                _bmvContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
    public class SlotDetails
    {
        public TimeOnly Start {  get; set; }
        public TimeOnly End { get; set; }
        public double WeekdayPrice { get; set; }
        public double WeekendPrice { get; set; }
    }
}
