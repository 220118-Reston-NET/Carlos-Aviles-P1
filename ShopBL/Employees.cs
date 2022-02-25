using ShopModel;
using ShopDL;

namespace ShopBL
{

    /*
     * Represents the handling of the employees repository.
     */
    public class Employees : IEmployees
    {

        /* The employee repository interface instance. */
        private IEmployeeRepo  repo;

        /// <summary>
        /// Instantiates a new employees instance.
        /// </summary>
        /// <param name="repo">The employee repository interface instance.</param>
        public Employees(IEmployeeRepo repo)
        {
            this.repo = repo;
        }

        public Employee AddEmployee(Employee employee)
        {
            return repo.AddEmployee(employee);
        }

        public Employee UpdateEmployee(Employee employee)
        {
            return repo.UpdateEmployee(employee);
        }

        public List<Employee> GetEmployees()
        {
            return repo.GetEmployees();
        }

        public int LoginEmployee(string username, string password)
        {
            return repo.LoginEmployee(username, password);
        }
    }
}