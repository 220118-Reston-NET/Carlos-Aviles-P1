using ShopModel;
using Xunit;

namespace ShopTest
{

    public class StoreModelTest
    {
        [Fact]
        public void StoreShouldSetValidData()
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
    }
}