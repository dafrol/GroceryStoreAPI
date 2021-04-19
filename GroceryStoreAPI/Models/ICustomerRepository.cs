using System.Collections.Generic;

namespace GroceryStoreAPI.Models
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Customers { get; }
        Customer AddCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
    }
}
