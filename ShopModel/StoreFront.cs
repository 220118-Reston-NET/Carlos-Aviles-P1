namespace ShopModel
{

    /*
     * Represents a store in the store app.
     */
    public class StoreFront
    {

        /* The unique identification of this store. */
        private int _id;
        public int Id {
            get { return _id; }
            set { _id = value; }
        }
        
        /* The name of the store. */
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        /* The address of the store. */
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        /* A list of items this store is selling. */
        private List<LineItem> _items;
        public List<LineItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        /* A list of order history customers has bought form this particular store. */
        private List<Order> _orders;
        public List<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        /// <summary>
        /// Instantiates a new store instance.
        /// </summary>
        public StoreFront()
        {
            _orders = new List<Order>();
        }
        
    }
}