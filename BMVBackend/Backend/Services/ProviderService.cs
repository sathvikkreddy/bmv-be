using Backend.Models;

namespace Backend.Services
{
    public class ProviderService
    {
        private readonly BmvContext _bmvContext = new BmvContext();
        public List<Provider> GetAllProviders()
        {
            return _bmvContext.Providers.ToList();
        }
        public Provider GetProviderById(int id)
        {
            return _bmvContext.Providers.Find(id);
        }
        public bool AddProvider(Provider p)
        {
            try
            {
                _bmvContext.Providers.Add(p);
                _bmvContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateProvider(int id,Provider p)
        {
            var dp = _bmvContext.Providers.Find(id);
            if (dp != null)
            {
                dp.Email = p.Email;
                dp.Mobile = p.Mobile;
                dp.Password = p.Password;
                dp.CreatedAt = p.CreatedAt;
                _bmvContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
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
