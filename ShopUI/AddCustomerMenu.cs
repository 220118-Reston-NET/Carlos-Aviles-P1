using System.Data.SqlClient;
using ShopBL;
using ShopModel;

namespace ShopUI
{
    /*
     * The add a customer menu interface.
     */
    public class AddCustomerMenu : MenuInterface
    {
        /// <summary>
        /// Represents a customer.
        /// </summary>
        /// <returns>The customer instance.</returns>
        private static Customer customer = new Customer();

        /* The customers interface instance. */
        private ICustomers customers;

        /// <summary>
        /// Instantiates a new add customer menu instance.
        /// </summary>
        /// <param name="customers">The customers interface instance.</param>
        public AddCustomerMenu(ICustomers customers)
        {
            this.customers = customers;
        }

        public void Print()
        {
            Console.WriteLine("Add a customer");
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");
            Console.WriteLine("[1] - Name: "+ customer.Name);
            Console.WriteLine("[2] - Age: "+ customer.Age);
            Console.WriteLine("[3] - Address: "+ customer.Address);
            Console.WriteLine("[4] - Phone Number: "+ customer.Phone);
            Console.WriteLine("[5] - Username: "+ customer.Username);
            Console.WriteLine("[6] - Password: "+ customer.Password);
            Console.WriteLine("[7] - Save Customer");
            Console.WriteLine("[8] - Clear");
            Console.WriteLine("[9] - Go back");
        }

        public MenuType UserInput()
        {
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    Console.WriteLine("Please enter a customer name:");
                    string name = Console.ReadLine();
                    if (name.Any(char.IsDigit))
                    {
                        Console.WriteLine("Customer name cannot have any numeric value!");
                        Console.ReadLine();
                        return MenuType.AddCustomer;
                    }
                    customer.Name = name;
                    return MenuType.AddCustomer;
                case "2":
                    Console.WriteLine("Please enter the customer age:");
                    String age = Console.ReadLine();
                    try
                    {
                        int toAge = Convert.ToInt32(age);
                        if (toAge < 3 || toAge > 120)
                        {
                            Console.WriteLine("Only ages between 3 and 120 are accepted.");
                            Console.ReadLine();
                            return MenuType.AddCustomer;
                        }
                        customer.Age = toAge;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Age must be an integer!");
                        Console.ReadLine();
                        return MenuType.AddCustomer;
                    }
                    return MenuType.AddCustomer;
                case "3":
                    Console.WriteLine("Please enter the customer address:");
                    customer.Address = Console.ReadLine();
                    return MenuType.AddCustomer;
                case "4":
                    Console.WriteLine("Please enter the customer phone number:");
                    customer.Phone = Console.ReadLine();
                    return MenuType.AddCustomer;
                case "5":
                    Console.WriteLine("Please enter a username:");
                    customer.Username = Console.ReadLine();
                    return MenuType.AddCustomer;
                case "6":
                    Console.WriteLine("Please enter a password:");
                    customer.Password = Console.ReadLine();
                    return MenuType.AddCustomer;

                case "7":
                    if (customers.CanAddCustomer(customer))
                    {
                        try
                        {
                            customers.AddCustomer(customer); 
                            Log.Information("Added customer "+ customer.Username +" to the database.");
                            Console.WriteLine("Created your account, "+ customer.Username +"!");
                            Console.ReadLine();
                            Program.customer = customer;
                            customer = new Customer();
                            return MenuType.ViewStore;
                        }
                        catch(SqlException e)
                        {
                            if (e.Message.Contains("is already in use."))
                            {
                                Log.Information("Could not save customer attmept "+ customer.Name +" because username already exists.");
                                Console.WriteLine("This username already exists. Please try a different one.");
                                Console.ReadLine();
                                return MenuType.AddCustomer;
                            }
                        }
                    }
                    else
                    {
                        Log.Information("Couldn't save customer attempt "+ customer.Name +" because some details aren't filled.");
                        Console.WriteLine("Can't save customer. Make sure all details are filled.");
                    }
                    Console.ReadLine();
                    return MenuType.AddCustomer;
                    
                case "8":
                    Log.Information("Cleared default customer data");
                    customer.Name = "";
                    customer.Age = 18;
                    customer.Address = "";
                    customer.Phone = "";
                    customer.Username = "";
                    customer.Password = "";
                    return MenuType.AddCustomer;
                case "9":
                    return MenuType.CustomerPortal;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.AddCustomer;
            }
        }
    }
}