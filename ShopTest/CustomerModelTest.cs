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
            string _name = "Carlos";
            string _address = "White House";
            string _phone = "1234";

            //act
            _customer.Name = _name;
            _customer.Address = _address;
            _customer.Phone = _phone;

            //assert
            Assert.NotNull(_customer);
        }
    }
}