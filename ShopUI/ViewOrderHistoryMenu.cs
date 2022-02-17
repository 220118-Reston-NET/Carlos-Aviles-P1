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


            //loop through each iteration in the orders list.
            foreach (Order _order in store.Orders)
            {
                Customer _customer = customers.GetCustomerFromOrder(_order.Id);
                Console.WriteLine("This order (#"+ _order.Id +") from "+ _customer.Name +" was a total of $"+ _order.Price);   
                foreach (PurchasedItem _item in _order.Items)
                    Console.WriteLine("- "+ _item.Item.Name +" was purchased for $"+ _item.Item.Price +" each ("+ _item.Quantity +"x)");
                    Console.WriteLine("This order was created at "+ _order.DateCreated);
                    
                Console.WriteLine();
            }

            Console.WriteLine("[1] - Go back");
            Console.WriteLine("[2] - Exit");
        }

        public MenuType UserInput()
        {
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    return MenuType.ViewStoreOptions;
                case "2":
                    return MenuType.Exit;
                default:
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}