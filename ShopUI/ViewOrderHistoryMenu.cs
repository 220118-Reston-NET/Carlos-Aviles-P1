using ShopBL;
using ShopModel;

namespace ShopUI
{

    /*
     * The interface that displays the order history of a store menu.
     */
    public class ViewOrderHistoryMenu : MenuInterface
    {

         /* The store interface instance. */
        private IStores stores;

        /* The customer interface instance. */
        private ICustomers customers;

        /// <summary>
        /// Instantiates a new replenish inventory instance.
        /// </summary>
        /// <param name="stores">The store interface instance.</param>
        /// <param name="customers">The customer interface instance.</param>
        public ViewOrderHistoryMenu(IStores stores, ICustomers customers)
        {
            this.stores = stores;
            this.customers = customers;
        }

 
        public void Print()
        {
            StoreFront store = stores.GetStores()[Program.Instance.storeIndex];
            Console.WriteLine(store.Name +"'s order history");
            Console.WriteLine();

            Console.WriteLine("Pick a response below:");
            Console.WriteLine("[1] - Simple sort");
            Console.WriteLine("[2] - Most recent sort");
            Console.WriteLine("[3] - Total price sort");
            Console.WriteLine("[4] - Customer sort");
            Console.WriteLine("[5] - Go back");
            Console.WriteLine("[6] - Exit");
        }

        public MenuType UserInput()
        {
            StoreFront store = stores.GetStores()[Program.Instance.storeIndex];
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    DisplayOrder(store.Orders);
                    return MenuType.ViewOrderHistory;
                case "2":
                    List<Order> recent = store.Orders;
                    recent.Reverse();
                    DisplayOrder(recent);
                    return MenuType.ViewOrderHistory;
                case "3":
                    List<Order> total = store.Orders;
                    total = total.OrderBy(order => order.Price).ToList();
                    total.Reverse();
                    DisplayOrder(total);
                    return MenuType.ViewOrderHistory;
                case "4":
                    List<Order> nameOrders = new List<Order>();
                    List<Customer> custs = new List<Customer>();
                    foreach (Order _order in store.Orders)
                    {
                        Customer _customer = customers.GetCustomerFromOrder(_order.Id);
                        if (!custs.Exists(c => c.Id == _customer.Id))
                        {
                            List<Order> orders = customers.GetOrders(_customer.Id);
                            foreach (Order order in orders)
                                nameOrders.Add(order);
                            custs.Add(_customer);
                        }
                    }
                    DisplayOrder(nameOrders);
                    return MenuType.ViewOrderHistory;
                case "5":
                    return MenuType.ViewStoreOptions;
                case "6":
                    return MenuType.Exit;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }

        public void DisplayOrder(List<Order> orders)
        {
            foreach (Order _order in orders)
            {
                Customer _customer = customers.GetCustomerFromOrder(_order.Id);
                Console.WriteLine("This order (#"+ _order.Id +") from "+ _customer.Name +" was a total of $"+ _order.Price);   
                foreach (PurchasedItem _item in _order.Items)
                    Console.WriteLine("- "+ _item.Item.Name +" was purchased for $"+ _item.Item.Price +" each ("+ _item.Quantity +"x)");
                Console.WriteLine("This order was created at "+ _order.DateCreated);
                Console.WriteLine();
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}