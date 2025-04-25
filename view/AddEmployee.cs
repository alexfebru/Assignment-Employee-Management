using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployeeManagementSystem;
using RuppSystem.model;
using RuppSystem.repositary;

namespace RuppSystem
{
    public partial class AddEmployee : UserControl
    {
        private readonly IEmployeeRepository _employeeRepository;

        private Dictionary<string, decimal> positionSalaryMap = new Dictionary<string, decimal>()
        {
            { "Business Management", 10000 },
            { "Front-End Developer", 7000 },
            { "Back-End Developer", 8000 },
            { "Data Administrator", 12000 },
            { "UI/UX Design", 3000 },
            { "Tester", 2500 },
        };
        public AddEmployee()
        {
            InitializeComponent();
            _employeeRepository = new EmployeeRepository();
            displayEmployeeData();

            addEmployee_position.DataSource = new List<string>(positionSalaryMap.Keys);
            ConfigureDataGridView();
        }
        private void addEmployee_position_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (addEmployee_position.SelectedItem != null)
            {
                string selectedPosition = addEmployee_position.SelectedItem.ToString();
                if (positionSalaryMap.ContainsKey(selectedPosition))
                {
                    addEmployee_position.Text = positionSalaryMap[selectedPosition].ToString(); 
                }
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.MultiSelect = false;
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

        public void clearFields()
        {
            addEmployee_id.Text = "";
            addEmployee_fullName.Text = "";
            addEmployee_gender.SelectedIndex = -1;
            addEmployee_phoneNum.Text = "";
            addEmployee_position.SelectedIndex = -1;
            addEmployee_status.SelectedIndex = -1;
            addEmployee_picture.Image = null;
        }
        public void displayEmployeeData()
        {
            dataGridView1.DataSource = _employeeRepository.GetAllEmployees();
        }

        private void addEmployee_addBtn_Click(object sender, EventArgs e)
        {
            if (addEmployee_position.SelectedItem == null)
            {
                MessageBox.Show("Please select a position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedPosition = addEmployee_position.SelectedItem.ToString();
            decimal salary = positionSalaryMap.ContainsKey(selectedPosition) ? positionSalaryMap[selectedPosition] : 0;

            Employee newEmployee = Employee.Create(
                addEmployee_id.Text.Trim(),
                addEmployee_fullName.Text.Trim(),
                addEmployee_gender.Text.Trim(),
                addEmployee_phoneNum.Text.Trim(),
                addEmployee_position.Text.Trim(),
                addEmployee_picture.ImageLocation,
                salary,
                addEmployee_status.Text.Trim()
            );

            _employeeRepository.AddEmployee(newEmployee);
            displayEmployeeData();
            MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clearFields();
        }

        private void addEmployee_updateBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addEmployee_id.Text) ||
                string.IsNullOrWhiteSpace(addEmployee_fullName.Text) ||
                addEmployee_gender.SelectedItem == null ||
                string.IsNullOrWhiteSpace(addEmployee_phoneNum.Text) ||
                addEmployee_position.SelectedItem == null ||
                addEmployee_status.SelectedItem == null ||
                addEmployee_picture.Image == null)
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirmUpdate = MessageBox.Show($"Are you sure you want to UPDATE Employee ID: {addEmployee_id.Text.Trim()}?",
                                                         "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmUpdate == DialogResult.Yes)
            {
                try
                {
                    if (addEmployee_position.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string selectedPosition = addEmployee_position.SelectedItem.ToString();
                    decimal salary = positionSalaryMap.ContainsKey(selectedPosition) ? positionSalaryMap[selectedPosition] : 0;

                    Employee updatedEmployee = Employee.Create(
                        addEmployee_id.Text.Trim(),
                        addEmployee_fullName.Text.Trim(),
                        addEmployee_gender.Text.Trim(),
                        addEmployee_phoneNum.Text.Trim(),
                        selectedPosition,
                        addEmployee_picture.ImageLocation,
                        salary,
                        addEmployee_status.Text.Trim()
                    );

                    _employeeRepository.UpdateEmployee(updatedEmployee);
                    displayEmployeeData();

                    MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void addEmployee_deleteBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addEmployee_id.Text))
            {
                MessageBox.Show("Please select an employee to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult check = MessageBox.Show($"Are you sure you want to DELETE Employee ID: {addEmployee_id.Text.Trim()}?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (check == DialogResult.Yes)
            {
                try
                {
                    string employeeId = addEmployee_id.Text.Trim();

                    Employee employeeToDelete = _employeeRepository.GetEmployeeById(employeeId);
                    if (employeeToDelete == null)
                    {
                        MessageBox.Show("Employee not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    
                    _employeeRepository.DeleteEmployee(employeeId);

                
                    if (string.IsNullOrEmpty(employeeToDelete.ImagePath) && File.Exists(employeeToDelete.ImagePath))
                    {
                        File.Delete(employeeToDelete.ImagePath);
                    }

                   
                    displayEmployeeData(); 
                    MessageBox.Show("Employee deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void addEmployee_clearBtn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to Clear " + "?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(check == DialogResult.Yes)
            {
                clearFields();
            }
            
        }

        private void addEmployee_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                string imagePath = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    addEmployee_picture.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue; 
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; 
            try
            {
                

                if (e.RowIndex >= 0) 
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    addEmployee_id.Text = row.Cells[0].Value?.ToString() ?? "";
                    addEmployee_fullName.Text = row.Cells[1].Value?.ToString() ?? "";
                    addEmployee_gender.SelectedItem = row.Cells[2].Value?.ToString();
                    addEmployee_phoneNum.Text = row.Cells[3].Value?.ToString() ?? "";


                    string position = row.Cells[4].Value?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(position) && addEmployee_position.Items.Contains(position))
                    {
                        addEmployee_position.SelectedItem = position;
                    }
                    else
                    {
                        addEmployee_position.SelectedIndex = -1;
                    }
                    addEmployee_status.SelectedItem = row.Cells[8].Value?.ToString();

                    string imagePath = row.Cells[5].Value?.ToString();
                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        addEmployee_picture.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        addEmployee_picture.Image = null;
                    }

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employee data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            /*string searchText = txtSearch.Text.Trim();
            if (searchText != "")
            {
                EmployeeData filterData = new EmployeeData();

                List<EmployeeData> filteredData = filterData
                    .employeeListData()
                    .Where(x => x.EmployeeID.ToLower().Contains(searchText.ToLower()) || x.Name.ToLower().Contains(searchText.ToLower())
                ).ToList();

                dataGridView1.DataSource = filteredData;
            }
            else
            {
                displayEmployeeData();
            }*/

            string searchText = txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                
                List<Employee> allEmployees = _employeeRepository.employeeListData();

                IEmployeeSearchStrategy strategy = new SearchByPositionStrategy();
                /*EmployeeSearchContext context = new EmployeeSearchContext(strategy);*/
               /* var context = new EmployeeSearchContext(new SearchByIdOrNameStrategy());*/
                EmployeeSearchContext context = new EmployeeSearchContext(new SearchByIdOrNameStrategy());
                EmployeeSearchContext contexts = new EmployeeSearchContext(new SearchByPositionStrategy());
                dataGridView1.DataSource = context.Search(allEmployees, searchText);
                dataGridView1.DataSource = contexts.Search(allEmployees, searchText);
            }
            else
            {
                displayEmployeeData();
            }

        }
    }
}
