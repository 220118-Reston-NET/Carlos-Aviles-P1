namespace ShopModel
{

    /*
     * Represents a employee to the store.
     */
    public class Employee
    {

        /* The unique identification of this employee. */
        private int _id;
        public int Id {
            get { return _id; }
            set { _id = value; }
        }

        /* The unique identification of the store. */
        private int _storeId;
        public int StoreId {
            get { return _storeId; }
            set { _storeId = value; }
        }

        /* The full name of this employee. */
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /* The username of this employee. */
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        /* The password of this employee. */
        private byte[] _password;
        public byte[] Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// Instantiates a new employee instance.
        /// </summary>
        public Employee()
        {
            Name = "John Doe";
        }
    }
}