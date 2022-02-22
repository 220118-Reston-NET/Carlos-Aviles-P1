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
            Console.WriteLine("Management");
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
                        String password = ConsoleUtility.ReadSensitiveLine();

                        int loginResult = employees.LoginEmployee(username, password);
                        if (loginResult == 1)
                        {
                            Employee employee = employees.GetEmployeeFromUsername(username);
                        
                            Program.employee = employee;
                            int storeId = employee.StoreId - 1;
                            Program.Instance.storeIndex = storeId;
                            Console.WriteLine("Successfully logged in as "+ employee.Name +". Entering "+ stores.GetStores()[storeId].Name +".");
                            Console.ReadLine();
                            return MenuType.ViewStoreOptions;
                        }
                        else
                        {
                            Console.WriteLine("Your username or password is not correct. Try again.");
                            Console.ReadLine();
                            return MenuType.SelectEmployee;
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
            catch (FormatException e)
            {
                Console.WriteLine("That's not a valid response.");
                Console.ReadLine();
                return MenuType.MainMenu;
            }
        }
    }
}