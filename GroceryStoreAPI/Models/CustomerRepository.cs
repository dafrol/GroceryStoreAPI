using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace GroceryStoreAPI.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private Dictionary<int, Customer> _customerDictionary = new Dictionary<int, Customer>();
        public CustomerRepository()
        {
            string jsonString = File.ReadAllText("database.json");
            try
            {
                JObject jsonObject = JObject.Parse(jsonString);

                JArray customerValues = (JArray)jsonObject["customers"];

                int count = 0;

                foreach (JToken customer in customerValues)
                {
                    _customerDictionary.Add(count, new Customer
                    {
                        Id = (int)customer["id"],
                        Name = customer["name"].ToString()
                    });
                    count++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Customer> Customers => _customerDictionary.Values;

        public Customer AddCustomer(Customer customer)
        {
            if (customer.Id >= 1 && !string.IsNullOrEmpty(customer.Name))
            {
                _customerDictionary.Add(_customerDictionary.Count, customer);
                return customer;
            }
            return null;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            int customerKey = -1;
            Customer customerToUpdate = new Customer();

            foreach (var item in _customerDictionary)
            {
                customerToUpdate = item.Value;
                if (customerToUpdate.Id == customer.Id)
                {
                    customerToUpdate.Name = customer.Name;
                    customerKey = item.Key;
                    break;
                }
            }
            if (customerKey >= 0)
            {
                _customerDictionary.Remove(customerKey);
                _customerDictionary.Add(customerKey, customerToUpdate);
                return customerToUpdate;
            }
            return null;
        }
    }
}
