using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjMongoDB20220714.Services;
using ProjMongoDBAPI.Models;
using ProjMongoDBAPI.Services;

namespace ProjMongoDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly AddressService _addressService;

        public CustomersController(CustomerService customerService, AddressService addressService)
        {
            _customerService = customerService;
            _addressService = addressService;
        }

        [HttpGet]
        public ActionResult<List<Customer>> Get() => _customerService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCustomById")]
        public ActionResult<Customer> Get(string Id)
        {
            var customer = _customerService.Get(Id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> Post(Customer customer)
        {
            Address address = _addressService.Get(customer.Address.Id);
            customer.Address = address;
            
            var c = _customerService.Create(customer);

            if (c == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("GetCustomerById", new { id = c.Id }, c);

        }
    }
}
