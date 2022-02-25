using System.Collections.Generic;
using ShopModel;
using Xunit;

namespace ShopTest
{

    public class LineItemsModelTest
    {
        [Fact]
        public void LineItemsShouldSetValidData()
        {
            //arrange
            List<LineItem> _items = new List<LineItem>();
            Product _product1 = new Product();
            LineItem _item = new LineItem();
            LineItem _item1 = new LineItem(_product1, 5);

            //act
            _item1.Product = _product1;
            _item1.Quantity = 5;
            _items.Add(_item1);
            _items.Add(_item);

            //assert
            Assert.NotNull(_item1);
            Assert.NotEmpty(_items);
            Assert.Equal(5, _item1.Quantity);
            Assert.Equal(_product1, _item1.Product);
        }
    }
}