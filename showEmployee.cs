using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployeeManagementSystem;

namespace RuppSystem
{
    public partial class showEmployee : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-6PF13PHQ\SQLEXPRESS;Initial Catalog=rupp;Integrated Security=True");

        public showEmployee()
        {
            InitializeComponent();
        }

        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;
            }
            displayEmployeeData();
        }

        public void displayEmployeeData()
        {
            EmployeeData ed = new EmployeeData();
            List<EmployeeData> listData = ed.employeeListData();

            dataGridView1.DataSource = listData;
        }

        private void showEmployee_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AddEmployee showdata = new AddEmployee();

            showdata.displayEmployeeData();
        }
    }
}
