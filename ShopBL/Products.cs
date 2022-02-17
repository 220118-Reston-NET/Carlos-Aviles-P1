using ShopModel;
using ShopDL;

namespace ShopBL
{
    
    /*
     * Represents the handling of the product repository.
     */
    public class Products : IProducts
    {

        /* The product repository interface instance. */
        private IProductRepo repo;

        /// <summary>
        /// Instantiates a new products instance.
        /// </summary>
        /// <param name="repo">The products repository interface instance.</param>
        public Products(IProductRepo repo)
        {
            this.repo = repo;
        }

        public List<Product> GetProducts()
        {
            return repo.GetProducts();
        }
    }
}