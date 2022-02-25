using ShopModel;
using Xunit;

namespace ShopTest
{

    public class CustomerModelTest
    {
        [Fact]
        public void CustomerShouldSetValidData()
        {
            //arrange
            Customer _customer = new Customer();
            int id = 1;
            string _name = "Carlos";
            string _address = "White House";
            string _phone = "1234";
            string username = "jaypowell";
            string password = "1234Pass";

            //act
            _customer.Id = id;
            _customer.Name = _name;
            _customer.Address = _address;
            _customer.Phone = _phone;
            _customer.Username = username;
            _customer.Password = password;

            //assert
            Assert.NotNull(_customer);
            Assert.Equal(id, _customer.Id);
            Assert.Equal(_name, _customer.Name);
            Assert.Equal(_address, _customer.Address);
            Assert.Equal(_phone, _customer.Phone);
            Assert.Equal(username, _customer.Username);
            Assert.Equal(password, _customer.Password);
        }
    }
}