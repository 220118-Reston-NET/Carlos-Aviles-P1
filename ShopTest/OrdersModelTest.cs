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

            //act
            _item.Item = _product1;
            _item.Quantity = 1;
            _orders.Items.Add(_item);
            _orders.Location = _store1.Address;
            _orders.Price = (decimal) _product1.Price;

            //assert
            Assert.NotNull(_product1.Price);
            Assert.NotEmpty(_orders.Items);
            Assert.NotNull(_store1);
        }
    }
}