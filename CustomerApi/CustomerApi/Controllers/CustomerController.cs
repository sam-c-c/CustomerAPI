using System;
using System.Linq;
using CustomerApi.Data.Providers;
using CustomerApi.Logging;
using CustomerApi.Mappers;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    //[Authorize]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerDataProvider customerDataProvider;
        private readonly ILogger logger;

        public CustomerController(ICustomerDataProvider customerDataProvider, ILogger logger)
        {
            this.customerDataProvider = customerDataProvider;
            this.logger = logger;
        }

        [HttpPost("api/AddCustomer")]
        public IActionResult AddCustomer([FromBody] CustomerModel customerModel)
        {
            try
            {
                if (customerModel.Addresses.Count(x => x.IsMainAddress == true) > 1)
                { 
                    return new BadRequestObjectResult("Customer has multiple main addresses");
                }
                if (!customerModel.Addresses.Any(x => x.IsMainAddress))
                {
                    return new BadRequestObjectResult("Customer has no main address");
                }
                var customer = CustomerModelToCustomerEntityMapper.Map(customerModel);
                var doesCustomerExist = customerDataProvider.DoesCustomerAlreadyExist(customer);
                if (doesCustomerExist)
                {
                    return new BadRequestObjectResult("Customer already exists");
                }
                var customerId = customerDataProvider.AddCustomer(customer);
                return Ok(customerId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpDelete("api/DeleteCustomer/{customerId}")]
        public IActionResult DeleteCustomer(int customerId)
        {
            try
            {
                customerDataProvider.DeleteCustomer(customerId);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet("api/GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = customerDataProvider.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet("api/GetActiveCustomers")]
        public IActionResult GetActiveCustomers()
        {
            try
            {
                var customers = customerDataProvider.GetAllCustomers();
                var activeCustomers = customers.Where(x => x.IsActive == true).ToList();
                return Ok(activeCustomers);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("api/AddAddress/{customerId}")]
        public IActionResult AddAddress(int customerId, [FromBody] AddressModel addressModel)
        {
            try
            {
                var address = AddressModelToAddressEntityMapper.Map(customerId, addressModel);
                var addressId = customerDataProvider.AddAddress(address);
                return Ok(addressId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpDelete("api/DeleteAddress/{customerId}/{addressId}")]
        public IActionResult DeleteAddress(int customerId, int addressId)
        {
            try
            {
                var customer = customerDataProvider.GetCustomer(customerId);
                if (customer == null || customer == default || customer.Addresses.Count == 1)
                {
                    return BadRequest("Customer only has one address");
                }
                customerDataProvider.DeleteAddress(customerId, addressId);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPut("api/UpdateCustomerIsActiveFlag/{customerId}/{isActive}")]
        public IActionResult UpdateCustomerIsActiveFlag(int customerId, bool isActive)
        {
            try
            {
                customerDataProvider.UpdateCustomerIsActiveFlag(customerId, isActive);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
