global using Serilog;
using Microsoft.Extensions.Configuration;
using ShopBL;
using ShopDL;
using ShopModel;
using ShopUI;

/*
 * The main entry point of the console application. Also executed program logic.
 */
public class Program
{

    /* Singleton instance of this class. */
    private static readonly Program INSTANCE = new Program();

    /* The while loop flag. */
    private bool repeat = true;

    /* The main menu interface instance. */
    private MainMenu mainMenu = new MainMenu();

    /* The menu interface instance. */
    private MenuInterface menu;

    /* The stores interface instance instantiated with the store repo. */
    private IStores stores;

    /* The employee interface instance instantiated with the employee repo. */
    private IEmployees employees;

    /* The customer interface instance instantiated with the customer repo. */
    private ICustomers customers;

    /* The product interface instance instantiated with the product repo. */
    private IProducts products;

    /* The order interface instance instantiated withe the order repo. */
    private IOrders orders;

    /* The index of the store the user is currently viewing. */
    public int storeIndex;

    /* The customer who is trying to purchase from a store. */
    public static Customer customer;

    /* The employee who is trying to administer a store. */
    public static Employee employee;

    /// <summary>
    /// Initializes the data and logger.
    /// </summary>
    private void init()
    {
        //load sensitive data
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        string _connectionString = configuration.GetConnectionString("ShopAppDB");

        //initialize instances
        stores = new Stores(new StoreRepository(_connectionString));
        customers = new Customers(new CustomerRepository(_connectionString));
        products = new Products(new ProductRepository(_connectionString));
        orders = new Orders(new OrderRepository(_connectionString));
        employees = new Employees(new EmployeeRepository(_connectionString));

        //load up the logger
        Log.Logger = new LoggerConfiguration().WriteTo.File("./logs/user.txt").CreateLogger();
    }

    /// <summary>
    /// The main starting logic starting point.
    /// </summary>
    private void start()
    {
        //Initialize the menu interface.
        menu = mainMenu;

        //Initialize all needed repositories.
        init();

        while(repeat)
        {
            Console.Clear();
            menu.Print();
            MenuType _input = menu.UserInput();

            switch(_input)
            {
                case MenuType.MainMenu:
                    Log.Information("Displaying MainMenu to user");
                    menu =  new MainMenu();
                    break;

                /*
                 * The customer's menus.
                 */
                case MenuType.CustomerPortal:
                    Log.Information("Displaying CustomerMenu to user");
                    menu = new CustomerPortalMenu();
                    break;
                case MenuType.AddCustomer:
                    Log.Information("Displaying AddCustomerMenu to user");
                    menu = new AddCustomerMenu(customers);
                    break;
                case MenuType.SearchCustomer:
                    Log.Information("Displaying SearchCustomerMenu to user");
                    menu = new SearchCustomerMenu(customers);
                    break;

                /*
                 * Everything to do with the stores part of the menu.
                 */
                case MenuType.Stores:
                    Log.Information("Displaying StoresMenu to user");
                    menu = new StoresMenu();
                    break;
                case MenuType.ViewStore:
                    Log.Information("Displaying ViewStoresMenu to user");
                    menu = new ViewStoreMenu(stores);
                    break;
                case MenuType.ViewStoreOptions:
                    Log.Information("Displaying ViewStoreOptionsMenu to user");
                    menu = new ViewStoreOptionsMenu(stores);
                    break;
                case MenuType.SelectCustomer:
                    Log.Information("Displaying SelectCustomerMenu to user");
                    menu = new SelectCustomerMenu(customers);
                    break;
                case MenuType.BuyProducts:
                    Log.Information("Displaying BuyProductsMenu to user");
                    menu = new BuyProductsMenu(orders, customers, stores);
                    break;
                case MenuType.SearchStore:
                    Log.Information("Displaying SearchStoreMenu to user");
                    menu = new SearchStoreMenu(stores);
                    break;
                case MenuType.ReplenishInventory:
                    Log.Information("Displaying ReplenishInventoryMenu to user");
                    menu = new ReplenishInventoryMenu(stores);
                    break;
                case MenuType.ViewOrderHistory:
                    Log.Information("Displaying ViewOrderHistory to user");
                    menu = new ViewOrderHistoryMenu(stores, customers);
                    break;
                case MenuType.SelectEmployee:
                    Log.Information("Displaying SelectEmployee to user");
                    menu = new SelectEmployeeMenu(employees, stores);
                    break;

                case MenuType.Exit:
                    Log.CloseAndFlush();
                    repeat = false;
                    break;
                default:
                    Log.Error("Customer landed on a nonexistant page! "+ _input);
                    Console.WriteLine("Page does not exist!");
                    break;
            }
        }
    }

    /// <summary>
    /// The store index represents the store id the customer is currently in.
    /// </summary>
    /// <param name="type">The store index.</param>
    public void UpdateStoreIndex(string type)
    {
        StoreFront store = stores.GetStores().Find(_temp => _temp.Name.Equals(type));
        if (store == null)
            store = stores.GetStores().Find(_temp => _temp.Address.Equals(type));

        storeIndex = store.Id - 1;
    }

    /// <summary>
    /// Property that deals with the singleton instance.
    /// </summary>
    /// <value>The instance of this class.</value>
    public static Program Instance
    {
        get
        {
            return INSTANCE;
        }
    }

    /// <summary>
    /// The main entry point of Project0.
    /// </summary>
    /// <param name="args">Program arguments.</param>
    static void Main(string[] args)
    {
        Program.Instance.start();   
    }
}