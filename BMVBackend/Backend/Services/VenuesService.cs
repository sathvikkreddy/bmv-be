using Backend.DTO.Venue;
using Backend.Models;

namespace Backend.Services
{
    public class VenuesService : IVenuesService
    {
        private readonly BmvContext _bmvContext = new BmvContext();
        public List<Venue> GetAllVenues()
        {
            return _bmvContext.Venues.ToList();
        }
        public Venue GetVenueById(int id)
        {
            return _bmvContext.Venues.Find(id);
        }
        public Venue AddVenue(PostVenueDTO venueWithSlotDetails)
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
                var newSlot = new SlotDetails(currentTime, endTime);
                Console.WriteLine(currentTime.ToString() + newSlot.Start.ToString());
                slots.Add(newSlot);
                currentTime = endTime.AddMinutes(1);
            }

            Venue v = new Venue();

            v.Name = venueWithSlotDetails.Name;
            v.Description  = venueWithSlotDetails.Description;
            v.Address = venueWithSlotDetails.Address;
            v.City = venueWithSlotDetails.City;
            v.Latitude = venueWithSlotDetails.Latitude;
            v.Longitude = venueWithSlotDetails.Longitude;
            v.ProviderId = 1;
            v.Image1 = "";
            v.Image2 = "";
            v.Image3 = "";
            _bmvContext.Categories.Add(new Category() { Name=venueWithSlotDetails.Category});
            _bmvContext.SaveChanges();
            Category c = _bmvContext.Categories.Where(c => c.Name == venueWithSlotDetails.Name).FirstOrDefault();
            v.CategoryId = c.Id;
            _bmvContext.Venues.Add(v);
            _bmvContext.SaveChanges();
            int vId = _bmvContext.Venues.Where(v=>v.Name==venueWithSlotDetails.Name).FirstOrDefault().Id;
            foreach(var slot in slots)
            {
                _bmvContext.Slots.Add(new Slot() { Start=slot.Start, End=slot.End, VenueId=vId});
            }
            _bmvContext.SaveChanges();
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
                var b = v.Rating == null;
                if (!b)
                {
                    uv.Rating = uv.Rating;
                }
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
        public SlotDetails(TimeOnly start, TimeOnly end)
        {
            Start = start;
            End = end;
        }
    }
}
