using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Tests.Controllers
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private Customer _customerToAdd = new Customer() { Id = 3, Name = "Dan" };
        private Customer _customerToUpdate = new Customer() { Id = 2, Name = "Bill" };
        
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
        public void GetCustomers_ReturnsOK()
        {
            Mock<ICustomerRepository> mockRepository = _getMockCustomers();
            CustomerController customerController = new CustomerController(mockRepository.Object);

            ActionResult<IEnumerable<Customer>> result = customerController.GetCustomers().Result;
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetCustomer_ReturnsOK()
        {
            Mock<ICustomerRepository> mockRepository = _getMockCustomers();
            CustomerController customerController = new CustomerController(mockRepository.Object);
            
            ActionResult<Customer> result = customerController.GetCustomer(1).Result;
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetInvalidCustomer_ReturnsBadRequest()
        {
            Mock<ICustomerRepository> mockRepository = _getMockCustomers();
            CustomerController customerController = new CustomerController(mockRepository.Object);

            ActionResult<Customer> result = customerController.GetCustomer(999).Result;
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void AddCustomer_ReturnsAddedCustomer()
        {
            Mock<ICustomerRepository> mockRepository = _getMockCustomers();
            CustomerController customerController = new CustomerController(mockRepository.Object);

            ActionResult<Customer> result = customerController.Post(_customerToAdd);
            Assert.That(result.Value == _customerToAdd);
        }

        [Test]
        public void UpdateCustomer_ReturnsUpdatedCustomer()
        {
            Mock<ICustomerRepository> mockRepository = _getMockCustomers();
            CustomerController customerController = new CustomerController(mockRepository.Object);

            ActionResult<Customer> result = customerController.Put(_customerToUpdate);
            Assert.That(result.Value == _customerToUpdate);
        }


        private Mock<ICustomerRepository> _getMockCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer { Id = 1, Name = "Bob" });
            customers.Add(new Customer { Id = 2, Name = "Mary" });
            customers.Add(new Customer { Id = 3, Name = "Joe" });

            CustomersModel model = new CustomersModel();
            model.Customers = customers as ICollection<Customer>;
            
            Mock<ICustomerRepository> mock = new Mock<ICustomerRepository>();
            mock.Setup(m => m.GetCustomers()).Returns(model.Customers);
            mock.Setup(m => m.GetCustomer(1)).Returns(customers[1]);
            mock.Setup(m => m.AddCustomer(It.IsAny<Customer>())).Returns(new KeyValuePair<string, Customer>(string.Empty, _customerToAdd));
            mock.Setup(m => m.UpdateCustomer(It.IsAny<Customer>())).Returns(_customerToUpdate);

            return mock;
        }
    }
}
