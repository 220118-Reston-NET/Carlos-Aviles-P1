using ShopModel;

namespace ShopDL
{
    
    /*
     * An interface that deals with the loading and saving of customers.
     */
    public interface ICustomerRepo
    {

        /// <summary>
        /// Add a customer to the database.
        /// </summary>
        /// <param name="customer">The customer instance.</param>
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
        /// Gets all the purchased items from an order from the database.
        /// </summary>
        /// <param name="orderId">The order's unique identification.</param>
        /// <returns>The list of purchased items.</returns>
        List<PurchasedItem> GetPurchasedItems(int orderId);

        /// <summary>
        /// Calculates the total amount of items bought in a single order.
        /// </summary>
        /// <param name="loadedItems">The list of purchased items in an order.</param>
        /// <returns>The total quantity amount.</returns>
        int GetTotalQuantityFromOrder(List<PurchasedItem> loadedItems);

        /// <summary>
        /// Gets a customer by using their unique identification.
        /// </summary>
        /// <param name="customerId">The customer's unique identification.</param>
        /// <returns>The customer instance.</returns>
        Customer GetCustomerFromId(int customerId);

        /// <summary>
        /// Search for a customer by trying to find them using the exact name.
        /// </summary>
        /// <param name="name">The name of the customer.</param>
        /// <returns>If the customer was found.</returns>
        bool SearchCustomerByExactName(string name);

        /// <summary>
        /// Search for a customer by name, using existing characters.
        /// </summary>
        /// <param name="name">The name of the customer.</param>
        /// <returns>If the customer was found.</returns>
        bool SearchCustomerByName(string name);

        /// <summary>
        /// Search for a customer by trying to find them using the exact address.
        /// </summary>
        /// <param name="address">The address of the customer.</param>
        /// <returns>If the customer was found.</returns>
        bool SearchCustomerByExactAddress(string address);

        /// <summary>
        /// Search for a customer by address, using existing characters.
        /// </summary>
        /// <param name="address">The address of the customer.</param>
        /// <returns>If the customer was found.</returns>
        bool SearchCustomerByAddress(string address);

        /// <summary>
        /// Search for a customer by trying to find them using the exact phone number.
        /// </summary>
        /// <param name="phone">The phone number of the customer.</param>
        /// <returns>If the customer was found.</returns>
        bool SearchCustomerByPhone(string phone);

        /// <summary>
        /// Gets a list of customers with the exact name.
        /// </summary>
        /// <param name="name">The customer name.</param>
        /// <returns>The list of customers.</returns>
        List<Customer> GetCustomersWithExactName(string name);

        /// <summary>
        /// Gets a list of similar customers by finding any that having a matching name.
        /// </summary>
        /// <param name="name">The customer name.</param>
        /// <returns>The list of similar customers.</returns>
        List<Customer> GetSimilarCustomersByName(string name);

        /// <summary>
        /// Gets a list of similar customers by finding any that having a matching address.
        /// </summary>
        /// <param name="address">The customer address.</param>
        /// <returns>The list of similar customers.</returns>
        List<Customer> GetSimilarCustomersByAddress(string address);

        /// <summary>
        /// Gets the customer instance by trying to find a customer with the exact full name.
        /// </summary>
        /// <param name="name">The customer name provided.</param>
        /// <returns>The customer instance.</returns>
        Customer GetCustomerByName(string name);

        /// <summary>
        /// Gets the customer instance by trying to find a customer with the exact address.
        /// </summary>
        /// <param name="address">The customer home addresse provided.</param>
        /// <returns>The customer instance.</returns>
        Customer GetCustomerByAddress(string address);

        /// <summary>
        /// Gets the customer instance by trying to find a customer with the exact phone number.
        /// </summary>
        /// <param name="phone">The phone number provided.</param>
        /// <returns>The customer instance.</returns>
        Customer GetCustomerByPhone(string phone);

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

        Customer GetCustomerFromUsername(string username);
    }
}