
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
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
