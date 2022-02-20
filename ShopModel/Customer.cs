namespace ShopModel
{

    /*
     * Represents a customer to the store.
     */
    public class Customer
    {

        /* The unique identification of this customer. */
        private int _id;
        public int Id {
            get { return _id; }
            set { _id = value; }
        }

        /* The full name of this customer. */
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /* The customer's age for legal reasons. */
        private int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        /* The customer's home address. */
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        
        /* The customer's private phone number. */
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        
        /* A list of purchased orders from each store. */
        private List<Order> _orders;
        public List<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// Instantiates a new customer instance.
        /// </summary>
        public Customer()
        {
            Name = "Morgan Stanley";
            Address = "Wall Street";
            Phone = "123456789";
            Age = 18;
            Orders = new List<Order>();
        }
    }
}