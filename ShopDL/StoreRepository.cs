using System.Data.SqlClient;
using System.Text.Json;
using ShopModel;

namespace ShopDL
{
    
    /*
     * A repository that will deal with the loading and saving of store data.
     */
    public class StoreRepository : IStoreRepo
    {
        
        /* The connection url link */
        private readonly string connectionURL;

        /// <summary>
        /// Instantiates a new store repository instance.
        /// </summary>
        /// <param name="connectionURL">The connection url.</param>
        public StoreRepository(string connectionURL)
        {
            this.connectionURL = connectionURL;
        }

        public StoreFront AddStore(StoreFront store)
        {
            string query = @"insert into [Store]
                values(@storeId, @storeName, @storeAddress)";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@storeId", store.Id);
                command.Parameters.AddWithValue("@storeName", store.Name);
                command.Parameters.AddWithValue("@storeAddress", store.Address);

                command.ExecuteNonQuery();
            }
            return store;
        }

        public StoreFront UpdateStore(StoreFront store)
        {
             string query = @"update [Store]
                set name=@storeName, address=@storeAddress
                where id=@storeId;";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@storeId", store.Id);
                command.Parameters.AddWithValue("@storeName", store.Name);
                command.Parameters.AddWithValue("@storeAddress", store.Address);

                command.ExecuteNonQuery();
            }
            return store;
        }

        public void UpdateStoreInventory(int storeId, int productId, int quantity)
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

        public List<StoreFront> GetStores()
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
                        Address = reader.GetString(2),
                        Items = GetLineItems(reader.GetInt32(0)),
                        Orders = GetOrders(reader.GetInt32(0))
                    });
                }
            }
            return loadedStores;
        }

        public List<LineItem> GetLineItems(int storeId)
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

        public List<Order> GetOrders(int storeId)
        {
            List<Order> loadedOrders = new List<Order>();
            string query = @"select * from [stores_orders] as so
                join [Order] as o on o.id = so.orderId and so.storeId = @storeId
                join [Store] as s on s.id = @storeId";
            
            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@storeId", storeId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    loadedOrders.Add(new Order() {
                        Id = reader.GetInt32(1),
                        Items = GetPurchasedItems(reader.GetInt32(1)),
                        Quantity = GetTotalQuantityFromOrder(GetPurchasedItems(reader.GetInt32(1))),
                        Price = (decimal) reader.GetSqlMoney(4),
                        DateCreated = reader.GetDateTime(5)
                    });
                }
            }
            return loadedOrders;
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

        public StoreFront GetStoreFront(int id)
        {
            return GetStores().Where(store => store.Id == id).First();
        }

        public int GetTotalQuantityFromOrder(List<PurchasedItem> loadedItems)
        {
            int total = 0;
            foreach(PurchasedItem item in loadedItems)
                total = total + item.Quantity;
            return total;
        }

        public bool SearchStoreByExactName(string name)
        {
            for(int index = 0; index < GetStores().Count; index++)
            {
                StoreFront store = GetStores()[index];
                if(store.Name == name)
                    return true;
            }
            return false;
        }

        public bool SearchStoreByName(string name)
        {
            bool flag = GetStores().Any(store => store.Name.Contains(name));
            return flag;
        }

        public bool SearchStoreByExactAddress(string address)
        {
            for(int index = 0; index < GetStores().Count; index++)
            {
                StoreFront store = GetStores()[index];
                if(store.Address == address)
                    return true;
            }
            return false;
        }

        public bool SearchStoreByAddress(string address)
        {
            bool flag = GetStores().Any(store => store.Address.Contains(address));
            return flag;
        }

        public List<StoreFront> GetSimilarStoresByName(string name)
        {
            return GetStores().FindAll(store => store.Name.Contains(name));   
        }

        public List<StoreFront> GetSimilarStoresByAddress(string address)
        {
            return GetStores().FindAll(store => store.Address.Contains(address));
        }

        public bool hasInventory(StoreFront store)
        {
            int quantity = 0;
            foreach(LineItem item in store.Items)
            {
                if(item.Quantity > 0)
                    quantity++;
            }
                if (quantity != 0)
                    return true;
            return false;
        }
    }
}