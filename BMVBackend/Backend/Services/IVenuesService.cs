using Backend.DTO.Venue;
using Backend.Models;

namespace Backend.Services
{
    public interface IVenuesService
    {
        Venue AddVenue(PostVenueDTO venueWithSlotDetails);
        bool DeleteVenue(int id);
        List<Venue> GetAllVenues();
        Venue GetVenueById(int id);
        Venue UpdateVenue(int id, PutVenueDTO v);
    }
}