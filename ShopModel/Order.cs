namespace ShopModel
{

    /*
     * Represents the receipt of what the customer ordered from a store.
     */
    public class Order
    {
        
        /* The unique identifcation of this order. */
        private int _id;
        public int Id {
            get { return _id; }
            set { _id = value; }
        }

        /* A list of items the customer has bought on this purchase. */
        private List<PurchasedItem> _items;
        public List<PurchasedItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        /* The total amount of items the user bought on this purchase. */
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        /* The store location of this purchase. */
        private string _location;
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        /* The total price of the order. */
        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        
        /// <summary>
        /// Instantiates a new order instance.
        /// </summary>
        public Order()
        {
            Items = new List<PurchasedItem>();
        }
    }
}