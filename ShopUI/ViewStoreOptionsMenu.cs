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
            Console.WriteLine("[2] - Replenish inventory");
            Console.WriteLine("[3] - View order history");
            Console.WriteLine("[4] - Go back");
            Console.WriteLine("[5] - Exit");
        }

        public MenuType UserInput()
        {
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    StoreFront store = stores.GetStores()[Program.Instance.storeIndex];
                    if (stores.hasInventory(store))
                    {
                        return MenuType.SelectCustomer;
                    }
                    else
                    {
                        Console.WriteLine("This store has nothing in inventory. Please come back later.");
                        Console.ReadLine();
                    }
                        return MenuType.ViewStoreOptions;
                case "2":
                    return MenuType.ReplenishInventory;
                case "3":
                    return MenuType.ViewOrderHistory;
                case "4":
                    return MenuType.ViewStore;
                case "5":
                    return MenuType.Exit;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}