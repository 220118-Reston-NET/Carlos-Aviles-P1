using ShopModel;
using ShopDL;

namespace ShopBL
{

    /*
     * Represents the handling of the customers repository.
     */
    public class Customers : ICustomers
    {

        /* The customer repository interface instance. */
        private ICustomerRepo  repo;

        /// <summary>
        /// Instantiates a new customers instance.
        /// </summary>
        /// <param name="cInter">The customer repository interface instance.</param>
        public Customers(ICustomerRepo repo)
        {
            this.repo = repo;
        }

        public Customer AddCustomer(Customer customer)
        {
            return repo.AddCustomer(customer);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            return repo.UpdateCustomer(customer);
        }

        public Customer DeleteCustomer(Customer customer)
        {
            return repo.DeleteCustomer(customer);
        }

        public List<Customer> GetCustomers()
        {
            return repo.GetCustomers();
        }

        public Task<List<Customer>> GetCustomersAsync()
        {
            return repo.GetCustomersAsync();
        }

        public int LoginCustomer(string username, string password)
        {
            return repo.LoginCustomer(username, password);
        }

        public List<Order> GetOrders(int customerId)
        {
            return repo.GetOrders(customerId);
        }

        public Customer GetCustomerFromId(int customerId)
        {
            return repo.GetCustomerFromId(customerId);
        }

        public List<Customer> GetCustomersWithExactName(string name)
        {
            return repo.GetCustomersWithExactName(name);
        }

        public List<Customer> GetSimilarCustomersByName(string name)
        {
            return repo.GetSimilarCustomersByName(name);
        }

        public List<Customer> GetSimilarCustomersByAddress(string address)
        {
            return repo.GetSimilarCustomersByAddress(address);
        }

        public Customer GetCustomerByName(string name)
        {
            return repo.GetCustomerByName(name);
        }

        public Customer GetCustomerByAddress(string address)
        {
            return repo.GetCustomerByAddress(address);
        }

        public Customer GetCustomerByPhone(string phone)
        {
            return repo.GetCustomerByPhone(phone);
        }

        public bool CanAddCustomer(Customer customer)
        {
            return repo.CanAddCustomer(customer);
        }

        public Customer GetCustomerFromOrder(int id)
        {
            return repo.GetCustomerFromOrder(id);
        }

        public string DisplayOrderHistory(List<Order> orders)
        {
            return repo.DisplayOrderHistory(orders);
        }
    }
}