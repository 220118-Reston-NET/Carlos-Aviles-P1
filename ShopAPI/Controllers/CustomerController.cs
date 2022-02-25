using System.Runtime.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShopBL;
using ShopModel;
using Swashbuckle.AspNetCore.Annotations;

namespace ShopAPI.Controllers
{


    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Everything about customers")]
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
        [SwaggerOperation(Summary="Gets all customers from the database")]
        [Authorize(Roles = "Admin")]
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
        [SwaggerOperation(Summary="Retrieves an existing customer using an id")]
        public async Task<IActionResult> GetCustomerById(int id)
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
        [SwaggerOperation(Summary="Retrieves an existing customer using a name")]
        public async Task<IActionResult> GetCustomerByName(string name)
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
        [SwaggerOperation(Summary="Adds a customer to the database")]
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
        [SwaggerOperation(Summary="Updates a customer in the database using JSON")]
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
        [SwaggerOperation(Summary="Deletes an existing customer from the database")]
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
        [SwaggerOperation(Summary="Places an order for a customer")]
        public IActionResult PlaceOrder(int customerId, [FromBody] List<CartItem> items, int storeId)
        {
            try
            {
                return Created("Created order!", orders.PlaceOrder(customerId, items, storeId));
            }
            catch (Exception e)
            {
                return StatusCode(422, e.Message);
            }
        }

        [HttpGet("ViewOrderHistory")]
        [SwaggerOperation(Summary = "Views a particular customer's order history")]
         public async Task<IActionResult> ViewOrder(int customerId, OrderSort type)
        {
            try
            {
                switch(type)
                {
                    case OrderSort.MostRecent:
                        List<Order> recent = customers.GetOrders(customerId);
                        recent.Reverse();
                        return Ok(customers.DisplayOrderHistory(recent));
                    case OrderSort.TotalPrice:
                        List<Order> total = customers.GetOrders(customerId);
                        total = total.OrderBy(order => order.Price).ToList();
                        total.Reverse();
                        return Ok(customers.DisplayOrderHistory(total));
                }
                return Ok(customers.GetOrders(customerId));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}