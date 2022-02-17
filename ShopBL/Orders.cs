using ShopModel;
using ShopDL;

namespace ShopBL
{

    /*
     * Represents the handling of the orders repository.
     */
    public class Orders : IOrders
    {

        /* The order repository interface instance. */
        private IOrderRepo repo;

        /// <summary>
        /// Instantiates a new orders instance.
        /// </summary>
        /// <param name="repo">The order repository interface instance.</param>
        public Orders(IOrderRepo repo)
        {
            this.repo = repo;
        }
        public void PlaceOrder(int customerId, List<CartItem> items, StoreFront store, double price)
        {
            repo.PlaceOrder(customerId, items, store, price);
        }

        public double GetCartTotal(List<CartItem> items)
        {
            return repo.GetCartTotal(items);
        }


        public List<Order> GetOrders()
        {
            return repo.GetOrders();
        }
    }
}