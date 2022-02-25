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

        public Order PlaceOrder(int customerId, List<CartItem> item, int storeId)
        {
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
                        Item = _item.Item,
                        Quantity = _item.Quantity
                    });
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
                order.Location = ""+ storeId;
            }
            return order;
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