using ShopBL;
using ShopModel;

namespace ShopUI
{
    /*
     * The select a customer menu instance.
     */
    public class SelectCustomerMenu : MenuInterface
    {

        //Variables that hold the input value of a specific task.
        private int _goBack;
        private int _exit;

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
            Console.WriteLine("Select customer");
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");

            //loop through each iteration in the customers list to display on the console.
            int index = 1;
            foreach(Customer _customer in customers.GetCustomers())
            {
                Console.WriteLine("["+ index +"] - "+ _customer.Name);
                index++;
            }

            //set up the go back and exit input setting
            _goBack = index;
            _exit = index + 1;

            Console.WriteLine("["+ _goBack +"] - Go back");
            Console.WriteLine("["+ _exit +"] - Exit");
        }

        public MenuType UserInput()
        {
            //convert the input string to an integer
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == _goBack)
                return MenuType.ViewStoreOptions;
            else if (input == _exit)
                return MenuType.Exit;
            else if (customers.GetCustomers()[input - 1] != null)
            {
                //find the customer in the customers list and get the instance.
                Customer buyer = customers.GetCustomers()[input - 1];
                Program.customer = buyer;

                Console.WriteLine("Entering store as "+ buyer.Name +".");
                Console.ReadLine();
                return MenuType.BuyProducts;
            }
            else
            {
                Console.WriteLine("That's not a valid response.");
                Console.ReadLine();
                return MenuType.MainMenu;
            }
        }
    }
}