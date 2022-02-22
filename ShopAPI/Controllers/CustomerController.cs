using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShopBL;
using ShopModel;

namespace ShopAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private ICustomers customers;
        private IOrders orders;

        private IMemoryCache cache;

        public CustomerController(ICustomers customers, IOrders orders, IMemoryCache cache)
        {
            this.customers = customers;
            this.orders = orders;
            this.cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                List<Customer> listOfCustomers = new List<Customer>();
                if (!cache.TryGetValue("customersList", out listOfCustomers))
                {
                    listOfCustomers = await customers.GetCustomersAsync();
                    cache.Set("customersList", listOfCustomers, new TimeSpan(0,0,30));
                }
                return Ok(customers.GetCustomers());
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerById([FromQuery] int id)
        {
            try
            {
                return Ok(customers.GetCustomerFromId(id));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("{name}")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerByName([FromQuery] string name)
        {
            try
            {
                return Ok(customers.GetCustomerByName(name));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            try
            {
                return Created("Successfully added", customers.AddCustomer(customer));
            }
            catch(Exception e)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Customer customer)
        {
            try
            {
                return Ok(customers.UpdateCustomer(customer));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("Delete/")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Customer customer)
        {
            try
            {
                return Ok(customers.DeleteCustomer(customer));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        
        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder(int customerId, [FromBody] List<CartItem> items, int storeId)
        {
            try
            {
                return Created("Created order!", orders.PlaceOrder(customerId, items, storeId));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}