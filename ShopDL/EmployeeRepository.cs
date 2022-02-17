using System.Data.SqlClient;
using ShopModel;

namespace ShopDL
{
    /*
     * A repository that will deal with the loading and saving of employee data through SQL.
     */
    public class EmployeeRepository : IEmployeeRepo
    {

        /* The connection url link */  
        private readonly string connectionURL;

        /// <summary>
        /// Instantiates a new customer repository instance.
        /// </summary>
        /// <param name="connectionURL">The connection url link</param>
        public EmployeeRepository(String connectionURL)
        {
            this.connectionURL = connectionURL;
        }

        public Employee AddEmployee(Employee employee)
        {
            string query = @"insert into [Employee]
                values(@empName, @empUser, @empPass)";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@empName", employee.Name);
                command.Parameters.AddWithValue("@empUser", employee.Username);
                command.Parameters.AddWithValue("@empPass", employee.Username);

                command.ExecuteNonQuery();
            }
            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            string query = @"update [Employee]
                set name=@empName, username=@empUser, password=@empPass
                where id=@empId;";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@empId", employee.Id);
                command.Parameters.AddWithValue("@empName", employee.Name);
                command.Parameters.AddWithValue("@empUser", employee.Username);
                command.Parameters.AddWithValue("@empPass", employee.Username);

                command.ExecuteNonQuery();
            }
            return employee;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> listOfEmployees = new List<Employee>();
            string query = @"select * from [Employee]";

            using (SqlConnection connection = new SqlConnection(connectionURL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfEmployees.Add(new Employee() {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Username = reader.GetString(2),
                        Password = reader.GetString(3)
                    });
                }
            }

            return listOfEmployees;
        }
    }
}