using ShopModel;

namespace ShopBL
{

    /*
     * An interface that deals with loading and saving stores.
     */
    public interface IStores
    {

        /// <summary>
        /// Adds a store to the database.
        /// </summary>
        /// <param name="store">The store instance.</param>
        /// <returns>The store instance.</returns>
        StoreFront AddStore(StoreFront store);

        /// <summary>
        /// Updates a store's data in the database.
        /// </summary>
        /// <param name="store">The store instance.</param>
        /// <returns>The store instance.</returns>
        StoreFront UpdateStore(StoreFront store);

        /// <summary>
        /// Updates a specified store's inventory.
        /// </summary>
        /// <param name="storeId">The store's unique identification.</param>
        /// <param name="productId">The product's unique identification.</param>
        /// <param name="quantity">The amount to update</param>
        void UpdateStoreInventory(int storeId, int productId, int quantity);

        /// <summary>
        /// Gets all the stores from the database.
        /// </summary>
        /// <returns>The list of stores.</returns>
        List<StoreFront> GetStores();

        /// <summary>
        /// Gets all the stores from the database.
        /// </summary>
        /// <returns>The list of stores in asynchronous task.</returns>
        Task<List<StoreFront>> GetStoresAsync();

        /// <summary>
        /// Gets all the orders from a specific store from the database.
        /// </summary>
        /// <param name="storeId">The stores's unique identification.</param>
        /// <returns>The list of orders.</returns>
        List<Order> GetOrders(int storeId);

        /// <summary>
        /// Gets all the items in stock in a specified store.
        /// </summary>
        /// <param name="storeId">The store's unique identification.</param>
        /// <returns>The list of line items.</returns>
        List<LineItem> GetLineItems(int storeId);

        /// <summary>
        /// Gets a specific store from the database.
        /// </summary>
        /// <param name="storeId">The store id.</param>
        /// <returns>The store instance.</returns>
        StoreFront GetStoreFront(int storeId);

        /// <summary>
        /// Gets a list of similar stores by finding any that having a matching name.
        /// </summary>
        /// <param name="name">The store name.</param>
        /// <returns>The list of similar stores.</returns>
        List<StoreFront> GetSimilarStoresByName(string name);

        /// <summary>
        /// Gets a list of similar stores by finding any that having a matching address.
        /// </summary>
        /// <param name="address">The store address.</param>
        /// <returns>The list of similar stores.</returns>
        List<StoreFront> GetSimilarStoresByAddress(string address);

        /// <summary>
        /// A flag that check if a store has items in stock.
        /// </summary>
        /// <param name="store">The store instance</param>
        /// <returns>If there are items in stock, it will return true</returns>
        bool hasInventory(StoreFront store);

        string DisplayOrderHistory(List<Order> orders);
    }
}