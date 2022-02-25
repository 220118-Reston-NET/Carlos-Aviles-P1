using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
                return Ok(stores.GetStores());
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}", Name = "GetStoreById")]
        [SwaggerOperation(Summary="Retrieves a store using an id")]
        public async Task<IActionResult> GetStoreById(int id)
        {
            try
            {
                return Ok(stores.GetStoreFront(id));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost("Add")]
        [SwaggerOperation(Summary="Adds a new store to the database")]
        public async Task<IActionResult> Post([FromBody] StoreFront store)
        {
            try
            {
                return Created("Successfully added", stores.AddStore(store));
            }
            catch(Exception e)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary="Updates store data to the database")]
        public async Task<IActionResult> Put([FromBody] StoreFront store)
        {
            try
            {
                return Ok(stores.UpdateStore(store));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost("ReplenishInventory")]
        [SwaggerOperation(Summary="Updates a store's inventory")]
        public async Task<IActionResult> ReplenishInventory(int storeId, int productId, int quantity)
        {
            try
            {
                stores.UpdateStoreInventory(storeId, productId, quantity);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet("ViewOrderHistory")]
        [SwaggerOperation(Summary="Views all ordres made in a specific store")]
        public async Task<IActionResult> ViewOrders(int storeId, OrderSort sort)
        {
            try
            {
                switch(sort)
                {
                    case OrderSort.MostRecent:
                        List<Order> recent = stores.GetOrders(storeId);
                        recent.Reverse();
                        return Ok(customers.DisplayOrderHistory(recent));
                    case OrderSort.TotalPrice:
                        List<Order> total = stores.GetOrders(storeId);
                        total = total.OrderBy(order => order.Price).ToList();
                        total.Reverse();
                        return Ok(customers.DisplayOrderHistory(total));
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
                        return Ok(customers.DisplayOrderHistory(nameOrders));
                }
                return Ok(stores.GetOrders(storeId));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

    }
}