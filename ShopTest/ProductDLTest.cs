using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest
{
    public abstract class ProductSql : IDisposable
    {
        private const string connectionURL = "DataSource=:memory:?Cache=Shared;";
        private readonly SqliteConnection connection;
        protected readonly ProductDbContext context;

        protected ProductSql()
        {
            connection = new SqliteConnection(connectionURL);
            connection.Open();
            var options = new DbContextOptionsBuilder<ProductDbContext>().UseSqlite(connection).Options;
            context = new ProductDbContext(options);
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }

    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }

    public class ProductDLTest : ProductSql
    {

        [Theory]
        [InlineData(1, "Iron Sword", 25.00, "A sword made out of iron.", "Mercenary", 18)]
        [InlineData(2, "Gold Sword", 50.00, "A sword made out of 24k gold.", "Mercenary", 18)]
        public async Task CreateProduct(int id, string name, float price, string description, string category, int minAge)
        {
            Product product = new Product();
            product.Id = id;
            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.Category = category;
            product.MinimumAge = minAge;
            await context.AddAsync(product);
            await context.SaveChangesAsync();

            Assert.Equal(product, context.Products.Find(product.Id));
        }
        
        [Fact]
        public async Task GetProducts()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<IProductRepo>().Setup(x => x.GetProducts()).Returns(GetSample());

                var cls = mock.Create<Products>();
                var expected = GetSample();
                var actual = cls.GetProducts();
                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual.Count);

                for (int index = 0; index < expected.Count; index++)
                {
                    Assert.Equal(expected[index].Id, actual[index].Id);
                    Assert.Equal(expected[index].Name, actual[index].Name);
                }
            }
        }

        public List<Product> GetSample()
        {
            List<Product> output = new List<Product>
            {
                new Product
                {
                    Id = 0,
                    Name = "Iron Sword"
                }
            };
            return output;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task DeleteProduct(int id)
        {
            Product product = context.Products.Find(id);
            context.Remove(product);
            await context.SaveChangesAsync();
            Product check = context.Products.Find(id);

            Assert.Null(check);
        }
    }
}