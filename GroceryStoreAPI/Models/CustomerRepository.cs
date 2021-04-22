using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GroceryStoreAPI.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        CustomersModel _customersModel;
        public CustomerRepository()
        {
            string _jsonString = File.ReadAllText("database.json");
            _customersModel = JsonConvert.DeserializeObject<CustomersModel>(_jsonString);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>(_customersModel.Customers);
        }

        public Customer GetCustomer(int id)
        {
            foreach(Customer customer in _customersModel.Customers)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }
            return null;
        }

        public KeyValuePair<string, Customer> AddCustomer(Customer customer)
        {
            Customer existingCustomer = _customersModel.Customers.FirstOrDefault(x => x.Id == customer.Id);
            if (existingCustomer != null)
            {
                return new KeyValuePair<string, Customer>($"Customer Id ({customer.Id}) already exists.", customer);
            }

            if (customer.Id < 1)
            {
                return new KeyValuePair<string, Customer>($"Customer Id {customer.Id} is invalid.", customer);
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                return new KeyValuePair<string, Customer>("Customer Name is invalid.", customer);
            }

            _customersModel.Customers.Add(customer);
            _updateDatabase();

            return new KeyValuePair<string, Customer>(string.Empty, customer);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            foreach (Customer customerToUpdate in _customersModel.Customers)
            {
                if (customerToUpdate.Id == customer.Id)
                {
                    customerToUpdate.Name = customer.Name;
                    return customerToUpdate;
                }
            }
            return null;
        }

        private void _updateDatabase()
        {
            string updateString = JsonConvert.SerializeObject(_customersModel);
            File.WriteAllText("database.json", updateString);
        }

    }
}
