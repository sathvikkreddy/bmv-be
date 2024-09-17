using Backend.DTO.Provider;
using Backend.Models;

namespace Backend.Services
{
    public interface IProviderService
    {
        bool AddProvider(Provider p);
        bool DeleteProvider(int id);
        List<Provider> GetAllProviders();
        Provider GetProviderById(int id);
        Provider UpdateProvider(int id, Provider p);
        bool ValidateProvider(ProviderLoginDTO p);
    }
}