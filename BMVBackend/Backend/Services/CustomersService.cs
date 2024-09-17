using Backend.DTO.Customer;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class CustomersService :  ICustomersService
    {
        private readonly BmvContext _bmvContext = new BmvContext();
        public List<Customer> GetAllCustomers()
        {
            List<Customer> users;
            try
            {
                users = _bmvContext.Customers.Include("Bookings").ToList();
                return users;
            }
            catch
            {
                return null;
            }
        }
        public Customer GetCustomerById(int id)
        {
            Customer user;
            try
            {
                user = _bmvContext.Customers.FirstOrDefault(x => x.Id == id);
                return user;
            }
            catch
            {
                return null;
            }
        }
        public bool AddCustomer(Customer u)
        {
            try
            {
                _bmvContext.Customers.Add(u);
                _bmvContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateCustomer(int id, Customer u)
        {
            var uu = _bmvContext.Customers.Find(id);
            if (uu != null)
            {
                uu.Email = u.Email;
                uu.Password = u.Password;
                uu.CreatedAt = u.CreatedAt;
                uu.Mobile = u.Mobile;
                _bmvContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteCustomer(int id)
        {
            var du = _bmvContext.Customers.Find(id);
            if (du != null)
            {
                _bmvContext.Customers.Remove(du);
                _bmvContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool ValidateCustomer(CustomerLoginDTO customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            var result = _bmvContext.Customers
        .FirstOrDefault(u => u.Email == customer.Email && u.Password == customer.Password);

            return result != null;
        }

    }
}
