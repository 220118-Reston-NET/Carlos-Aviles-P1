using ShopBL;
using ShopModel;

namespace ShopUI
{
    /*
     * The search for a customer menu interface.
     */
    public class SearchCustomerMenu : MenuInterface
    {

        /* The customers interface instance. */
        private ICustomers customers;

        /// <summary>
        /// Instantiates a new search customer menu instance.
        /// </summary>
        /// <param name="customers">The customers interface instance.</param>
        public SearchCustomerMenu(ICustomers customers)
        {
            this.customers = customers;
        }

        public void Print()
        {
            Console.WriteLine("Search for a customer");
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");
            Console.WriteLine("[1] - Search by Name");
            Console.WriteLine("[2] - Search by Address");
            Console.WriteLine("[3] - Search by Phone");
            Console.WriteLine("[4] - Go back");
        }

        public MenuType UserInput()
        {
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    Log.Information("Attempting to search for customer by name.");
                    Console.WriteLine("Please enter a customer name:");
                    string name = Console.ReadLine();

                    //first, search if the input matches exactly with any customer name.
                    if (customers.SearchCustomerByExactName(name))
                    {
                        if (customers.GetCustomersWithExactName(name).Count == 1)
                        {
                            Customer customer = customers.GetCustomerByName(name);
                            DisplaySearch(customer);
                        }
                        else
                        {
                            Console.WriteLine("Multiple customers with the exact name exist. Choose who to view.");
                            int index = 1;
                            foreach(Customer customer in customers.GetCustomersWithExactName(name))
                            {
                                Console.WriteLine("["+ index +"] "+customer.Name +" who currently resides in "+ customer.Address);
                                index++;
                            }
                            int goBack = index;
                            Console.WriteLine("["+ goBack +"] - Go back");
                            try
                            {
                                int picked = Convert.ToInt32(Console.ReadLine());
                                if (goBack == picked)
                                    return MenuType.SearchCustomer;
                                List<Customer> cust = customers.GetCustomersWithExactName(name);

                                for(int temp = 0; temp < index; temp++)
                                {
                                    if (temp == picked)
                                    {
                                        Customer customer = cust[picked - 1];
                                        DisplaySearch(customer);
                                    }
                                }
                            }
                            catch(FormatException e)
                            {
                                Log.Error("Error attempting to search for customer, based on incorrect user input");
                                Console.WriteLine("That's not a valid response.");
                                Console.ReadLine();
                                return MenuType.SearchCustomer;
                            }
                        }
                    }

                    //otherwise, search if the input matches any customer names.
                    else if (customers.SearchCustomerByName(name))
                    {
                        Console.WriteLine("Could not find customer with that exact name. But found a similar result.");
                        foreach(Customer customer in customers.GetSimilarCustomersByName(name))
                            Console.WriteLine(customer.Name + " who currently resides in "+ customer.Address);
                    }
                    else
                        Console.WriteLine("Could not find customer!");
                    Console.ReadLine();
                    return MenuType.SearchCustomer;

                case "2":
                    Log.Information("Attempting to search for customer by address.");
                    Console.WriteLine("Please enter the customer address:");
                    string address = Console.ReadLine();

                    //first, search if the input matches exactly with any customer address.
                    if (customers.SearchCustomerByExactAddress(address))
                    {
                        if (customers.GetSimilarCustomersByAddress(address).Count == 1)
                        {
                            Customer customer = customers.GetCustomerByAddress(address);
                            DisplaySearch(customer);
                        }
                        else
                        {
                            Console.WriteLine("Multiple customers with the exact address exist. Choose who to view.");
                            int index = 1;
                            foreach(Customer customer in customers.GetSimilarCustomersByAddress(address))
                            {
                                Console.WriteLine("["+ index +"] "+customer.Name +" who currently resides in "+ customer.Address);
                                index++;
                            }
                            int goBack = index;
                            Console.WriteLine("["+ goBack +"] - Go back");
                            try
                            {
                                int picked = Convert.ToInt32(Console.ReadLine());
                                if (goBack == picked)
                                    return MenuType.SearchCustomer;
                                List<Customer> cust = customers.GetSimilarCustomersByAddress(address);

                                for(int temp = 0; temp < index; temp++)
                                {
                                    if (temp == picked)
                                    {
                                        Customer customer = cust[picked - 1];
                                        DisplaySearch(customer);
                                    }
                                }
                            }
                            catch(FormatException e)
                            {
                                Log.Error("Error attempting to search for customer, based on incorrect user input");
                                Console.WriteLine("That's not a valid response.");
                                Console.ReadLine();
                                return MenuType.SearchCustomer;
                            }
                        }
                    }

                    //otherwise, search if the input matches any customer address.
                    else if (customers.SearchCustomerByAddress(address))
                    {
                        Console.WriteLine("Could not find customer with that exact address. But found a similar result.");
                        foreach(Customer customer in customers.GetSimilarCustomersByAddress(address))
                            Console.WriteLine(customer.Name + " lives in the address "+ customer.Address);
                    }
                    else
                        Console.WriteLine("Could not find customer!");
                    Console.ReadLine();
                    return MenuType.SearchCustomer;

                case "3":
                    Log.Information("Attempting to search for customer by phone number");
                    Console.WriteLine("Please enter the customer phone number:");
                    string phone = Console.ReadLine();

                    //search if the input matches exactly with any customer phone number.
                    if (customers.SearchCustomerByPhone(phone))
                    {
                        Customer customer = customers.GetCustomerByPhone(phone);
                        DisplaySearch(customer);
                    }
                    else
                        Console.WriteLine("Could not find customer!");
                    Console.ReadLine();
                    return MenuType.SearchCustomer;

                case "4":
                    return MenuType.CustomerPortal;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }

        /// <summary>
        /// Displays all the customer details onto the console.
        /// </summary>
        /// <param name="customer">The customer instance.</param>
        private void DisplaySearch(Customer customer)
        {
            Console.WriteLine(customer.Name + " lives in address "+ customer.Address + " with the phone number "+ customer.Phone +".");
            if (customer.Orders.Count == 0)
                Console.WriteLine("This customer has not ordered anything from any store.");
            else
            {
                //loop through each iteration in the orders list to display all the order the customer has made.
                foreach (Order order in customer.Orders)
                {
                    foreach (PurchasedItem item in order.Items)
                        Console.WriteLine("- "+ item.Quantity +"x "+item.Item.Name +" was bought for $"+ item.Item.Price + " each at "+ order.Location +".");
                    Console.WriteLine("This order came out to a total of $"+ order.Price);
                }
            }
        }
    }
}