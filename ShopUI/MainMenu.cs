namespace ShopUI
{
    /*
     * The main menu interface that will display upon first load.
     */
    public class MainMenu : MenuInterface
    {

        public void Print()
        {
            Console.WriteLine("Welcome to iStore!");
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");
            Console.WriteLine("[1] - Customers");
            Console.WriteLine("[2] - Management");
            Console.WriteLine("[3] - Exit");

        }

        public MenuType UserInput()
        {
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    return MenuType.CustomerPortal;
                case "2":
                    return MenuType.SelectEmployee;
                case "3":
                    return MenuType.Exit;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}