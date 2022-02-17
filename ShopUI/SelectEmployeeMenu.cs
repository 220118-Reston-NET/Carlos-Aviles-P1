using ShopBL;
using ShopModel;

namespace ShopUI
{
    /*
     * The select an employee menu instance.
     */
    public class SelectEmployeeMenu : MenuInterface
    {

        /* The store interface instance. */
        private IStores stores;

        //Variables that hold the input value of a specific task.
        private int _goBack;
        private int _exit;

        /* The employees interface instance */
        private IEmployees employees;

        /// <summary>
        /// Instantiates a new select employee menu instance.
        /// </summary>
        /// <param name="employees">The employees interface instance.</param>
        /// <param name="stores">The store interface instance.</param>
        public SelectEmployeeMenu(IEmployees employees, IStores stores)
        {
            this.employees = employees;
            this.stores = stores;
        }

        public void Print()
        {
            Console.WriteLine("Select employee");
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");

            //loop through each iteration in the employees list to display on the console.
            int index = 1;
            StoreFront store = stores.GetStores()[Program.Instance.storeIndex];
            foreach(Employee _employee in employees.GetEmployeesFromStoreId(store.Id))
            {
                Console.WriteLine("["+ index +"] - "+ _employee.Name);
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
            else if (employees.GetEmployees()[input - 1] != null)
            {
                //find the employee in the employees list and get the instance.
                Employee employee = employees.GetEmployees()[input - 1];

                Console.WriteLine("Type in your username:");
                string user = Console.ReadLine();
                if (user != employee.Username)
                {
                    Console.WriteLine("Incorrect username.");
                    Console.ReadLine();
                    return MenuType.SelectEmployee;
                }

                Console.WriteLine("Type in your password:");
                string pass = Console.ReadLine();
                if (pass != employee.Password)
                {
                    Console.WriteLine("Incorrect password.");
                    Console.ReadLine();
                    return MenuType.SelectEmployee;
                }

                Program.employee = employee;
                Console.WriteLine("Successfully logged in as an employee. Entering store as "+ employee.Name +".");
                Console.ReadLine();
                return MenuType.ReplenishInventory;
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