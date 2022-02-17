using ShopModel;
using Xunit;

namespace ShopTest
{

    public class ProductModelTest
    {
        [Fact]
        public void StoreShouldSetValidData()
        {
            //arrange
            Product _product = new Product();
            string _name = "Water";

            //act
            _product.Name = _name;
            _product.Description = "A 12 pack of water bottles";
            _product.Price = 12;
            _product.Category = "Drinks";

            //assert
            Assert.NotNull(_product.Name);
            Assert.NotNull(_product.Description);
            Assert.NotNull(_product.Price);
            Assert.NotNull(_product.Category);
        }
    }
}