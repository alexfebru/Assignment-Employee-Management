using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem;
using RuppSystem.model;

namespace RuppSystem.repositary
{
    public class SalaryRepository : ISalaryUpdateStrategy
    {

        public bool UpdateSalary(string employeeID, int salary)
        {
            bool success = false;
            using (
                     SqlConnection connect = DatabaseConnection.Instance.GetConnection()
                )
            {
                try
                {
                    connect.Open();
                    DateTime today = DateTime.Today;

                    string query = "UPDATE employees SET salary = @salary, update_date = @updateDate WHERE employee_id = @employeeID";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@salary", salary);
                        cmd.Parameters.AddWithValue("@updateDate", today);
                        cmd.Parameters.AddWithValue("@employeeID", employeeID);

                        success = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating salary: " + ex.Message);
                }
            }
            return success;
        }

    }
}
