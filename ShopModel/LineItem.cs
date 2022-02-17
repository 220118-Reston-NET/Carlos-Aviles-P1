namespace ShopModel
{

    /*
     * Represents the items being sold in a store.
     */
    public class LineItem
    {

        /* The product instance. */
        private Product _product;
        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        /* The total amount of inventory of this item. */
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        /// <summary>
        /// Instantiates a new line item instance.
        /// </summary>
        public LineItem()
        {

        }

        /// <summary>
        /// Instantiates a new line item instance.
        /// </summary>
        /// <param name="product">The product instance.</param>
        /// <param name="quantity">The quantity amount in stock.</param>
        public LineItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

    }
}