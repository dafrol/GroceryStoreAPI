
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GroceryStoreAPI.Models
{
    public class CustomersModel
    {
        public ICollection<Customer> Customers
        {
            get { return customers; }
            set { customers = value; }
        }

        [JsonProperty(PropertyName = "customers")]
        private ICollection<Customer> customers;
    
    }
    public class Customer
    {
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [JsonProperty(PropertyName = "id")]
        private int id;
        [JsonProperty(PropertyName = "name")]
        private string name;
    }
}
