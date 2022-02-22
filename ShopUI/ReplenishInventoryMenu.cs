using ShopBL;
using ShopModel;

namespace ShopUI
{

    /*
     * The interface that displays the restock inventory menu.
     */
    public class ReplenishInventoryMenu : MenuInterface
    {

        /* The store interface instance. */
        private IStores stores;

        //Variables that hold the input value of a specific task.
        private int _goBack;
        private int _exit;

        /// <summary>
        /// Instantiates a new replenish inventory instance.
        /// </summary>
        /// <param name="stores">The store interface instance.</param>
        public ReplenishInventoryMenu(IStores stores)
        {
            this.stores = stores;
        }

        public void Print()
        {
            StoreFront store = stores.GetStores()[Program.Instance.storeIndex];
            Console.WriteLine(store.Name +"'s inventory");
            Console.WriteLine("Signed in as "+ Program.employee.Name);
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");

            //loop through each iteration in the store items list to display on the console.
            int index = 1;
            foreach(LineItem item in store.Items)
            {
                Console.WriteLine("["+ index +"] - Restock "+ item.Product.Name +" ("+ item.Quantity +" in stock)");
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
            StoreFront store = stores.GetStores()[Program.Instance.storeIndex];
            int input = 0;
            try
            {
                input = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Log.Warning("Invalid user input in the ReplenishInventoryMenu");
                Console.WriteLine("That's not a valid response.");
                Console.ReadLine();
                return MenuType.ReplenishInventory;
            }

            if (input == _goBack)
            {
                return MenuType.ViewStoreOptions;
            }
            else if (input == _exit)
            {
                Program.employee = null;
                return MenuType.Exit;
            }
            else if (input < _goBack && input > 0)
            {
                LineItem item = store.Items[input - 1];

                Console.WriteLine("How many "+ item.Product.Name +" would you like to restock?");
                try
                {
                    int quantity = Convert.ToInt32(Console.ReadLine());
                    if (quantity <= 0 || quantity >= 100)
                    {
                        Console.WriteLine("You cannot replenish less than 1 or more than 100!");
                        Console.ReadLine();
                        return MenuType.ReplenishInventory;
                    }
                    item.Quantity = item.Quantity + quantity;
                    Console.WriteLine();
                    Console.WriteLine("Added "+ quantity +"x "+ item.Product.Name +" to the current stock.");
                    Log.Information("Added "+ quantity +"x "+ item.Product.Name +" to the current stock.");
                    stores.UpdateStoreInventory(store.Id, item.Product.Id, item.Quantity);
                    return MenuType.ReplenishInventory;
                }
                catch (FormatException e)
                {
                    Log.Warning("Invalid user input while trying to add quantities to store in the ReplenishInventoryMenu");
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.ReplenishInventory;
                }
            }
            else
            {
                Console.WriteLine("That's not a valid response.");
                Console.ReadLine();
                Program.employee = null;
                return MenuType.MainMenu;
            }
        }
    }
}