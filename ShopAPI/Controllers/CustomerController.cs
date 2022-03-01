using System.Runtime.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
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
        [Authorize(Roles = "Customer,Admin")]
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
                Log.Information("Successfully got all customers");
                return Ok(customers.GetCustomers());
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return StatusCode(422, e.Message);
            }
        }

        [Route("{id:int}")]
        [HttpGet]
        [SwaggerOperation(Summary="Retrieves an existing customer using an id")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                Log.Information("Successfully retrieved customer with the id "+ id);
                return Ok(customers.GetCustomerFromId(id));
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return NotFound();
            }
        }

        [Route("{name}")]
        [HttpGet]
        [SwaggerOperation(Summary="Retrieves an existing customer using a name")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetCustomerByName(string name)
        {
            try
            {
                Log.Information("Successfully retrieved customer with the name "+ name);
                return Ok(customers.GetCustomerByName(name));
            }
            catch (Exception e)
            {
                return StatusCode(422, e.Message);
            }
        }

        [HttpPost("Add")]
        [SwaggerOperation(Summary="Adds a customer to the database")]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            try
            {
                Log.Information("Successfully added customer with the name "+ customer.Name);
                return Created("Successfully added", customers.AddCustomer(customer));
            }
            catch(Exception e)
            {
                Log.Warning(e.Message);
                return StatusCode(422, e.Message);
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary="Updates a customer in the database using JSON")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> Put([FromBody] Customer customer)
        {
            try
            {
                Log.Information("Successfully updated customer with the name "+ customer.Name);
                return Ok(customers.UpdateCustomer(customer));
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return StatusCode(422, e.Message);
            }
        }

        [Route("Delete/")]
        [HttpDelete]
        [SwaggerOperation(Summary="Deletes an existing customer from the database")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] Customer customer)
        {
            try
            {
                Log.Information("Successfully deleted customer with the name "+ customer.Name);
                return Ok(customers.DeleteCustomer(customer));
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return StatusCode(422, e.Message);
            }
        }
        
        [HttpPost("PlaceOrder")]
        [SwaggerOperation(Summary="Places an order for a customer")]
        [Authorize(Roles = "Customer,Admin")]
        public IActionResult PlaceOrder(int customerId, [FromBody] List<CartItem> items, int storeId)
        {
            try
            {
                Log.Information("Successfully placed order for customer with id "+ customerId);
                return Created("Created order!", orders.PlaceOrder(customerId, items, storeId));
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return StatusCode(422, e.Message);
            }
        }

        [HttpGet("ViewOrderHistory")]
        [SwaggerOperation(Summary = "Views a particular customer's order history")]
        [Authorize(Roles = "Customer,Admin")]
         public async Task<IActionResult> ViewOrder(int customerId, OrderSort type)
        {
            try
            {
                switch(type)
                {
                    case OrderSort.Oldest:
                     List<Order> oldest = customers.GetOrders(customerId).OrderBy(order => order.DateCreated).ToList();
                        return Ok(oldest);
                    case OrderSort.Recent:
                        List<Order> recent = customers.GetOrders(customerId).OrderBy(order => order.DateCreated).ToList();
                        recent.Reverse();
                        return Ok(recent);
                    case OrderSort.Total:
                        List<Order> total = customers.GetOrders(customerId);
                        total = total.OrderBy(order => order.Price).ToList();
                        total.Reverse();
                        return Ok(total);
                }
                return Ok(customers.GetOrders(customerId));
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return StatusCode(422, e.Message);
            }
        }
    }
}