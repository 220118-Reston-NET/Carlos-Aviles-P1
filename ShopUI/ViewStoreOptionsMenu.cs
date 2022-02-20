using ShopBL;
using ShopModel;

namespace ShopUI
{
    
    /*
     * A menu that will bring up a list of options a user can do in a particular store.
     */
    public class ViewStoreOptionsMenu : MenuInterface
    {

        /* The store interface instance. */
        private IStores stores;

        /// <summary>
        /// Instantiates a new view store options instance.
        /// </summary>
        /// <param name="stores">The store interface instance.</param>
        public ViewStoreOptionsMenu(IStores stores)
        {
            this.stores = stores;
        }

        public void Print()
        {
            Console.WriteLine(stores.GetStores()[Program.Instance.storeIndex].Name +" options");
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");
            Console.WriteLine("[1] - Buy a product");
            Console.WriteLine("[2] - Go back");
            Console.WriteLine("[3] - Exit");
            if (Program.employee != null)
            {
                Console.WriteLine();
                Console.WriteLine("Signed in as "+ Program.employee.Name);
                Console.WriteLine("[a] - Search for a customer");
                Console.WriteLine("[b] - Replenish inventory");
                Console.WriteLine("[c] - View orders history");
            }
        }

        public MenuType UserInput()
        {
            string input = Console.ReadLine();

            switch(input)
            {
                case "a":
                    return MenuType.SearchCustomer;
                case "b":
                    return MenuType.ReplenishInventory;
                case "c":
                    return MenuType.ViewOrderHistory;

                case "1":
                    StoreFront store = stores.GetStores()[Program.Instance.storeIndex];
                    if (stores.hasInventory(store))
                    {
                        return MenuType.BuyProducts;
                    }
                    else
                    {
                        Console.WriteLine("This store has nothing in inventory. Please come back later.");
                        Console.ReadLine();
                    }
                    return MenuType.ViewStoreOptions;
                case "2":
                    if (Program.employee != null)
                    {
                        Program.employee = null;
                        return MenuType.SelectEmployee;
                    }
                    return MenuType.ViewStore;
                case "3":
                    return MenuType.Exit;
                default:
                    Program.employee = null;
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}