using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShopBL;
using ShopModel;

namespace ShopAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {

        private IStores stores;
        private IMemoryCache cache;

        public StoreController(IStores stores, IMemoryCache cache)
        {
            this.stores = stores;
            this.cache = cache;
        }

        [HttpGet]
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

        [HttpGet("OrderHistory")]
        public async Task<IActionResult> ViewOrderHistory(int storeId)
        {
            try
            {
                //stores.
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

    }
}