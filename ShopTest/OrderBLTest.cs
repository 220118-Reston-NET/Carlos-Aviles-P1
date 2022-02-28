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
            List<CartItem> listOfItems = new List<CartItem>();
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
            Assert.Equal(0, Orders.GetCartTotal(listOfItems));
        }

        [Fact]
        public void OrderShouldThrowInvalidCart()
        {
            //arrange
            int id = 1;
            int storeId = 1;
            int quantity = 1;
            string location = "Nutrient North";
            List<CartItem> listOfItems = new List<CartItem>();
            Order _Order = new Order() {
                Id = id,
                Quantity = quantity,
                Location = location,
                Price = 20
            };

            Mock<IOrderRepo> mockRepo = new Mock<IOrderRepo>();
            mockRepo.Setup(repo => repo.PlaceOrder(id, listOfItems, storeId)).Returns(_Order);

            IOrders Orders = new Orders(mockRepo.Object);

            //assert
            var ex = Assert.ThrowsAny<System.Exception>(() => Orders.PlaceOrder(id, listOfItems, storeId));
            Assert.Equal(ex.Message, "You don't have anything in your cart!");
        }

        [Fact]
        public void OrderShouldThrowInvalidQuantity()
        {
            //arrange
            int id = 1;
            int storeId = 1;
            int quantity = 1;
            string location = "Southern South";
            List<CartItem> listOfItems = new List<CartItem>();
            listOfItems.Add(new CartItem());
            Order _Order = new Order() {
                Id = id,
                Quantity = quantity,
                Location = location,
                Price = 20
            };

            Mock<IOrderRepo> mockRepo = new Mock<IOrderRepo>();
            mockRepo.Setup(repo => repo.PlaceOrder(id, listOfItems, storeId)).Returns(_Order);

            IOrders Orders = new Orders(mockRepo.Object);

            //assert
            Assert.ThrowsAny<System.Exception>(() => Orders.PlaceOrder(id, listOfItems, storeId));
        }
    }
}