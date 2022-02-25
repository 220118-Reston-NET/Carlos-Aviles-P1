using ShopModel;

namespace ShopBL
{

    /*
     * An interface that deals with the loading and saving of customers.
     */
    public interface ICustomers
    {
        
        /// <summary>
        /// Add a customer to the database.
        /// </summary>
        /// <param name="customer">The customer instance.</param>
        /// <returns>The cutomser instance</returns>
        Customer AddCustomer(Customer customer);

        /// <summary>
        /// Updates a customer's data in the database
        /// </summary>
        /// <param name="customer">The customer instance.</param>
        /// <returns>The customer instance</returns>
        Customer UpdateCustomer(Customer customer);

        /// <summary>
        /// Deletes a customer from the database.
        /// </summary>
        /// <param name="customer">The customer instance.</param>
        /// <returns>The customer instance.</returns>
        Customer DeleteCustomer(Customer customer);

        /// <summary>
        /// Gets all the customers from the database and places them in a list.
        /// </summary>
        /// <returns>The list of customers.</returns>
        List<Customer> GetCustomers();

        Task<List<Customer>> GetCustomersAsync();

        int LoginCustomer(string username, string password);

        /// <summary>
        /// Gets all the orders from a specific customer from the database.
        /// </summary>
        /// <param name="customerId">The customer's unique identification.</param>
        /// <returns>The list of orders.</returns>
        List<Order> GetOrders(int customerId);

        /// <summary>
        /// Gets a customer by using their unique identification.
        /// </summary>
        /// <param name="customerId">The customer's unique identification.</param>
        /// <returns>The customer instance.</returns>
        Customer GetCustomerFromId(int customerId);

        /// <summary>
        /// Gets the customer instance by trying to find a customer with the exact full name.
        /// </summary>
        /// <param name="name">The customer name provided.</param>
        /// <returns>The customer instance.</returns>
        Customer GetCustomerByName(string name);

        /// <summary>
        /// A flag that checks if customer data is empty.
        /// </summary>
        /// <param name="customer">The customer instance.</param>
        /// <returns>If a customer can be added to the repository.</returns>
        bool CanAddCustomer(Customer customer);

        /// <summary>
        /// Finds a customer by searching through each order.
        /// </summary>
        /// <param name="id">The order id.</param>
        /// <returns>The customer instance, if found.</returns>
        Customer GetCustomerFromOrder(int id);

        string DisplayOrderHistory(List<Order> orders);
    }
}