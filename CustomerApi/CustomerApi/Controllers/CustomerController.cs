using System;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    //[Authorize]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public CustomerController()
        {

        }

        [HttpPost("api/AddCustomer")]
        public IActionResult AddCustomer()
        {
            throw new NotImplementedException();
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
