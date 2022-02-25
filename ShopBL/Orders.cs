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
        public Order PlaceOrder(int customerId, List<CartItem> items, int storeId)
        {
            if (items.Count == 0)
                throw new Exception("You don't have anything in your cart!");
            foreach(CartItem item in items)
            {
                if (item.Quantity == 0)
                    throw new Exception("You must have at-least one quantity for "+ repo.GetProductFromOrder(item.Item.Id).Name +"!");
            }
            return repo.PlaceOrder(customerId, items, storeId);
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