using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RuppSystem.model;

namespace RuppSystem
{
    public partial class Dashboard : UserControl
    {
        private readonly EmployeeRepository _employeeRepository;
       

        private Dictionary<string, decimal> positionSalaryMap = new Dictionary<string, decimal>()
        {
            { "Business Management", 10000 },
            { "Front-End Developer", 7000 },
            { "Back-End Developer", 8000 },
            { "Data Administrator", 12000 },
            { "UI/UX Design", 3000 },
            { "Tester", 2500 }
        };

        public Dashboard()
        {
            InitializeComponent();
            _employeeRepository = new EmployeeRepository();
            displayTE();
            displayAE();
            displayIE();
            ConfigureDataGridView();
            LoadEmployeeData();
        }

        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;
            }

            displayTE();
            displayAE();
            displayIE();
            LoadEmployeeData();
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
        
            dataGridView1.MultiSelect = false; 
        }
        public void displayTE()
        {
            dashboard_TE.Text = _employeeRepository.GetTotalEmployees().ToString();
        }

        public void displayAE()
        {
            dashboard_AE.Text = _employeeRepository.GetActiveEmployees().ToString();
        }

        public void displayIE()
        {
            dashboard_IE.Text = _employeeRepository.GetInactiveEmployees().ToString();
        }

        private void dashboard_TE_Click(object sender, EventArgs e)
        {

        }

        private void LoadEmployeeData()
        {
          
            try
            {
                dataGridView1.DataSource = _employeeRepository.GetAllEmployees();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employee data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
