using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace GroceryStoreAPI.Tests.Controllers
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private Customer _customerToAdd = new Customer() { Id = 3, Name = "Dan" };
        private Customer _customerToUpdate = new Customer() { Id = 2, Name = "Bill" };
        private Dictionary<int, Customer> _customers = new Dictionary<int, Customer>();

        #region Constructor
        [Test]
        public void Constructor_Fails_With_Invalid_Repository()
        {
            Assert.That(() => new CustomerController(null), Throws.ArgumentNullException);
        }
        [Test]
        public void Constructor_Is_Successful_With_Valid_Repository()
        {
            Assert.That(() => new CustomerController(Mock.Of<ICustomerRepository>()), Throws.Nothing);
        }
        #endregion


        [Test]
        public void GetCustomers_ReturnsAllCustomers()
        {
            Mock<ICustomerRepository> mockRepository = _getMockCustomers();
            CustomerController customerController = new CustomerController(mockRepository.Object);

            IEnumerable<Customer> result = customerController.GetCustomers();
            Assert.That(mockRepository.Object.Customers == result);
        }

        [Test]
        public void GetCustomer_ReturnsOK()
        {
            Mock<ICustomerRepository> mockRepository = _getMockCustomers();
            CustomerController customerController = new CustomerController(mockRepository.Object);
            
            Microsoft.AspNetCore.Mvc.ActionResult result = customerController.GetCustomer(1).Result;
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetInvalidCustomer_ReturnsBadRequest()
        {
            Mock<ICustomerRepository> mockRepository = _getMockCustomers();
            CustomerController customerController = new CustomerController(mockRepository.Object);

            Microsoft.AspNetCore.Mvc.ActionResult result = customerController.GetCustomer(5).Result;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void AddCustomer_ReturnsAddedCustomer()
        {
            Mock<ICustomerRepository> mockRepository = _getMockCustomers();
            CustomerController customerController = new CustomerController(mockRepository.Object);

            Customer result = customerController.Post(_customerToAdd);
            Assert.That(result == _customerToAdd);
        }

        [Test]
        public void UpdateCustomer_ReturnsUpdatedCustomer()
        {
            Mock<ICustomerRepository> mockRepository = _getMockCustomers();
            CustomerController customerController = new CustomerController(mockRepository.Object);

            Customer result = customerController.Put(_customerToUpdate);
            Assert.That(result == _customerToUpdate);
        }


        private Mock<ICustomerRepository> _getMockCustomers()
        {
            _customers = new Dictionary<int, Customer>();
            _customers.Add(0, new Customer { Id = 1, Name = "Bob" });
            _customers.Add(1, new Customer { Id = 2, Name = "Mary" });
            _customers.Add(2, new Customer { Id = 3, Name = "Joe" });

            Mock<ICustomerRepository> mock = new Mock<ICustomerRepository>();
            mock.Setup(m => m.Customers).Returns(_customers.Values);
            mock.Setup(m => m.AddCustomer(It.IsAny<Customer>())).Returns(_customerToAdd);
            mock.Setup(m => m.UpdateCustomer(It.IsAny<Customer>())).Returns(_customerToUpdate);

            return mock;
        }
    }
}
