using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest
{

    public class ProductBLTest
    {
        [Fact]
        public void ProductShouldSetValidData()
        {
            //arrange
            int id = 1;
            string name = "Steak";
            Product _Product = new Product() {
                Id = id,
                Name = name,
                Price = 50.0,
                Description = "A 12-inch steak tenderloin.",
                Category = "Food",
                MinimumAge = 12
            };
            List<Product> expectedList = new List<Product>();
            expectedList.Add(_Product);

            Mock<IProductRepo> mockRepo = new Mock<IProductRepo>();
            mockRepo.Setup(repo => repo.GetProducts()).Returns(expectedList);

            IProducts Products = new Products(mockRepo.Object);

            //act
            List<Product> actualList = Products.GetProducts();

            //assert
            Assert.Same(expectedList, actualList);
            Assert.Equal(id, actualList[0].Id);
            Assert.Equal(name, actualList[0].Name);
        }

        [Fact]
        public async Task GetProductsAsynchronous()
        {
            //arrange
            int id = 1;
            string name = "Steak";
            Product _Product = new Product() {
                Id = id,
                Name = name,
                Price = 50.0,
                Description = "A 12-inch steak tenderloin.",
                Category = "Food",
                MinimumAge = 12
            };
            List<Product> expectedList = new List<Product>();
            expectedList.Add(_Product);

            Mock<IProductRepo> mockRepo = new Mock<IProductRepo>();
            mockRepo.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(expectedList);

            IProducts Products = new Products(mockRepo.Object);

            //act
            List<Product> actualList = await Products.GetProductsAsync();

            //assert
            Assert.Same(expectedList, actualList);
            Assert.Equal(id, actualList[0].Id);
            Assert.Equal(name, actualList[0].Name);
        }
        
    }
}