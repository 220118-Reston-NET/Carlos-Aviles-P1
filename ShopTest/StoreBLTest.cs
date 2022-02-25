using System.Collections.Generic;
using Moq;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest
{

    public class StoreBLTest
    {

        [Fact]
        public void ShouldAddStore()
        {
            //arrange
            int id = 1;
            string name = "Nutrient North";
            string address = "Georgia";
            StoreFront store = new StoreFront() {
                Id = id,
                Name = name,
                Address = address
            };

            Mock<IStoreRepo> mockRepo = new Mock<IStoreRepo>();
            mockRepo.Setup(repo => repo.AddStore(store)).Returns(store);

            IStores Stores = new Stores(mockRepo.Object);

            //act
            StoreFront actualStore = Stores.AddStore(store);

            //assert
            Assert.Same(store, actualStore);
        }

        [Fact]
        public void StoreShouldUpdate()
        {
            //arrange
            int id = 1;
            string name = "Nutrient North";
            string address = "Georgia";
            StoreFront _Store = new StoreFront() {
                Id = id,
                Name = name,
                Address = address
            };
            List<StoreFront> expectedList = new List<StoreFront>();
            expectedList.Add(_Store);

            Mock<IStoreRepo> mockRepo = new Mock<IStoreRepo>();
            mockRepo.Setup(repo => repo.GetStores()).Returns(expectedList);

            IStores Stores = new Stores(mockRepo.Object);

            //act
            List<StoreFront> actualList = Stores.GetStores();

            //assert
            Assert.Same(expectedList, actualList);
            Assert.Equal(id, actualList[0].Id);
            Assert.Equal(name, actualList[0].Name);
        }

        [Fact]
        public void StoreShouldGetOrder()
        {
            //arrange
            Order order = new Order()
            {
                Id = 0,
                Quantity = 1,
                Location = "Store",
                Price = (decimal) 1.5
            };
            List<Order> expectedList = new List<Order>();
            expectedList.Add(order);

            Mock<IStoreRepo> mockRepo = new Mock<IStoreRepo>();
            mockRepo.Setup(repo => repo.GetOrders(order.Id)).Returns(expectedList);

            IStores Stores = new Stores(mockRepo.Object);

            //act
            List<Order> actualList = Stores.GetOrders(order.Id);

            //assert
            Assert.Same(expectedList, actualList);
            Assert.Equal(order.Id, actualList[0].Id);
            Assert.Equal(order.Price, actualList[0].Price);
        }
    }
}