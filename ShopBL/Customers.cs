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

        public List<Customer> GetCustomers()
        {
            return repo.GetCustomers();
        }

        public Customer GetCustomerFromId(int customerId)
        {
            return repo.GetCustomerFromId(customerId);
        }

        public bool SearchCustomerByExactName(string name) {
            return repo.SearchCustomerByExactName(name);
        }

        public bool SearchCustomerByName(string name)
        {
            return repo.SearchCustomerByName(name);
        }

        public bool SearchCustomerByExactAddress(string address)
        {
            return repo.SearchCustomerByExactAddress(address);
        }

        public bool SearchCustomerByAddress(string address)
        {
            return repo.SearchCustomerByAddress(address);
        }

        public bool SearchCustomerByPhone(string phone)
        {
            return repo.SearchCustomerByPhone(phone);
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
    }
}