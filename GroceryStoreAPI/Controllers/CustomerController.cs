using GroceryStoreAPI.Models;
using GroceryStoreAPI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            Guard.VerifyArgumentNotNull(customerRepository, nameof(customerRepository));

            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IEnumerable<Customer> GetCustomers() => 
            _customerRepository.Customers;

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            foreach (Customer customer in _customerRepository.Customers)
            {
                if (customer.Id == id)
                {
                    return Ok(customer);
                }
            }
            return BadRequest($"Customer with Id {id} not found.");
        }

        [HttpPost]
        public Customer Post([FromBody] Customer customer) =>
            _customerRepository.AddCustomer(customer);
            

        [HttpPut]
        public Customer Put([FromBody] Customer customer) => 
            _customerRepository.UpdateCustomer(customer);
    }
}
