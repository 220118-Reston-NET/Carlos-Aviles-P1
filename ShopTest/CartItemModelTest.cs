using ShopModel;
using Xunit;

namespace ShopTest
{

    public class CartItemModelTest
    {
        [Fact]
        public void CartItemShouldSetValidData()
        {
            //arrange
            int id = 1;
            string name = "Steak";
            int quantity = 1;
            Product _product = new Product();
            CartItem _CartItem = new CartItem(_product, quantity);
            CartItem _cart = new CartItem();

            //act
            _product.Id = id;
            _product.Name = name;
            _product.Price = 50.0;
            _product.Description = "A 12-inch steak tenderloin.";
            _product.Category = "Food";
            _product.MinimumAge = 12;

            //assert
            Assert.NotNull(_CartItem.Item);
            Assert.Equal(_CartItem.Quantity, quantity);
            Assert.NotNull(_product);
            Assert.NotNull(_cart);
        }
    }
}