namespace ShopUI
{
    /*
     * The interface that displays the customer portal.
     */
    public class CustomerPortalMenu : MenuInterface
    {

        public void Print()
        {
            Console.WriteLine("Customers");
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");
            Console.WriteLine("[1] - Register as new customer");
            Console.WriteLine("[2] - Login as existing customer");
            Console.WriteLine("[3] - View stores");
            Console.WriteLine("[4] - Search for a store");
            Console.WriteLine("[5] - Go back");
            Console.WriteLine("[6] - Exit");
        }

        public MenuType UserInput()
        {
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    return MenuType.AddCustomer;
                case "2":
                    return MenuType.SelectCustomer;
                case "3":
                    return MenuType.SelectCustomer;
                case "4":
                    return MenuType.SearchStore;
                case "5":
                    return MenuType.MainMenu;
                case "6":
                    return MenuType.Exit;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}