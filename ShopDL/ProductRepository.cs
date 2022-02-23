using System.Data.SqlClient;
using System.Text.Json;
using ShopModel;

namespace ShopDL
{

    /*
     * A repository that will deal with the loading and saving of product data.
     */
    public class ProductRepository : IProductRepo
    {

        /* The connection url link */        
         private readonly string connectionURL;

        /// <summary>
        /// Instantiates a new product repository instance.
        /// </summary>
        /// <param name="connectionURL">The connection url link</param>
         public ProductRepository(string connectionURL)
         {
            this.connectionURL = connectionURL;
         }

        public List<Product> GetProducts()
        {
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
            return _loadedProducts;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            List<Product> _loadedProducts = new List<Product>();
            string query = @"select * from [Product]";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
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

            return _loadedProducts;
        }
    }
}