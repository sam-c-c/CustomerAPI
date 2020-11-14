using System;
using System.Linq;
using CustomerApi.Data.Providers;
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

        public CustomerController(ICustomerDataProvider customerDataProvider)
        {
            this.customerDataProvider = customerDataProvider;
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
                // TODO: Log the exception
                return StatusCode(500);
            }
        }

        [HttpPost("api/DeleteCustomer")]
        public IActionResult DeleteCustomer()
        {
            throw new NotImplementedException();
        }

        [HttpPost("api/GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        [HttpPost("api/GetActiveCustomers")]
        public IActionResult GetActiveCustomers()
        {
            throw new NotImplementedException();
        }

        [HttpPost("api/AddAddress")]
        public IActionResult AddAddress()
        {
            throw new NotImplementedException();
        }

        [HttpPost("api/DeleteAddress")]
        public IActionResult DeleteAddress()
        {
            throw new NotImplementedException();
        }

        [HttpPost("api/SetCustomerStatus")]
        public IActionResult SetCustomerStatus()
        {
            throw new NotImplementedException();
        }
    }
}
