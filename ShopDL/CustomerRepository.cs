using System.Data.SqlClient;
using System.Text.Json;
using ShopModel;

namespace ShopDL
{
    /*
     * A repository that will deal with the loading and saving of customer data through SQL.
     */
    public class CustomerRepository : ICustomerRepo
    {

        /* The connection url link */  
        private readonly string connectionURL;

        /// <summary>
        /// Instantiates a new customer repository instance.
        /// </summary>
        /// <param name="connectionURL">The connection url link</param>
        public CustomerRepository(string connectionURL)
        {
            this.connectionURL = connectionURL;
        }
        
        public Customer AddCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("CreateCustomer", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@age", customer.Age);
                command.Parameters.AddWithValue("@address", customer.Address);
                command.Parameters.AddWithValue("@phone", customer.Phone);
                command.Parameters.AddWithValue("@username",customer.Username);
                command.Parameters.AddWithValue("@password", customer.Password);
                
                command.ExecuteNonQuery();
            }
            return customer;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            string query = @"update [Customer]
                set name=@custName, age=@custAge, address=@custAddress, phone=@custPhone
                where id=@custId;";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@custId", customer.Id);
                command.Parameters.AddWithValue("@custName", customer.Name);
                command.Parameters.AddWithValue("@custAge", customer.Age);
                command.Parameters.AddWithValue("@custAddress", customer.Address);
                command.Parameters.AddWithValue("@custPhone", customer.Phone);

