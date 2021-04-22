using System.Collections.Generic;

namespace GroceryStoreAPI.Models
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(int id);
        KeyValuePair<string, Customer> AddCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
    }
}
