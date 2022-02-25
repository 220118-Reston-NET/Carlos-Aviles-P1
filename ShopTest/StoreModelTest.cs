using System.Collections.Generic;
using ShopModel;
using Xunit;

namespace ShopTest
{

    public class StoreModelTest
    {
        [Fact]
        public void StoreShouldSetValidName()
        {
            //arrange
            StoreFront _store = new StoreFront();
            string _name = "Store";

            //act
            _store.Name = _name;

            //assert
            Assert.NotNull(_store.Name);
            Assert.Equal(_name, _store.Name);
        }

        [Fact]
        public void StoreShouldSetValidId()
        {
            //arrange
            StoreFront _store = new StoreFront();
            int id = 1;

            //act
            _store.Id = id;

            //assert
            Assert.NotNull(_store.Id);
            Assert.Equal(id, _store.Id);
        }

        [Fact]
        public void StoreShouldSetValidAddress()
        {
            //arrange
            StoreFront _store = new StoreFront();
            string address = "Store";

            //act
            _store.Address = address;

            //assert
            Assert.NotNull(_store.Address);
            Assert.Equal(address, _store.Address);
        }

        [Fact]
        public void StoreShouldSetValidOrders()
        {
            //arrange
            StoreFront _store = new StoreFront();
            List<Order> orders = new List<Order>();

            //act
            _store.Orders = orders;

            //assert
            Assert.NotNull(_store.Orders);
            Assert.Equal(orders, _store.Orders);
        }
    }
}