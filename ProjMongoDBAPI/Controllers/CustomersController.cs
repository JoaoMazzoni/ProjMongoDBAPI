using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjMongoDBAPI.Models;
using ProjMongoDBAPI.Services;

namespace ProjMongoDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<List<Customer>> Get() => _customerService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCustomById")]
        public ActionResult<Customer> Get(string Id)
        {
            var customer = _customerService.Get(Id);
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> Post(Customer customer)
        {
            var c = _customerService.Create(customer);

            if(customer == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("GetCustomerById", new {id = customer.Id}, c);  
            
        }
    }
}
