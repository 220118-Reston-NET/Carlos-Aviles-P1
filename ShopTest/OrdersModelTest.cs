using System.Collections.Generic;
using ShopModel;
using Xunit;

namespace ShopTest
{

    public class OrdersModelTest
    {
        [Fact]
        public void OrdersShouldSetValidData()
        {
            //arrange
            StoreFront _store1 = new StoreFront();
            Order _orders = new Order();
            _orders.Items = new List<PurchasedItem>();
            PurchasedItem _item = new PurchasedItem();
            Product _product1 = new Product();
            int orderId = 1;
            int quantity = 1;
            PurchasedItem _item2 = new PurchasedItem(orderId, _product1, quantity);

            //act
            _item.OrderId = orderId;
            _item.Item = _product1;
            _item.Quantity = quantity;
            _orders.Items.Add(_item);
            _orders.Location = _store1.Address;
            _orders.Price = (decimal) _product1.Price;

            //assert
            Assert.NotNull(_product1.Price);
            Assert.NotEmpty(_orders.Items);
            Assert.NotNull(_store1);
            Assert.Equal(_item.Quantity, quantity);
            Assert.Equal(_item.Item, _product1);
            Assert.Equal(_item.OrderId, orderId);
        }
    }
}