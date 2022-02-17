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
        public void StoreShouldSetValidData()
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
    }
}