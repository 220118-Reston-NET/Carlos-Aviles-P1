using System.Data.SqlClient;
using System.Text.Json;
using ShopModel;

namespace ShopDL
{

    /*
     * A repository that will deal with the loading and placing of customer orders.
     */
    public class OrderRepository : IOrderRepo
    {

        /* The connection url link */     
        private readonly string connectionURL;

        /// <summary>
        /// Instantiates a new order repository instance.
        /// </summary>
        /// <param name="connectionURL">The connection url link</param>
        public OrderRepository(string connectionURL)
        {
            this.connectionURL = connectionURL;
        }

        private Customer GetCustomer(int customerId)
        {
            List<Customer> listOfCustomers = new List<Customer>();
            string query = @"select * from [Customer]";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfCustomers.Add(new Customer() {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Age = reader.GetInt32(2),
                    });
                }
            }
            return listOfCustomers.Where(cust => cust.Id == customerId).First();
        }

        private StoreFront GetStore(int storeId)
        {
            List<StoreFront> loadedStores = new List<StoreFront>();
            string query = @"select * from [Store]";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    loadedStores.Add(new StoreFront() {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Items = GetLineItems(reader.GetInt32(0)),
                        Address = reader.GetString(2)
                    });
                }
            }
            return loadedStores.Where(store => store.Id == storeId).First();
        }

        private List<LineItem> GetLineItems(int storeId)
        {
            List<LineItem> loadedItems = new List<LineItem>();
            string query = @"select * from Store s join LineItem as si 
                on si.storeId = s.id and s.id = @storeId join Product as p on si.productId = p.id";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@storeId", storeId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    loadedItems.Add(new LineItem() {
                        Product = new Product() {
                            Id = reader.GetInt32(6),
                            Name = reader.GetString(7),
                            Price = reader.GetDouble(8),
                            Description = reader.GetString(9),
                            Category = reader.GetString(10),
                            MinimumAge = reader.GetInt32(11)
                        },
                        Quantity = reader.GetInt32(5)
                    });
                }
            }
            return loadedItems;
        }

        public Order PlaceOrder(int customerId, List<CartItem> item, int storeId)
        {
            Customer customer = GetCustomer(customerId);
            StoreFront store = GetStore(storeId);

            foreach(CartItem _item in item)
            {
                _item.Item = GetProductFromOrder(_item.Item.Id);
                if (!store.Items.Any(itemInStore => itemInStore.Product.Id == _item.Item.Id))
                    throw new Exception("This item is not for sale in "+ store.Name +".");
                if (customer.Age < _item.Item.MinimumAge)
                    throw new Exception("Customer is less than the required age to buy this product "+ _item.Item.Name+".");
                if(_item.Quantity <= 0)
                    throw new Exception("Quantity has to be more than 0 for item "+ _item.Item.Name +".");

                foreach(LineItem inventory in store.Items)
                {
                    if(inventory.Product.Id == _item.Item.Id)
                    {
                        if (_item.Quantity > inventory.Quantity)
                            throw new Exception("Cannot buy more than what is in inventory for product "+ _item.Item.Name +"!");
                    }
                }
            }

            Order order = new Order();
            string orderQuery = @"insert into [Order]
                values(@storeId, @totalPrice, @dateCreated); SELECT SCOPE_IDENTITY();";
            
            string purchasedQuery = @"insert into [PurchasedItem]
                values(@orderId, @productId, @quantity)";

            string soQuery = @"insert into [stores_orders]
                values(@storeId, @orderId)";

            string coQuery = @"insert into [customers_orders]
                values(@customerId, @orderId)";

            decimal totalPrice = (decimal) GetCartTotal(item);
            int totalQuantity = 0;
            DateTime dateCreated = DateTime.Now;
            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                //insert into order table
                SqlCommand command = new SqlCommand(orderQuery, connection);
                command.Parameters.AddWithValue("@storeId", storeId);
                command.Parameters.AddWithValue("@totalPrice", totalPrice);
                command.Parameters.AddWithValue("@dateCreated", dateCreated);

                int orderId = Convert.ToInt32(command.ExecuteScalar());

                List<PurchasedItem> items = new List<PurchasedItem>();
                
                //Insert into purhcaseditem table
                command = new SqlCommand(purchasedQuery, connection);
                foreach(CartItem _item in item)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@orderId", orderId);
                    command.Parameters.AddWithValue("@productId", _item.Item.Id);
                    command.Parameters.AddWithValue("@quantity", _item.Quantity);
                    command.ExecuteNonQuery();
                    items.Add(new PurchasedItem(){
                        OrderId = orderId,
                        Item = _item.Item,
                        Quantity = _item.Quantity
                    });
                    totalQuantity += _item.Quantity;
                }

                //insert into stores_orders table
                command = new SqlCommand(soQuery, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@storeId", storeId);
                command.Parameters.AddWithValue("@orderId", orderId);
                command.ExecuteNonQuery();

                //insert into customers_orders table
                command = new SqlCommand(coQuery, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@customerId", customerId);
                command.Parameters.AddWithValue("@orderId", orderId);
                command.ExecuteNonQuery();

                order.Id = orderId;
                order.Price = totalPrice;
                order.DateCreated = dateCreated;
                order.Items = items;
                order.Location = store.Address;
                order.Quantity = totalQuantity;
            }
            foreach(CartItem _item in item)
            {
                int oldQuantity = GetLineItems(storeId).Where(it => it.Product.Id == _item.Item.Id).First().Quantity;
                int newQuantity = oldQuantity - _item.Quantity;
                UpdateStoreInventory(storeId, _item.Item.Id, newQuantity);
            }
            return order;
        }

        private void UpdateStoreInventory(int storeId, int productId, int quantity)
        {
            string query = @"update LineItem set LineItem.quantity = @quantity
                where LineItem.storeId = @storeId and LineItem.productId = @productId";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@storeId", storeId);
                command.Parameters.AddWithValue("@productId", productId);
                command.Parameters.AddWithValue("@quantity", quantity);
                
                command.ExecuteNonQuery();
            }
        }

        public List<Order> GetOrders()
        {
            List<Order> _loadedOrders = new List<Order>();
            string query = @"select * from [Order]";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    _loadedOrders.Add(new Order() {
                        Id = reader.GetInt32(0), 
                        Price = reader.GetInt32(2),
                        DateCreated = reader.GetDateTime(3)
                    });
                }
            }
            return _loadedOrders;
        }

        public Product GetProductFromOrder(int productId)
        {
            Product product = new Product();
            List<Product> _loadedProducts = new List<Product>();
            string query = @"select * from [Product]";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    _loadedProducts.Add(new Product() {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDouble(2),
                        Description = reader.GetString(3),
                        Category = reader.GetString(4),
                        MinimumAge = reader.GetInt32(5)
                    });
                }
            }

            foreach(Product prod in _loadedProducts)
            {
                if (productId == prod.Id)
                    return prod;
            }
            return product;
        }

        /// <summary>
        /// Gets the total dollar value of the customer's cart.
        /// </summary>
        /// <param name="items">The list of items to be purchased.</param>
        /// <returns>The total dollar value of each item in the cart.</returns>
        public double GetCartTotal(List<CartItem> items)
        {
            double answer = 0.0;
            foreach(CartItem item in items)
                answer = answer + (item.Item.Price * item.Quantity);
            return answer;
        }
    }
}