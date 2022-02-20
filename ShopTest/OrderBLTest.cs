using System.Collections.Generic;
using Moq;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest
{

    public class OrderBLTest
    {
        [Fact]
        public void OrderShouldSetValidData()
        {
            //arrange
            int id = 1;
            int quantity = 1;
            string location = "Nutrient North";
            List<PurchasedItem> listOfPurchasedItems = new List<PurchasedItem>();
            listOfPurchasedItems.Add(new PurchasedItem());
            Order _Order = new Order() {
                Id = id,
                Items = listOfPurchasedItems,
                Quantity = quantity,
                Location = location,
                Price = 20
            };
            List<Order> expectedList = new List<Order>();
            expectedList.Add(_Order);

            Mock<IOrderRepo> mockRepo = new Mock<IOrderRepo>();
            mockRepo.Setup(repo => repo.GetOrders()).Returns(expectedList);

            IOrders Orders = new Orders(mockRepo.Object);

            //act
            List<Order> actualList = Orders.GetOrders();

            //assert
            Assert.Same(expectedList, actualList);
            Assert.Equal(id, actualList[0].Id);
            Assert.Equal(listOfPurchasedItems, actualList[0].Items);
            Assert.Equal(quantity, actualList[0].Quantity);
            Assert.Equal(location, actualList[0].Location);
        }
    }
}