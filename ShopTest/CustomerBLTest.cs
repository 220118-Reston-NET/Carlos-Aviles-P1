using System.Collections.Generic;
using Moq;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest
{

    public class CustomerBLTest
    {
        [Fact]
        public void CustomerShouldSetValidData()
        {
            //arrange
            string name = "Abigail Smith";
            Customer _customer = new Customer() {
                Name = name,
                Address = "Wood House",
                Phone = "123456789",
                Age = 18

            };
            List<Customer> expectedList = new List<Customer>();
            expectedList.Add(_customer);

            Mock<ICustomerRepo> mockRepo = new Mock<ICustomerRepo>();
            mockRepo.Setup(repo => repo.GetCustomers()).Returns(expectedList);

            ICustomers customers = new Customers(mockRepo.Object);

            //act
            List<Customer> actualList = customers.GetCustomers();

            //assert
            Assert.Same(expectedList, actualList);
            Assert.Equal(name, actualList[0].Name);
        }
    }
}