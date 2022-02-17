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
    }
}