using ShopModel;

namespace ShopDL
{
    
    /*
     * An interface that deals with the loading and saving of employees.
     */
    public interface IEmployeeRepo
    {

        /// <summary>
        /// Adds an employee to the database.
        /// </summary>
        /// <param name="employee">The employee instance.</param>
        /// <returns>The employee instance.</returns>
        Employee AddEmployee(Employee employee);
 
        /// <summary>
        /// Updates an employee in the database.
        /// </summary>
        /// <param name="employee">The employee instance.</param>
        /// <returns>The employee instance.</returns>
        Employee UpdateEmployee(Employee employee);

        /// <summary>
        /// Gets all the employees in the database and places them in a list.
        /// </summary>
        /// <returns>The list of employees.</returns>
        List<Employee> GetEmployees();

        /// <summary>
        /// Connects with a SQL procedure that returns a login response upon a successful or unsuccessful employee login.
        /// </summary>
        /// <param name="username">The username the user provided.</param>
        /// <param name="password">The password the user provided.</param>
        /// <returns>The login response code of the login.</returns>
        int LoginEmployee(string username, string password);

        /// <summary>
        /// Gets all the employees that work in a particular store, and places them in a list.
        /// </summary>
        /// <param name="storeId">The unique store identification.</param>
        /// <returns>The list of employees.</returns>
        List<Employee> GetEmployeesFromStoreId(int storeId);

        /// <summary>
        /// Gets the instance of an employee by finding them via their username.
        /// </summary>
        /// <param name="username">The provided username.</param>
        /// <returns>The employee instance.</returns>
        Employee GetEmployeeFromUsername(string username);
    }
}