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
            Console.WriteLine("[1] - Add a new customer");
            Console.WriteLine("[2] - Search for an existing customer");
            Console.WriteLine("[3] - Go back");
            Console.WriteLine("[4] - Exit");
        }

        public MenuType UserInput()
        {
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    return MenuType.AddCustomer;
                case "2":
                    return MenuType.SearchCustomer;
                case "3":
                    return MenuType.MainMenu;
                case "4":
                    return MenuType.Exit;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}