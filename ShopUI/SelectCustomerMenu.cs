using ShopBL;
using ShopModel;

namespace ShopUI
{
    /*
     * The select a customer menu instance.
     */
    public class SelectCustomerMenu : MenuInterface
    {

        /* The customers interface instance */
        private ICustomers customers;

        /// <summary>
        /// Instantiates a new select customer menu instance.
        /// </summary>
        /// <param name="customers">The customers interface instance.</param>
        public SelectCustomerMenu(ICustomers customers)
        {
            this.customers = customers;
        }

        public void Print()
        {
            Console.WriteLine("Customer");
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");

            Console.WriteLine("[1] - Login");
            Console.WriteLine("[2] - Go back");
            Console.WriteLine("[3] - Exit");
        }

        public MenuType UserInput()
        {
            try
            {
                //convert the input string to an integer
                int input = Convert.ToInt32(Console.ReadLine());

                switch(input)
                {
                    case 1:
                        Console.WriteLine("Type in your username:");
                        String username = Console.ReadLine();
                        Console.WriteLine("Type in your password:");
                        String password = Console.ReadLine();

                        int loginResult = customers.LoginCustomer(username, password);
                        if (loginResult == 1)
                        {
                            Customer customer = customers.GetCustomerFromUsername(username);
                        
                            Program.customer = customer;
                            Console.WriteLine("Successfully logged in as "+ customer.Name +".");
                            Console.ReadLine();
                            return MenuType.ViewStore;
                        }
                        else
                        {
                            Console.WriteLine("Your username or password is not correct. Try again.");
                            Console.ReadLine();
                            return MenuType.SelectCustomer;
                        }
                    case 2:
                        return MenuType.MainMenu;
                    case 3:
                        return MenuType.Exit;
                    default:
                        Console.WriteLine("That's not a valid response.");
                        Console.ReadLine();
                        return MenuType.MainMenu;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("That's not a valid response.");
                Console.ReadLine();
                return MenuType.MainMenu;
            }
        }
    }
}