                command.ExecuteNonQuery();
            }
            return customer;
        }

        public Customer DeleteCustomer(Customer customer)
        {
            string query = @"delete from [Customer] where id=@customerId;";

            using(SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@customerId", customer.Id);

                command.ExecuteNonQuery();
            }
            return customer;
        }

        public List<Customer> GetCustomers()
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
                        Address = reader.GetString(3),
                        Phone = reader.GetString(4),
                        Orders = GetOrders(reader.GetInt32(0)),
                        Username = reader.GetString(5)
                    });
                }
            }

            return listOfCustomers;
        }

        public int LoginCustomer(string username, string password)
        {
            int loginResult = 0;
            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("CustomerLogin", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                
                loginResult = Convert.ToInt32(command.ExecuteScalar());
            }
            return loginResult;
        }

        public List<Order> GetOrders(int customerId)
        {
            List<Order> loadedOrders = new List<Order>();
            string query = @"select * from [customers_orders] as co 
                join [Order] as o on o.id = co.orderId and co.customerId = @customerId
                join [Store] as s on s.id = o.storeId";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@customerId", customerId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    loadedOrders.Add(new Order() {
                        Id = reader.GetInt32(1),
                        Items = GetPurchasedItems(reader.GetInt32(1)),
                        Quantity = GetTotalQuantityFromOrder(GetPurchasedItems(reader.GetInt32(1))),
                        Location = reader.GetString(7),
                        Price = (decimal) reader.GetSqlMoney(4),
                        DateCreated = reader.GetDateTime(5)
                    });
                }
            }
            return loadedOrders;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            List<Customer> listOfCustomers = new List<Customer>();
            string query = @"select * from [Customer]";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    listOfCustomers.Add(new Customer() {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Age = reader.GetInt32(2),
                        Address = reader.GetString(3),
                        Phone = reader.GetString(4),
                        Orders = GetOrders(reader.GetInt32(0)),
                        Username = reader.GetString(5)
                    });
                }
            }

            return listOfCustomers;
        }

        public List<PurchasedItem> GetPurchasedItems(int orderId)
        {
            List<PurchasedItem> loadedItems = new List<PurchasedItem>();
            string query = @"select * from [PurchasedItem] as item
                join [stores_orders] as so on item.orderId = so.orderId and so.orderId = @orderId
                join [Product] as p on item.productId = p.id";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    loadedItems.Add(new PurchasedItem() {
                        OrderId = reader.GetInt32(0),
                        Item = new Product() {
                            Id = reader.GetInt32(1),
                            Name = reader.GetString(6),
                            Price = (double) reader.GetSqlDouble(7)
                        },
                        Quantity = reader.GetInt32(2)
                    });
                }
            }
            return loadedItems;
        }

        public int GetTotalQuantityFromOrder(List<PurchasedItem> loadedItems)
        {
            int total = 0;
            foreach(PurchasedItem item in loadedItems)
                total = total + item.Quantity;
            return total;
        }

        public Customer GetCustomerFromId(int customerId)
        {
            return GetCustomers().First(customer => customer.Id == customerId);
        }

        public bool SearchCustomerByExactName(string name) {
            for(int index = 0; index < GetCustomers().Count; index++)
            {
                Customer customer = GetCustomers()[index];
                if(customer.Name == name)
                    return true;
            }
            return false;
        }

        public bool SearchCustomerByName(string name)
        {
            bool flag = GetCustomers().Any(customer => customer.Name.Contains(name));
            return flag;
        }

        public bool SearchCustomerByExactAddress(string address)
        {
            for(int index = 0; index < GetCustomers().Count; index++)
            {
                Customer customer = GetCustomers()[index];
                if(customer.Address == address)
                    return true;
            }
            return false;
        }

        public bool SearchCustomerByAddress(string address)
        {
            bool flag = GetCustomers().Any(customers => customers.Address.Contains(address));
            return flag;
        }

        public bool SearchCustomerByPhone(string phone)
        {
            for(int index = 0; index < GetCustomers().Count; index++)
            {
                Customer customer = GetCustomers()[index];
                if(customer.Phone == phone)
                    return true;
            }
            return false;
        }

        public List<Customer> GetCustomersWithExactName(string name)
        {
            return GetCustomers().FindAll(customer => customer.Name.Equals(name));
        }

        public List<Customer> GetSimilarCustomersByName(string name)
        {
            return GetCustomers().FindAll(customer => customer.Name.Contains(name));
        }

        public List<Customer> GetSimilarCustomersByAddress(string address)
        {
            return GetCustomers().FindAll(customer => customer.Address.Contains(address));
        }

        public Customer GetCustomerByName(string name)
        {
            return GetCustomers().Find(customer => customer.Name.Equals(name));
        }

        public Customer GetCustomerByAddress(string address)
        {
            return GetCustomers().Find(customer => customer.Address.Equals(address));
        }

        public Customer GetCustomerByPhone(string phone)
        {
            return GetCustomers().Find(customer => customer.Phone.Equals(phone));
        }

        public bool CanAddCustomer(Customer customer)
        {
            if (customer.Name != "" && customer.Address != "" && customer.Phone != "" && (customer.Age >= 0 && customer.Age <= 120) &&
                customer.Username != "" && customer.Password != "")
                return true;
            return false;
        }

        public Customer GetCustomerFromOrder(int id)
        {
            foreach(Customer customer in GetCustomers())
            {
                foreach(Order order in customer.Orders)
                {
                    if(order.Id == id)
                        return customer;
                }
            }
            return null;
        }

        public Customer GetCustomerFromUsername(string username)
        {
            return GetCustomers().Find(customer => customer.Username == username);
        }

        public string DisplayOrderHistory(List<Order> orders)
        {
            string text = "";
            foreach (Order _order in orders)
            {
                Customer _customer = GetCustomerFromOrder(_order.Id);
                text += "\nThis order ("+ _order.Id +") from "+ _customer.Name +" was a total of $"+ _order.Price +"\n";
                foreach (PurchasedItem _item in _order.Items)
                    text += ("- "+ _item.Item.Name +" was purchased for $"+ _item.Item.Price +" each ("+ _item.Quantity +"x)\n");
                text += "This order was created on "+ _order.DateCreated+" in "+ _order.Location +"\n";
            }
            return text;
        }
    }
}