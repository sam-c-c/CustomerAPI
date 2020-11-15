using CustomerApi.Controllers;
using CustomerApi.Data.Entities;
using CustomerApi.Data.Providers;
using CustomerApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace CustomerApi.UnitTests
{
    [TestClass]
    public class CustomerControllerTests
    {
        private Mock<ICustomerDataProvider> mockCustomerDataProvider;
        private CustomerController controller;

        [TestInitialize]
        public void BeforeEachTest()
        {
            mockCustomerDataProvider = new Mock<ICustomerDataProvider>();
            controller = new CustomerController(mockCustomerDataProvider.Object);
        }

        [TestMethod]
        public void AddCustomer_CustomerHasMultipleMainAddresses_Returns400Response()
        {
            // Arrange
            var address = new AddressModel() { IsMainAddress = true };
            var addresses = new List<AddressModel>() { address, address };

            // Act
            var result = controller.AddCustomer(new CustomerModel() { Addresses = addresses }) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            var message = result.Value as string;
            message.Should().NotBeNullOrEmpty();
            message.Should().Contain("Customer has multiple main addresses");
        }

        [TestMethod]
        public void AddCustomer_MultipleAddressesNoMainAddress_Returns400Response()
        {
            // Arrange
            var address = new AddressModel();
            var addresses = new List<AddressModel>() { address, address };

            // Act
            var result = controller.AddCustomer(new CustomerModel() { Addresses = addresses }) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            var message = result.Value as string;
            message.Should().NotBeNullOrEmpty();
            message.Should().Contain("Customer has no main address");
        }

        [TestMethod]
        public void AddCustomer_CustomerDataProviderThrowsException_Returns500Response()
        {
            // Arrange
            mockCustomerDataProvider.Setup(x => x.DoesCustomerAlreadyExist(It.IsAny<Customer>())).Throws(new Exception());

            // Act
            var result = controller.AddCustomer(new Models.CustomerModel()) as StatusCodeResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [TestMethod]
        public void AddCustomer_CustomerAlreadyExists_Returns400Response()
        {
            // Arrange
            mockCustomerDataProvider.Setup(x => x.DoesCustomerAlreadyExist(It.IsAny<Customer>())).Returns(true);

            // Act
            var customerModel = new CustomerModel() { Addresses = new List<AddressModel>() { new AddressModel() { IsMainAddress = true } } };
            var result = controller.AddCustomer(customerModel) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            var message = result.Value as string;
            message.Should().NotBeNullOrEmpty();
            message.Should().Contain("Customer already exists");
        }

        [TestMethod]
        public void AddCustomer_AddsCustomer_Returns200ResponseWithId()
        {
            // Arrange
            mockCustomerDataProvider.Setup(x => x.DoesCustomerAlreadyExist(It.IsAny<Customer>())).Returns(false);
            mockCustomerDataProvider.Setup(x => x.AddCustomer(It.IsAny<Customer>())).Returns(202);

            // Act
            var customerModel = new CustomerModel() { Addresses = new List<AddressModel>() { new AddressModel() { IsMainAddress = true } } };
            var result = controller.AddCustomer(customerModel) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            var message = (int)result.Value;
            message.Should().NotBe(0);
            message.Should().Be(202);
        }

        [TestMethod]
        public void DeleteCustomer_DataProviderThrowsException_Returns500Response()
        {
            // Arrange
            mockCustomerDataProvider.Setup(x => x.DeleteCustomer(It.IsAny<int>())).Throws(new Exception());

            // Act
            var result = controller.DeleteCustomer(1000) as StatusCodeResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [TestMethod]
        public void DeleteCustomer_DeleteIsSuccesful_Returns200Response()
        {
            // Arrange

            // Act
            var result = controller.DeleteCustomer(1000) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public void SetCustomerStatus_DataProviderThrowsException_Returns500Response()
        {
            // Arrange
            mockCustomerDataProvider.Setup(x => x.UpdateCustomerIsActiveFlag(It.IsAny<int>(), It.IsAny<bool>())).Throws(new Exception());

            // Act
            var result = controller.UpdateCustomerIsActiveFlag(1000, true) as StatusCodeResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [TestMethod]
        public void SetCustomerStatus_UpdateIsSuccesful_Returns200Response()
        {
            // Arrange

            // Act
            var result = controller.UpdateCustomerIsActiveFlag(1000, true) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public void GetAllCustomers_DataProviderThrowsException_Returns500Response()
        {
            // Arrange
            mockCustomerDataProvider.Setup(x => x.GetAllCustomers()).Throws(new Exception());

            // Act
            var result = controller.GetAllCustomers() as StatusCodeResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [TestMethod]
        public void GetAllCustomers_UpdateIsSuccesful_Returns200ResponseWithData()
        {
            // Arrange
            var customer = new Customer();
            var customers = new List<Customer>() { customer, customer };
            mockCustomerDataProvider.Setup(x => x.GetAllCustomers()).Returns(customers);

            // Act
            var result = controller.GetAllCustomers() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            var data = result.Value as List<Customer>;
            data.Should().NotBeNull();
            data.Count.Should().Be(2);
        }

        [TestMethod]
        public void GetActiveCustomers_DataProviderThrowsException_Returns500Response()
        {
            // Arrange
            mockCustomerDataProvider.Setup(x => x.GetAllCustomers()).Throws(new Exception());

            // Act
            var result = controller.GetActiveCustomers() as StatusCodeResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [TestMethod]
        public void GetActiveCustomers_UpdateIsSuccesful_Returns200ResponseWithData()
        {
            // Arrange
            var activeCustomer = new Customer() { IsActive = true };
            var notActiveCustomer = new Customer();
            var customers = new List<Customer>() { activeCustomer, notActiveCustomer };
            mockCustomerDataProvider.Setup(x => x.GetAllCustomers()).Returns(customers);

            // Act
            var result = controller.GetActiveCustomers() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            var data = result.Value as List<Customer>;
            data.Should().NotBeNull();
            data.Count.Should().Be(1);
        }
    }
}
