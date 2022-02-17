using ShopModel;
using Xunit;

namespace ShopTest
{

    public class EmployeeModelTest
    {
        [Fact]
        public void EmployeeShouldSetValidData()
        {
            //arrange
            Employee _employee = new Employee();
            string _name = "Carlos";
            string _username = "WhiteHouse";
            string _password = "1234";

            //act
            _employee.Name = _name;
            _employee.Username = _username;
            _employee.Password = _password;

            //assert
            Assert.NotNull(_employee);
        }
    }
}