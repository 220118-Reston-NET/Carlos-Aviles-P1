using ShopBL;
using ShopModel;

namespace ShopUI
{

    /*
     * The interface that displays the buy products menu.
     */
    public class BuyProductsMenu : MenuInterface
    {

        /* The orders interface instance. */
        private IOrders orders;

        /* The customers interface instance. */
        private ICustomers customers;

        /* The stores interface instance. */
        private IStores stores;

        /// <summary>
        /// Instantiates a new buy products menu instance.
        /// </summary>
        /// <param name="orders">The orders interface instance.</param>
        /// <param name="customers">The customers interface instance.</param>
        /// <param name="stores">The stores interface instance.</param>
        public BuyProductsMenu(IOrders orders, ICustomers customers, IStores stores)
        {
            this.orders = orders;
            this.customers = customers;
            this.stores = stores;
        }

        /* A static list of items in the customer's cart. */
        private static List<CartItem> cart = new List<CartItem>();

        //Variables that hold the input value of a specific task.
        private int inventory;
        private int emptyCart;
        private int makePurchase;
        private int goBack;
        private int exit;
        public void Print()
        {
            StoreFront store = stores.GetStores()[Program.Instance.storeIndex];

            Console.WriteLine("Viewing "+ store.Name);
            Console.WriteLine();
            Console.WriteLine("Pick a response below:");

            //loop through each iteration in the store items list to display on the console.
            int index = 1;
            foreach(LineItem item in store.Items)
            {
                Console.WriteLine("["+ index +"] - Buy "+ item.Product.Name +" for $"+ item.Product.Price +" ("+ item.Quantity +" left in stock)");
                index++;
            }
            
            //initialize the input variables.
            inventory = index;
            emptyCart = index + 1;
            makePurchase = index + 2;
            goBack = index + 3;
            exit = index + 4;

            Console.WriteLine("["+ inventory +"] - View cart");
            Console.WriteLine("["+ emptyCart +"] - Empty cart");
            Console.WriteLine("["+ makePurchase +"] - Purchase items");
            Console.WriteLine("["+ goBack +"] - Go back");
            Console.WriteLine("["+ exit +"] - Exit");
        }

        public MenuType UserInput()
        {
            StoreFront _store = stores.GetStores()[Program.Instance.storeIndex];
            try
            {
                int input = Convert.ToInt32(Console.ReadLine());
                if (input == inventory)
                {
                    Console.WriteLine("Your cart:");
                    if(cart.Count == 0)
                        Console.WriteLine("There's nothing in your cart!");
                    else
                        displayCart();
                    return MenuType.BuyProducts;
                }
                else if (input == emptyCart)
                {
                    restock(_store);
                    cart.Clear();
                    Console.WriteLine("Your cart has been cleared.");
                    Console.ReadLine();
                    return MenuType.BuyProducts;
                }
                else if (input == makePurchase)
                {
                    if(cart.Count == 0)
                        Console.WriteLine("There's nothing in your cart!");
                    else
                    {
                        orders.PlaceOrder(Program.customer.Id, cart, _store, orders.GetCartTotal(cart));

                        Log.Information(Program.customer.Name + " placed an order for a total of $"+ orders.GetCartTotal(cart) +" at "+ _store.Name);
                        Console.WriteLine(Program.customer.Name +" has purchased this order for $"+ orders.GetCartTotal(cart));
                        cart.Clear();
                        Program.customer = null;
                    }
                    Console.ReadLine();
                    return MenuType.ViewStoreOptions;
                }
                else if (input == goBack)
                {
                    restock(_store);
                    cart.Clear();
                    return MenuType.ViewStoreOptions;
                }
                else if (input == exit)
                {
                    restock(_store);
                    cart.Clear();
                    return MenuType.Exit;
                }
                else if (input < inventory && input > 0)
                {
                    LineItem item = _store.Items[input - 1];

                    if(Program.customer.Age < item.Product.MinimumAge)
                    {
                        Console.WriteLine("You need to be at-least "+ item.Product.MinimumAge +" years old to purchase this item!");
                        Console.ReadLine();
                        return MenuType.BuyProducts;
                    }

                    Console.WriteLine("How many "+ item.Product.Name +" would you like to buy?");
                    int inputQuantity = Convert.ToInt32(Console.ReadLine());
                    if (inputQuantity <= 0)
                    {
                        Console.WriteLine("You cannot buy 0 quantities.");
                        Console.ReadLine();
                        return MenuType.BuyProducts;
                    }
                    else if (inputQuantity > item.Quantity)
                    {
                        Console.WriteLine("There's only "+ item.Quantity +" left in stock!");
                        Console.ReadLine();
                        return MenuType.BuyProducts;
                    }

                    bool exists = cart.Any(_temp => _temp.Item.Name.Equals(item.Product.Name));
                    if (exists)
                    {
                        CartItem _inventory = cart.Find(_temp => _temp.Item.Name.Equals(item.Product.Name));
                        _inventory.Quantity = _inventory.Quantity + inputQuantity;
                    }
                    else
                    {
                        cart.Add(new CartItem(item.Product, inputQuantity));
                    }

                    item.Quantity = item.Quantity - inputQuantity;
                    stores.UpdateStoreInventory(_store.Id, item.Product.Id, item.Quantity);
                    Console.WriteLine();
                    Log.Information(Program.customer.Name + " added an item ("+ item.Product.Name +") to their cart.");
                    Console.WriteLine("Added "+ inputQuantity +"x "+ item.Product.Name +" to your cart.");
                    displayCart();
                    return MenuType.BuyProducts;
                }
                else
                {
                    restock(_store);
                    cart.Clear();
                    Console.WriteLine("That's not a valid response.");
                    Console.ReadLine();
                    return MenuType.BuyProducts;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("That's not a valid response.");
                Console.ReadLine();
                return MenuType.BuyProducts;
            }
        }

        private void displayCart()
        {
            foreach (CartItem item in cart)
                Console.WriteLine(item.Item.Name + " - "+ item.Quantity +"x");
            Console.WriteLine("Total costs in your cart is $"+ orders.GetCartTotal(cart) +".");
            Console.ReadLine();
        }

        private void restock(StoreFront _store)
        {
            if (cart.Count == 0)
                return;
            foreach (CartItem item in cart)
            {
                int _quantity = _store.Items.Find(_item => _item.Product.Id == item.Item.Id).Quantity;
                stores.UpdateStoreInventory(_store.Id, item.Item.Id, item.Quantity + _quantity);
            }
        }

    }
}