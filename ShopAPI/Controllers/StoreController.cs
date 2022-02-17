using Microsoft.AspNetCore.Mvc;
using ShopBL;
using ShopModel;

namespace ShopAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {

        private IStores stores;

        public StoreController(IStores stores)
        {
            this.stores = stores;
        }

        [HttpGet]
        public IActionResult GetAllStores()
        {
            try
            {
                return Ok(stores.GetStores());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}", Name = "GetStoreById")]
        public IActionResult GetStoreById(int id)
        {
            try
            {
                return Ok(stores.GetStoreFront(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Add")]
        public IActionResult Post([FromBody] StoreFront store)
        {
            try
            {
                return Created("Successfully added", stores.AddStore(store));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] StoreFront store)
        {
            try
            {
                return Ok(stores.UpdateStore(store));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

    }
}