using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using RuppSystem;

namespace EmployeeManagementSystem
{
    class SalaryDatas
    {

       

        public string EmployeeID { set; get; } // 0
        public string Name { set; get; } // 1
        public string Gender { set; get; } // 2
        public string Contact { set; get; } // 3
        public string Position { set; get; } // 4
        public int Salary { set; get; } // 5

        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-6PF13PHQ\SQLEXPRESS;Initial Catalog=rupp;Integrated Security=True");

/*
        SqlConnection connect = DatabaseConnection.Instance.GetConnection();*/

        public List<SalaryDatas> salaryEmployeeListData()
        {
            List<SalaryDatas> listdata = new List<SalaryDatas>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM employees WHERE status = 'Active' " +
                        "AND delete_date IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SalaryDatas sd = new SalaryDatas();
                            sd.EmployeeID = reader["employee_id"].ToString();
                            sd.Name = reader["full_name"].ToString();
                            sd.Gender = reader["gender"].ToString();
                            sd.Contact = reader["contact_number"].ToString();
                            sd.Position = reader["position"].ToString();
                            sd.Salary = (int)reader["salary"];

                            listdata.Add(sd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listdata;
        }

    }
}
