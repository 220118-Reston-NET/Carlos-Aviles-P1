using ShopBL;
using ShopModel;

namespace ShopUI
{

    /*
     * A menu that will display a list of stores in this app.
     */
    public class ViewStoreMenu : MenuInterface
    {

        /* The store interface instance. */
        private IStores stores;

        /// <summary>
        /// Instantiates a new view store menu instance.
        /// </summary>
        /// <param name="stores">The store interface instance.</param>
        public ViewStoreMenu(IStores _inter)
        {
            stores = _inter;
        }

        public void Print()
        {
            Console.WriteLine("View stores");
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");

            //loop through each iteration in the stores list to display on the console.
            int index = 1;
            foreach(StoreFront store in stores.GetStores())
            {
                Console.WriteLine("["+ index +"] - "+ store.Name);
                index++;
            }

            Console.WriteLine("[4] - Go back");
        }

        public MenuType UserInput()
        {
            string _input = Console.ReadLine();

            switch(_input)
            {
                //there is only a maximum of 3 stores, otherwise this switch case is a bad idea
                //if there's more expansions
                case "1":
                case "2":
                case "3":
                    Program.Instance.storeIndex = Convert.ToInt32(_input) - 1;
                    return MenuType.ViewStoreOptions;
                case "4":
                    return MenuType.CustomerPortal;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}