using Backend.Models;

namespace Backend.Services
{
    public interface ICustomersService
    {
        bool AddCustomer(Customer u);
        bool DeleteCustomer(int id);
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        bool UpdateCustomer(int id, Customer u);
    }
}