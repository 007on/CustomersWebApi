using CustomersWebApi.Controllers;
using CustomersWebApi.Data;
using CustomersWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CustomersWebApiTestProject
{
    [TestFixture]
    public class CustomersApiControllerTests
    {
        private CustomersApiController CreateTestObject(Mock<IModel> mockModel)
        {
            return new CustomersApiController(mockModel.Object);
        }

        [Test]
        public void CustomersApiController_Constructor_BadArgumentTest()
        {
            Assert.Catch<ArgumentNullException>(() => new CustomersApiController(null));
        }

        [Test]
        public void CustomersApiController_Constructor_ValidArgumentTest()
        {
            var mockModel = new Mock<IModel>();

            CustomersApiController target = null;

            Assert.DoesNotThrow(() => target = new CustomersApiController(mockModel.Object));
            Assert.AreEqual(mockModel.Object, target.Model);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task CustomersApiController_GetCustomerById_ValidIdTest(int id)
        {
            var mockModel = new Mock<IModel>();
            mockModel
                .Setup(m => m.FindCustomerByIdAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new CustomerDTO()));
            var target = CreateTestObject(mockModel);

            var result = await target.GetCustomerById(id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ObjectResult);
            mockModel.Verify(m => m.FindCustomerByIdAsync(id), Times.Once);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task CustomersApiController_GetCustomerById_InvalidIdTest(int id)
        {
            var mockModel = new Mock<IModel>();
            mockModel
                .Setup(m => m.FindCustomerByIdAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new CustomerDTO(null)));
            var target = CreateTestObject(mockModel);

            var result = await target.GetCustomerById(id);

            Assert.IsInstanceOf<NotFoundResult>(result);
            mockModel.Verify(m => m.FindCustomerByIdAsync(id), Times.Once);
        }

        [Test]
        public async Task CustomersApiController_AddCustomer_ValidArgumentTest()
        {
            var expectedCustomer = new CustomerDTO()
            {
                BirthDate = "11.11.1111",
                FirstName = "test",
                LastName = "test"
            };
            var mockMofdel = new Mock<IModel>();
            mockMofdel
                .Setup(m => m.AddCustomerAsync(It.IsAny<CustomerDTO>()))
                .Returns(() => Task.FromResult(0));
            var target = CreateTestObject(mockMofdel);

            var result = await target.AddCustomer(expectedCustomer);

            Assert.IsInstanceOf<StatusCodeResult>(result);
            mockMofdel.Verify(m => m.AddCustomerAsync(expectedCustomer), Times.Once);
        }

        [Test]
        public async Task CustomersApiController_AddCustomer_WithInvalidBirthDateTest()
        {
            var expectedCustomer = new CustomerDTO()
            {
                BirthDate = "",
                FirstName = "test",
                LastName = "test"
            };
            var mockMofdel = new Mock<IModel>();
            mockMofdel
                .Setup(m => m.AddCustomerAsync(It.IsAny<CustomerDTO>()))
                .Returns(() => Task.FromResult(0));
            var target = CreateTestObject(mockMofdel);

            var result = await target.AddCustomer(expectedCustomer);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            mockMofdel.Verify(m => m.AddCustomerAsync(expectedCustomer), Times.Never);
        }

        [Test]
        public async Task CustomersApiController_AddCustomer_WithInvalidFirstNameTest()
        {
            var expectedCustomer = new CustomerDTO()
            {
                BirthDate = "11.11.1111",
                FirstName = "",
                LastName = "test"
            };
            var mockMofdel = new Mock<IModel>();
            mockMofdel
                .Setup(m => m.AddCustomerAsync(It.IsAny<CustomerDTO>()))
                .Returns(() => Task.FromResult(0));
            var target = CreateTestObject(mockMofdel);

            var result = await target.AddCustomer(expectedCustomer);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            mockMofdel.Verify(m => m.AddCustomerAsync(expectedCustomer), Times.Never);
        }

        [Test]
        public async Task CustomersApiController_AddCustomer_WithInvalidLastNameTest()
        {
            var expectedCustomer = new CustomerDTO()
            {
                BirthDate = "11.11.1111",
                FirstName = "test",
                LastName = ""
            };
            var mockMofdel = new Mock<IModel>();
            mockMofdel
                .Setup(m => m.AddCustomerAsync(It.IsAny<CustomerDTO>()))
                .Returns(() => Task.FromResult(0));
            var target = CreateTestObject(mockMofdel);

            var result = await target.AddCustomer(expectedCustomer);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            mockMofdel.Verify(m => m.AddCustomerAsync(expectedCustomer), Times.Never);
        }
    }
}