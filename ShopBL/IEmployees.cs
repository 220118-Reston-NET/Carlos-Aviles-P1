using ShopModel;

namespace ShopBL
{

    /*
     * An interface that deals with the loading and saving of employees.
     */
    public interface IEmployees
    {
        Employee AddEmployee(Employee employee);
 
        Employee UpdateEmployee(Employee employee);

        List<Employee> GetEmployees();

        List<Employee> GetEmployeesFromStoreId(int storeId);
    }
}