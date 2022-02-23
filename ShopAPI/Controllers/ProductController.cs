using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShopBL;
using ShopModel;
using Swashbuckle.AspNetCore.Annotations;

namespace ShopAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Everything about products")]
    public class ProductController : ControllerBase
    {

        private IProducts products;

        private IMemoryCache cache;

        public ProductController(IProducts products, IMemoryCache cache)
        {
            this.products = products;
            this.cache = cache;
        }

        [HttpGet]
        [SwaggerOperation(Summary="Gets all products from the database")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                List<Product> listOfProducts = new List<Product>();
                if (!cache.TryGetValue("productsList", out listOfProducts))
                {
                    listOfProducts = await products.GetProductsAsync();
                    cache.Set("productsList", listOfProducts, new TimeSpan(0,0,30));
                }
                return Ok(products.GetProducts());
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("{id:int}")]
        [HttpGet]
        [SwaggerOperation(Summary="Retrieves a product using an id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                return Ok(products.GetProducts().Where(p => p.Id == id));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("{name}")]
        [HttpGet]
        [SwaggerOperation(Summary="Retrieves a product using a name")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            try
            {
                return Ok(products.GetProducts().Where(p => p.Name == name));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

    }
}