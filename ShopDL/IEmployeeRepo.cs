using ShopModel;

namespace ShopDL
{
    
    /*
     * An interface that deals with the loading and saving of employees.
     */
    public interface IEmployeeRepo
    {

        Employee AddEmployee(Employee employee);
 
        Employee UpdateEmployee(Employee employee);

        List<Employee> GetEmployees();
    }
}