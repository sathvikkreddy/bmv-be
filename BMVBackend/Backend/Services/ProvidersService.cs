using Backend.DTO;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ProvidersService : IProviderService
    {
        private readonly BmvContext _bmvContext = new BmvContext();
        public List<Provider> GetAllProviders()
        {
            try
            {
                return _bmvContext.Providers.Include("Bookings").Include("Venues")
                    .ToList();
            }
            catch 
            {
                return null;
            }
        }
        public Provider GetProviderById(int id)
        {
            try
            {
                return _bmvContext.Providers.Include("Bookings").Include("Venues").Where(p=>p.Id==id).ToList().FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public bool AddProvider(Provider p)
        {
            try
            {
                _bmvContext.Providers.Add(p);
                int x  = _bmvContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Provider UpdateProvider(int id, Provider p)
        {
            Provider cProvider;
            try
            {
                cProvider = _bmvContext.Providers.Find(id);
                if (cProvider == null) { 
                    return null ;
                }
            }
            catch 
            {
                return null;
            }
            cProvider.Email = p.Email==null? cProvider.Email : p.Email;
            cProvider.Mobile = p.Mobile == null ? cProvider.Mobile : p.Mobile;
            cProvider.Password = p.Password == null ? cProvider.Password : p.Password;
            cProvider.Name = p.Name == null ? cProvider.Name : p.Name;
            try
            {
                _bmvContext.SaveChanges();
                return cProvider;
            }
            catch
            {
                return null;
            }
        }
        public bool DeleteProvider(int id)
        {
            var dp = _bmvContext.Providers.Find(id);
            if (dp != null)
            {
                _bmvContext.Providers.Remove(dp);
                _bmvContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
