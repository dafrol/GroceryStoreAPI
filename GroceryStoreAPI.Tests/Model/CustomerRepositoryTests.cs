using GroceryStoreAPI.Models;
using NUnit.Framework;

namespace GroceryStoreAPI.Tests.Model
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        [Test]
        public void AddCustomerFails_With_Invalid_Data()
        {
            CustomerRepository repo = new CustomerRepository();
            
            Customer customerNoName = new Customer() { Id = 1, Name = "" };
            Customer customerInvalidId = new Customer() { Id = -1, Name = "Fred" };
            
            Customer noNameResult = repo.AddCustomer(customerNoName);
            Customer invalidIdResult = repo.AddCustomer(customerInvalidId);
            
            Assert.That(noNameResult == null);
            Assert.That(invalidIdResult == null);
        }

        [Test]
        public void AddCustomerIsSuccessful_With_Valid_Data()
        {
            CustomerRepository repo = new CustomerRepository();

            Customer newCustomer = new Customer() { Id = 1, Name = "Fred" };
            Customer result = repo.AddCustomer(newCustomer);

            Assert.That(result == newCustomer);
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
