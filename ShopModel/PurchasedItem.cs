namespace ShopModel
{
    /*
     * Represents an item in the customer's inventory.
     */
    public class PurchasedItem
    {

        /* The unique identification of this order. */
        private int _orderId;
        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        /* The product instance. */
        private Product _item;
        public Product Item
        {
            get { return _item; }
            set { _item = value; }
        }

        /* The quantity amount of the product trying to be purchased. */
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        /// <summary>
        /// Instantiates a new purchased item instance.
        /// </summary>
        public PurchasedItem()
        {
            
        }

        /// <summary>
        /// Instantiates a new cart item instance.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <param name="product">The product instance.</param>
        /// <param name="quantity">The amount being purchased.</param>
        public PurchasedItem(int orderId, Product product, int quantity)
        {
            OrderId = orderId;
            Item = product;
            Quantity = quantity;
        }
    }
}