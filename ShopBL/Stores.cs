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

        public void UpdateStoreInventory(int storeId, int productId, int quantity)
        {
            repo.UpdateStoreInventory(storeId, productId, quantity);
        }

        public List<StoreFront> GetStores()
        {
            return repo.GetStores();
        }

        public Task<List<StoreFront>> GetStoresAsync()
        {
            return repo.GetStoresAsync();
        }

        public List<LineItem> GetLineItems(int storeId)
        {
            return repo.GetLineItems(storeId);
        }

        public StoreFront GetStoreFront(int storeId)
        {
            return repo.GetStoreFront(storeId);
        }

        public bool SearchStoreByExactName(string name)
        {
            return repo.SearchStoreByExactName(name);
        }

        public bool SearchStoreByName(string name)
        {
            return repo.SearchStoreByName(name);
        }

        public bool SearchStoreByExactAddress(string address)
        {
            return repo.SearchStoreByExactAddress(address);
        }

        public bool SearchStoreByAddress(string address)
        {
            return repo.SearchStoreByAddress(address);
        }

        public List<StoreFront> GetSimilarStoresByName(string name)
        {
            return repo.GetSimilarStoresByName(name);
        }

        public List<StoreFront> GetSimilarStoresByAddress(string address)
        {
            return repo.GetSimilarStoresByAddress(address);
        }

        public bool hasInventory(StoreFront store)
        {
            return repo.hasInventory(store);
        }
    }
}