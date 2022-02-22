using System;
using System.Collections.Generic;
using Autofac.Extras.Moq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest
{

    public abstract class StoreSql : IDisposable
    {
        protected const string connectionURL = "DataSource=:memory:?Cache=Shared;";
        private readonly SqliteConnection connection;
        protected readonly StoreDbContext context;

        protected StoreSql()
        {
            connection = new SqliteConnection(connectionURL);
            connection.Open();
            var options = new DbContextOptionsBuilder<StoreDbContext>().UseSqlite(connection).Options;
            context = new StoreDbContext(options);
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }

    public class StoreDbContext : DbContext
    {
        public DbSet<StoreFront> Stores { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StoreFront>(eb => {
               eb.Ignore(k=> k.Items);
               eb.Ignore(k=> k.Orders);
            });
        }
    }

    public class StoreDLTest : StoreSql
    {

        [Theory]
        [InlineData(1, "Wild West", "The west of the wilderness.")]
        public void CreateStore(int id, string name, string address)
        {
            StoreFront store = new StoreFront();

            store.Id = id;
            store.Name = name;
            store.Address = address;
            context.Add(store);
            context.SaveChanges();
            
            Assert.Equal(store, context.Stores.Find(store.Id));
        }

        [Fact]
        public void GetAllStores()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<IStoreRepo>().Setup(x => x.GetStores()).Returns(GetSample());

                var cls = mock.Create<Stores>();
                var expected = GetSample();
                var actual = cls.GetStores();

                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual.Count);

                for (int index = 0; index < expected.Count; index++)
                {
                    Assert.Equal(expected[index].Id, actual[index].Id);
                    Assert.Equal(expected[index].Name, actual[index].Name);
                }
            }
        }

        public List<StoreFront> GetSample()
        {
            List<StoreFront> output = new List<StoreFront>
            {
                new StoreFront
                {
                    Id = 1,
                    Name = "Iron Sword"
                }
            };
            return output;
        }

        [Theory]
        [InlineData(1)]
        public void DeleteStore(int id)
        {
            StoreFront store = context.Stores.Find(id);
            context.Remove(store);
            context.SaveChanges();
            StoreFront check = context.Stores.Find(id);

            Assert.Null(check);
        }
    }
}