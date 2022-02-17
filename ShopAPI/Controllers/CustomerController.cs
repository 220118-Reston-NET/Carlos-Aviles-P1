using Microsoft.AspNetCore.Mvc;
using ShopBL;
using ShopModel;

namespace ShopAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private ICustomers customers;

        public CustomerController(ICustomers customers)
        {
            this.customers = customers;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                return Ok(customers.GetCustomers());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{name}", Name = "GetCustomerByName")]
        public IActionResult GetCustomerByName(string name)
        {
            try
            {
                return Ok(customers.GetCustomerByName(name));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Add")]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                return Created("Successfully added", customers.AddCustomer(customer));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Customer customer)
        {
            try
            {
                return Ok(customers.UpdateCustomer(customer));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}