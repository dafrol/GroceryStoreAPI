
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GroceryStoreAPI.Models
{
    public class CustomersModel
    {
        [JsonProperty(PropertyName = "customers")]
        public ICollection<Customer> Customers { get; set; }
    
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
        private int id { get; set; }
        [JsonProperty(PropertyName = "name")]
        private string name { get; set; }
    }
}
