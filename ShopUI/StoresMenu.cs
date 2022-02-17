namespace ShopUI
{
    /*
     * The menu interface that will display basic store front options.
     */
    public class StoresMenu : MenuInterface
    {

        public void Print()
        {
            Console.WriteLine("Stores");
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");
            Console.WriteLine("[1] - View stores");
            Console.WriteLine("[2] - Search for a store");
            Console.WriteLine("[3] - Go back");
            Console.WriteLine("[4] - Exit");
        }

        public MenuType UserInput()
        {
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    return MenuType.ViewStore;
                case "2":
                    return MenuType.SearchStore;
                case "3":
                    return MenuType.MainMenu;
                case "4":
                    return MenuType.Exit;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.Stores;
            }
        }
    }
}