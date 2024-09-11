using Backend.Models;

namespace Backend.Services
{
    public class VenueService
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
        public bool AddVenue(Venue v)
        {
            try
            {
                _bmvContext.Venues.Add(v);
                _bmvContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateVenue(int id, Venue v)
        {
            var uv = _bmvContext.Venues.Find(id);
            if (uv != null)
            {
                uv.City = v.City;
                uv.CreatedAt = v.CreatedAt;
                uv.Address = v.Address;
                uv.IsAcceptingBookings = v.IsAcceptingBookings;
                uv.Description = v.Description;
                uv.Rating = v.Rating;
                uv.ProviderId = v.ProviderId;
                _bmvContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
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
}
