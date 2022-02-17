using ShopBL;
using ShopDL;
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
            Console.WriteLine("[5] - Save Customer");
            Console.WriteLine("[6] - Clear");
            Console.WriteLine("[7] - Go back");
        }

        public MenuType UserInput()
        {
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    Console.WriteLine("Please enter a customer name:");
                    string name = Console.ReadLine();
                    if (name.Any(char.IsDigit)) {
                        Console.WriteLine("Customer name cannot have any numeric value!");
                        Console.ReadLine();
                        return MenuType.AddCustomer;
                    }
                    customer.Name = name;
                    return MenuType.AddCustomer;
                case "2":
                    Console.WriteLine("Please enter the customer age:");
                    String age = Console.ReadLine();
                    try {
                        int toAge = Convert.ToInt32(age);
                        if (toAge < 3 || toAge > 120) {
                            Console.WriteLine("Only ages between 3 and 120 are accepted.");
                            Console.ReadLine();
                            return MenuType.AddCustomer;
                        }
                        customer.Age = toAge;
                    } catch (FormatException e) {
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
                    if (customers.CanAddCustomer(customer))
                    {
                        customers.AddCustomer(customer); 
                        Log.Information("Added customer "+ customer.Name +" to the database.");
                        Console.WriteLine("Added "+ customer.Name +" to the database.");
                        customer = new Customer();
                    }
                    else
                    {
                        Log.Information("Couldn't save customer attempt "+ customer.Name +" because either exists or incorrect details filled");
                        Console.WriteLine("Can't save customer. Make sure customer has all -> Name, Age, Address and Phone.");
                        Console.WriteLine("And make sure the same customer doesn't already exist.");
                    }
                    Console.ReadLine();
                    return MenuType.AddCustomer;
                    
                case "6":
                    Log.Information("Cleared default customer data");
                    customer.Name = "";
                    customer.Age = 18;
                    customer.Address = "";
                    customer.Phone = "";
                    return MenuType.AddCustomer;
                case "7":
                    return MenuType.CustomerPortal;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}