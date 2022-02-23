namespace ShopModel
{

    /*
     * Represents an item that is in a customer's cart.
     */
    public class CartItem
    {

        /* The product instance. */
        private Product _item;
        public Product Item
        {
            get { return _item; }
            set { _item = value; }
        }

        /* The total quantity of said item in cart. */
        public int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public CartItem()
        {
            
        }

        /// <summary>
        /// Instantiaes a new cart item instance.
        /// </summary>
        /// <param name="product">The product instance.</param>
        /// <param name="quantity">The quantity.</param>
        public CartItem(Product product, int quantity)
        {
            Item = product;
            Quantity = quantity;
        }
    }
}