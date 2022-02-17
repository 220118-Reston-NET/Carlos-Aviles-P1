using ShopBL;
using ShopModel;

namespace ShopUI
{
    /*
     * The search for a store menu interface.
     */
    public class SearchStoreMenu : MenuInterface
    {

        /* The store interface instance. */
        private IStores stores;

        /// <summary>
        /// Instantiates a new search store menu instance.
        /// </summary>
        /// <param name="stores">The store interface instance.</param>
        public SearchStoreMenu(IStores stores)
        {
            this.stores = stores;
        }

        public void Print()
        {
            Console.WriteLine("Search for a store");
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");
            Console.WriteLine("[1] - Search by Name");
            Console.WriteLine("[2] - Search by Address");
            Console.WriteLine("[3] - Go back");
        }

        public MenuType UserInput()
        {
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    Console.WriteLine("Please enter a store name:");
                    string name = Console.ReadLine();

                    //first, search if the input matches exactly with any store name.
                    if (stores.SearchStoreByExactName(name))
                    {
                        Program.Instance.UpdateStoreIndex(name);
                        return MenuType.ViewStoreOptions;
                    }

                    //otherwise, search if the input matches any store names.
                    else if (stores.SearchStoreByName(name))
                    {
                        Console.WriteLine("Could not find store with that exact name. But found a similar result.");
                        foreach(StoreFront store in stores.GetSimilarStoresByName(name))
                            Console.WriteLine(store.Name);
                    }
                    else
                        Console.WriteLine("Could not find store!");
                    Console.ReadLine();
                    return MenuType.SearchStore;

                case "2":
                    Console.WriteLine("Please enter the store address:");
                    string address = Console.ReadLine();

                    //first, search if the input matches exactly with any store address.
                    if (stores.SearchStoreByExactAddress(address))
                    {
                        Program.Instance.UpdateStoreIndex(address);
                        return MenuType.ViewStoreOptions;
                    }

                    //otherwise, search if the input matches any store address.
                    else if (stores.SearchStoreByAddress(address))
                    {
                        Console.WriteLine("Could not find store with that exact address. But found a similar result.");
                        foreach (StoreFront store in stores.GetSimilarStoresByAddress(address))
                            Console.WriteLine(store.Name + " - "+ store.Address);
                    }
                    else
                        Console.WriteLine("Could not find store!");
                    Console.ReadLine();
                    return MenuType.SearchStore;

                case "3":
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}