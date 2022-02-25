using System.Collections.Generic;
using Moq;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest
{

    public class EmployeeBLTest
    {

        [Fact]
        public void ShouldAddEmployee()
        {
            //arrange
            string name = "Abigail Smith";
            string user = "abig";
            Employee _employee = new Employee() {
                Name = name,
                Username = user
            };

            Mock<IEmployeeRepo> mockRepo = new Mock<IEmployeeRepo>();
            mockRepo.Setup(repo => repo.AddEmployee(_employee)).Returns(_employee);

            IEmployees employees = new Employees(mockRepo.Object);

            //act
            Employee actual = employees.AddEmployee(_employee);

            //assert
            Assert.Same(_employee, actual);
            Assert.NotNull(actual);
        }

        [Fact]
        public void ShouldUpdateEmployee()
        {
            //arrange
            string name = "Abigail Smith";
            string user = "abig";
            Employee _employee = new Employee() {
                Name = name,
                Username = user
            };

            Mock<IEmployeeRepo> mockRepo = new Mock<IEmployeeRepo>();
            mockRepo.Setup(repo => repo.UpdateEmployee(_employee)).Returns(_employee);

            IEmployees employees = new Employees(mockRepo.Object);

            //act
            Employee actual = employees.UpdateEmployee(_employee);

            //assert
            Assert.Same(_employee, actual);
            Assert.NotNull(actual);
        }

        [Fact]
        public void EmployeeShouldSetValidData()
        {
            //arrange
            string name = "Abigail Smith";
            string user = "abig";
            Employee _employee = new Employee() {
                Name = name,
                Username = user
            };
            List<Employee> expectedList = new List<Employee>();
            expectedList.Add(_employee);

            Mock<IEmployeeRepo> mockRepo = new Mock<IEmployeeRepo>();
            mockRepo.Setup(repo => repo.GetEmployees()).Returns(expectedList);

            IEmployees employees = new Employees(mockRepo.Object);

            //act
            List<Employee> actualList = employees.GetEmployees();

            //assert
            Assert.Same(expectedList, actualList);
            Assert.Equal(name, actualList[0].Name);
        }
    }
}