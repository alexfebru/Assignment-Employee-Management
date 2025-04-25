using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem;

namespace RuppSystem.model
{
    public class EmployeeRepository : IEmployeeRepository
    {

     /*   private readonly SqlConnection _connection;*/


        SqlConnection _connection = DatabaseConnection.Instance.GetConnection();

        private Dictionary<string, decimal> positionSalaryMap = new Dictionary<string, decimal>()
        {
            { "Business Management", 10000 },
            { "Front-End Developer", 7000 },
            { "Back-End Developer", 8000 },
            { "Data Administrator", 12000 },
            { "UI/UX Design", 3000 },
            { "Tester", 2500 },
        };

        public EmployeeRepository()
        {
            _connection = DatabaseConnection.Instance.GetConnection();
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            string query = "SELECT * FROM employees WHERE delete_date IS NULL";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeID = reader["employee_id"].ToString(),
                            FullName = reader["full_name"].ToString(),
                            Gender = reader["gender"].ToString(),
                            ContactNumber = reader["contact_number"].ToString(),
                            Position = reader["position"].ToString(),
                            ImagePath = reader["image"].ToString(),
                            Salary = Convert.ToDecimal(reader["salary"]),
                            InsertDate = Convert.ToDateTime(reader["insert_date"]),
                            Status = reader["status"].ToString()
                        });
                    }
                }
                _connection.Close();
            }
            return employees;
        }

        public Employee GetEmployeeById(string id)
        {
            Employee employee = null;
            string query = "SELECT * FROM employees WHERE employee_id = @id AND delete_date IS NULL";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            EmployeeID = reader["employee_id"].ToString(),
                            FullName = reader["full_name"].ToString(),
                            Gender = reader["gender"].ToString(),
                            ContactNumber = reader["contact_number"].ToString(),
                            Position = reader["position"].ToString(),
                            ImagePath = reader["image"].ToString(),
                            Salary = Convert.ToDecimal(reader["salary"]),
                            InsertDate = Convert.ToDateTime(reader["insert_date"]),
                            Status = reader["status"].ToString()
                        };
                    }
                }
                _connection.Close();
            }
            return employee;
        }

        public void AddEmployee(Employee employee)
        {
            string query = "INSERT INTO employees " +
                "(employee_id, full_name, gender, contact_number, position, image, salary, insert_date, status) " +
                           "VALUES (@id, @fullName, @gender, @contact, @position, @image, @salary, @date, @status)";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@id", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@fullName", employee.FullName);
                cmd.Parameters.AddWithValue("@gender", employee.Gender);
                cmd.Parameters.AddWithValue("@contact", employee.ContactNumber);
                cmd.Parameters.AddWithValue("@position", employee.Position);
                cmd.Parameters.AddWithValue("@image", employee.ImagePath);
                cmd.Parameters.AddWithValue("@salary", employee.Salary);
                cmd.Parameters.AddWithValue("@date", employee.InsertDate);
                cmd.Parameters.AddWithValue("@status", employee.Status);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            string query = "UPDATE employees SET full_name = @fullName, gender = @gender, contact_number = @contact, " +
                           "position = @position, update_date = @date, status = @status WHERE employee_id = @id";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@id", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@fullName", employee.FullName);
                cmd.Parameters.AddWithValue("@gender", employee.Gender);
                cmd.Parameters.AddWithValue("@contact", employee.ContactNumber);
                cmd.Parameters.AddWithValue("@position", employee.Position);
                cmd.Parameters.AddWithValue("@date", DateTime.Today);
                cmd.Parameters.AddWithValue("@status", employee.Status);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void DeleteEmployee(string id)
        {
            string query = "UPDATE employees SET delete_date = @date WHERE employee_id = @id";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date", DateTime.Today);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public int GetTotalEmployees()
        {
            return GetEmployeeCount("SELECT COUNT(id) FROM employees WHERE delete_date IS NULL");
        }

        public int GetActiveEmployees()
        {
            return GetEmployeeCount("SELECT COUNT(id) FROM employees WHERE status = 'Active' AND delete_date IS NULL");
        }

        public int GetInactiveEmployees()
        {
            return GetEmployeeCount("SELECT COUNT(id) FROM employees WHERE status = 'Inactive' AND delete_date IS NULL");
        }

        private int GetEmployeeCount(string query)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, _connection))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        public List<Employee> employeeListData()
        {
            List<Employee> listdata = new List<Employee>();

            if (_connection.State != ConnectionState.Open)
            {
                try
                {
                    _connection.Open();

                    string selectData = "SELECT * FROM employees WHERE delete_date IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, _connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Employee ed = new Employee();
                          /*  ed.id  = (int)reader["id"];*/
                            ed.EmployeeID = reader["employee_id"].ToString();
                            ed.FullName = reader["full_name"].ToString();
                            ed.Gender = reader["gender"].ToString();
                            ed.ContactNumber = reader["contact_number"].ToString();
                            ed.Position = reader["position"].ToString();
                            ed.ImagePath = reader["image"].ToString();
                            ed.Salary = (int)reader["salary"];
                            ed.Status = reader["status"].ToString();

                            listdata.Add(ed);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    _connection.Close();
                }
            }
            return listdata;
        }

        public List<Employee> salaryEmployeeListData()
        {
            List<Employee> listdata = new List<Employee>();

            if (_connection.State != ConnectionState.Open)
            {
                try
                {
                    _connection.Open();

                    string selectData = "SELECT * FROM employees WHERE delete_date IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, _connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Employee ed = new Employee();
                            ed.EmployeeID = reader["employee_id"].ToString();
                            ed.FullName = reader["full_name"].ToString();
                            ed.Position = reader["position"].ToString();
                            ed.Salary = (int)reader["salary"];

                            listdata.Add(ed);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    _connection.Close();
                }
            }
            return listdata;
        }

    }
}
