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
        public void ShouldAddCustomer()
        {
            //arrange
            string name = "Smith";
            Customer _customer = new Customer() {
                Name = name,
                Address = "Red House",
                Age = 45

            };

            Mock<ICustomerRepo> mockRepo = new Mock<ICustomerRepo>();
            mockRepo.Setup(repo => repo.AddCustomer(_customer)).Returns(_customer);

            ICustomers customers = new Customers(mockRepo.Object);

            //act
            Customer actualCustomer = customers.AddCustomer(_customer);

            //assert
            Assert.Same(_customer, actualCustomer);
            Assert.NotNull(actualCustomer);
        }

        [Fact]
        public void ShouldUpdateCustomer()
        {
            //arrange
            string name = "Abigail";
            Customer _customer = new Customer() {
                Name = name,
                Address = "Wood House",
                Phone = "123456789",
                Age = 18

            };

            Mock<ICustomerRepo> mockRepo = new Mock<ICustomerRepo>();
            mockRepo.Setup(repo => repo.UpdateCustomer(_customer)).Returns(_customer);

            ICustomers customers = new Customers(mockRepo.Object);

            //act
            Customer actualCustomer = customers.UpdateCustomer(_customer);

            //assert
            Assert.Same(_customer, actualCustomer);
            Assert.NotNull(actualCustomer);
        }

        [Fact]
        public void ShouldDeleteCustomer()
        {
            //arrange
            string name = "Abigail Williams";
            Customer _customer = new Customer() {
                Name = name,
                Address = "A ouse",
                Age = 17

            };

            Mock<ICustomerRepo> mockRepo = new Mock<ICustomerRepo>();
            mockRepo.Setup(repo => repo.DeleteCustomer(_customer)).Returns(_customer);

            ICustomers customers = new Customers(mockRepo.Object);

            //act
            Customer actualCustomer = customers.DeleteCustomer(_customer);

            //assert
            Assert.Same(_customer, actualCustomer);
            Assert.NotNull(actualCustomer);
        }

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

        [Fact]
        public void CustomerShouldGetOrder()
        {
            //arrange
            Order order = new Order()
            {
                Id = 5,
                Quantity = 5,
                Location = "Store2",
                Price = (decimal) 1.55
            };
            List<Order> expectedList = new List<Order>();
            expectedList.Add(order);

            Mock<ICustomerRepo> mockRepo = new Mock<ICustomerRepo>();
            mockRepo.Setup(repo => repo.GetOrders(order.Id)).Returns(expectedList);

            ICustomers customers = new Customers(mockRepo.Object);

            //act
            List<Order> actualList = customers.GetOrders(order.Id);

            //assert
            Assert.Same(expectedList, actualList);
            Assert.Equal(order.Id, actualList[0].Id);
            Assert.Equal(order.Price, actualList[0].Price);
        }

        [Fact]
        public void ShouldGetCustomerFromId()
        {
            //arrange
            int id = 1;
            int nullId = 2;
            
            Customer expectingCustomer = new Customer();
            Mock<ICustomerRepo> mockRepo = new Mock<ICustomerRepo>();
            mockRepo.Setup(repo => repo.GetCustomerFromId(id)).Returns(expectingCustomer);

            ICustomers customers = new Customers(mockRepo.Object);

            //act
            Customer notNullCustomer = customers.GetCustomerFromId(id);
            Customer nullCust = customers.GetCustomerFromId(nullId);

            //assert
            Assert.Same(expectingCustomer, notNullCustomer);
            Assert.NotNull(notNullCustomer);
            Assert.Null(nullCust);
        }
    }
}