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
    [SwaggerTag("Everything about stores")]
    public class StoreController : ControllerBase
    {

        private IStores stores;

        private IMemoryCache cache;
        private ICustomers customers;

        public StoreController(IStores stores, ICustomers customers, IMemoryCache cache)
        {
            this.stores = stores;
            this.customers = customers;
            this.cache = cache;
        }

        [HttpGet]
        [SwaggerOperation(Summary="Gets all stores from the database")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetAllStores()
        {
            try
            {
                List<StoreFront> listOfStores = new List<StoreFront>();
                if (!cache.TryGetValue("storesList", out listOfStores))
                {
                    listOfStores = await stores.GetStoresAsync();
                    cache.Set("storesList", listOfStores, new TimeSpan(0, 0, 30));
                }
                Log.Information("Successfully grabed all stores list.");
                return Ok(stores.GetStores());
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return NotFound();
            }
        }

        [HttpGet("{id}", Name = "GetStoreById")]
        [SwaggerOperation(Summary="Retrieves a store using an id")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetStoreById(int id)
        {
            try
            {
                Log.Information("Successfully grabed store with the id "+ id);
                return Ok(stores.GetStoreFront(id));
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return NotFound();
            }
        }

        [HttpPost("Add")]
        [SwaggerOperation(Summary="Adds a new store to the database")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] StoreFront store)
        {
            try
            {
                Log.Information("Successfully created a new store "+ store.Name);
                return Created("Successfully added", stores.AddStore(store));
            }
            catch(Exception e)
            {
                Log.Warning(e.Message);
                return StatusCode(422, e.Message);
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary="Updates store data to the database")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] StoreFront store)
        {
            try
            {
                Log.Information("Successfully updated store "+ store.Name);
                return Ok(stores.UpdateStore(store));
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return StatusCode(422, e.Message);
            }
        }

        [HttpPost("ReplenishInventory")]
        [SwaggerOperation(Summary="Updates a store's inventory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReplenishInventory(int storeId, int productId, int quantity)
        {
            try
            {
                Log.Information("Successfully replenished inventory for store with id "+ storeId);
                return Ok(stores.UpdateStoreInventory(storeId, productId, quantity));
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return StatusCode(422, e.Message);
            }
        }

        [HttpGet("ViewOrderHistory")]
        [SwaggerOperation(Summary="Views all orders made in a specific store")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ViewOrders(int storeId, OrderSort sort)
        {
            try
            {
                switch(sort)
                {
                    case OrderSort.Recent:
                        List<Order> recent = stores.GetOrders(storeId);
                        recent.Reverse();
                        return Ok(recent);
                    case OrderSort.Total:
                        List<Order> total = stores.GetOrders(storeId);
                        total = total.OrderBy(order => order.Price).ToList();
                        total.Reverse();
                        return Ok(total);
                    case OrderSort.Customer:
                        List<Order> nameOrders = new List<Order>();
                        List<Customer> custs = new List<Customer>();
                        foreach (Order _order in stores.GetStores().Where(s => s.Id == storeId).First().Orders)
                        {
                            Customer _customer = customers.GetCustomerFromOrder(_order.Id);
                            if (!custs.Exists(c => c.Id == _customer.Id))
                            {
                                List<Order> orders = customers.GetOrders(_customer.Id);
                                foreach (Order order in orders)
                                    nameOrders.Add(order);
                                custs.Add(_customer);
                            }
                        }
                        return Ok(nameOrders);
                }
                return Ok(stores.GetOrders(storeId));
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return NotFound();
            }
        }

    }
}