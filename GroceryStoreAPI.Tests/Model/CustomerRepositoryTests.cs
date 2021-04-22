using GroceryStoreAPI.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Tests.Model
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        [Test]
        public void GetCustomerIsSuccessful_With_Valid_Id()
        {
            CustomerRepository repo = new CustomerRepository();

            int validId = 1;

            Customer customer = repo.GetCustomer(validId);

            Assert.That(customer != null);
        }

        [Test]
        public void GetCustomerFails_With_InValid_Id()
        {
            CustomerRepository repo = new CustomerRepository();

            int invalidId = repo.GetCustomers().ToList().Count + 1;

            Customer customer = repo.GetCustomer(invalidId);

            Assert.That(customer == null);
        }

        [Test]
        public void AddCustomerFails_With_Invalid_Data()
        {
            CustomerRepository repo = new CustomerRepository();
            
            Customer customerNoName = new Customer() { Id = 999, Name = "" };
            Customer customerInvalidId = new Customer() { Id = -1, Name = "Fred" };
            Customer customerExistingId = new Customer() { Id = 1, Name = "Fred" };
            
            KeyValuePair<string, Customer> noNameResult = repo.AddCustomer(customerNoName);
            KeyValuePair<string, Customer> invalidIdResult = repo.AddCustomer(customerInvalidId);
            KeyValuePair<string, Customer> existingIdResult = repo.AddCustomer(customerExistingId);

            Assert.That(!string.IsNullOrEmpty(noNameResult.Key) && noNameResult.Value == customerNoName);
            Assert.That(!string.IsNullOrEmpty(invalidIdResult.Key) && invalidIdResult.Value == customerInvalidId);
            Assert.That(!string.IsNullOrEmpty(existingIdResult.Key) && existingIdResult.Value == customerExistingId);
        }

        [Test]
        public void AddCustomerIsSuccessful_With_Valid_Data()
        {
            CustomerRepository repo = new CustomerRepository();

            int newCustomerId = repo.GetCustomers().ToList().Count + 1;

            Customer newCustomer = new Customer() { Id = newCustomerId, Name = "Fred" };
            KeyValuePair<string, Customer> result = repo.AddCustomer(newCustomer);

            Assert.That(string.IsNullOrEmpty(result.Key) && result.Value == newCustomer);
        }

        [Test]
        public void UpdateCustomerFails_With_Unknown_Customer_Id()
        {
            CustomerRepository repo = new CustomerRepository();

            Customer customerToUpdate = new Customer() { Id = 999, Name = "Sharon" };
            Customer result = repo.UpdateCustomer(customerToUpdate);

            Assert.That(result == null);
        }

        [Test]
        public void UpdateCustomerIsSuccessful_With_Valid_Customer_Id()
        {
            CustomerRepository repo = new CustomerRepository();

            Customer customerToUpdate = new Customer() { Id = 2, Name = "Sharon" };
            Customer result = repo.UpdateCustomer(customerToUpdate);

            Assert.That(result.Id == customerToUpdate.Id && result.Name == customerToUpdate.Name);
        }
    }
}
