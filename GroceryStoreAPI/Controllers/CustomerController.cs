using GroceryStoreAPI.Models;
using GroceryStoreAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return new OkObjectResult(_customerRepository.GetCustomers());
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            Customer customer = _customerRepository.GetCustomer(id);
            if (customer != null)
            {
                return new OkObjectResult(customer);
            }
            return BadRequest($"Customer with Id {id} not found.");
        }

        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            KeyValuePair<string, Customer> addedCustomer = _customerRepository.AddCustomer(customer);
            if (string.IsNullOrEmpty(addedCustomer.Key))
            {
                return addedCustomer.Value;
            }
            return BadRequest($"Error adding customer: {addedCustomer.Key}");
        }

        [HttpPut]
        public ActionResult<Customer> Put([FromBody] Customer customer)
        {
            Customer updatedCustomer = _customerRepository.UpdateCustomer(customer);
            if (updatedCustomer != null)
            {
                return updatedCustomer;
            }
            return BadRequest($"Customer with Id {customer.Id} does not exist.");
        }
    }
}
