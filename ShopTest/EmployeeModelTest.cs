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

            int id = 0;
            int storeId = 0;
            string _name = "Carlos";
            string _username = "WhiteHouse";
            byte[] password = new byte[4 * 4];

            //act
            _employee.Name = _name;
            _employee.Username = _username;
            _employee.Id = id;
            _employee.StoreId = storeId;
            _employee.Password = password;

            //assert
            Assert.NotNull(_employee);
        }
    }
}