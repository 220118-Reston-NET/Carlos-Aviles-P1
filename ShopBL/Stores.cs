using ShopDL;
using ShopModel;

namespace ShopBL
{

    /*
     * Represents the handling of the store repository.
     */
    public class Stores : IStores
    {

        /* The store repository interface instance. */
        private IStoreRepo repo;

        /// <summary>
        /// Instantiates a new stores instance.
        /// </summary>
        /// <param name="repo">The store repository interface instance.</param>
        public Stores(IStoreRepo repo)
        {
            this.repo = repo;
        }

        public StoreFront AddStore(StoreFront store)
        {
            return repo.AddStore(store);
        }

        public StoreFront UpdateStore(StoreFront store)
        {
            return repo.UpdateStore(store);
        }

        public StoreFront UpdateStoreInventory(int storeId, int productId, int quantity)
        {
            return repo.UpdateStoreInventory(storeId, productId, quantity);
        }

        public List<StoreFront> GetStores()
        {
            return repo.GetStores();
        }

        public Task<List<StoreFront>> GetStoresAsync()
        {
            return repo.GetStoresAsync();
        }

        public List<Order> GetOrders(int storeId)
        {
            return repo.GetOrders(storeId);
        }

        public List<LineItem> GetLineItems(int storeId)
        {
            return repo.GetLineItems(storeId);
        }

        public StoreFront GetStoreFront(int storeId)
        {
            return repo.GetStoreFront(storeId);
        }

        public bool hasInventory(StoreFront store)
        {
            return repo.hasInventory(store);
        }
    }
}