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
            LineItem _item1 = new LineItem();
            Product _product1 = new Product();

            //act
            _item1.Product = _product1;
            _item1.Quantity = 5;
            _items.Add(_item1);

            //assert
            Assert.NotNull(_item1);
            Assert.NotEmpty(_items);
        }
    }
}