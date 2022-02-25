using ShopModel;

namespace ShopDL
{
    
    /*
     * An interface that deals with the loading and placing of orders.
     */
    public interface IOrderRepo
    {

        /// <summary>
        /// Places a customer order.
        /// </summary>
        /// <param name="items">The list of items being bought.</param>
        /// <param name="_storeName">The store name of this purchase.</param>
        /// <param name="totalPrice">The total price of the cart value.</param>
        Order PlaceOrder(int customerId, List<CartItem> items, int storeId);

        /// <summary>
        /// The complete list of orders in a database.
        /// </summary>
        /// <returns>The list of orders.</returns>
        List<Order> GetOrders();

        double GetCartTotal(List<CartItem> items);

        Product GetProductFromOrder(int productId);
    }
}