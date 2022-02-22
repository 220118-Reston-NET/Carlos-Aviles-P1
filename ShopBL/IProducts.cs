using ShopModel;

namespace ShopBL
{

    /*
     * An interface that deals with the loading and adding of products.
     */
    public interface IProducts
    {

        /// <summary>
        /// Gets the complete list of products in the database.
        /// </summary>
        /// <returns>The list of products.</returns>
        List<Product> GetProducts();

        /// <summary>
        /// Gets all the products from the database.
        /// </summary>
        /// <returns>The list in asynchronous task.</returns>
        Task<List<Product>> GetProductsAsync();
    }
}