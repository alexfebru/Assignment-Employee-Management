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

namespace RuppSystem
{
    public partial class Form1 : Form
    {
        /*   SqlConnection connect
               = new SqlConnection(@"Data Source=LAPTOP-6PF13PHQ\SQLEXPRESS;Initial Catalog=rupp;Integrated Security=True");*/

        SqlConnection connect = DatabaseConnection.Instance.GetConnection();
        /*   private readonly SqlConnection _connection;
           SqlConnection connect = DatabaseConnection.Instance.GetConnection();*/

        public Form1()
        {
            InitializeComponent();
            
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (login_username.Text == ""
                || login_password.Text == "")
            {
                MessageBox.Show("Please fill all blank fields"
                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();

                        string selectData = "SELECT * FROM users WHERE username = @username " +
                        "AND password = @password";

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@username", login_username.Text.Trim());
                            cmd.Parameters.AddWithValue("@password", login_password.Text.Trim());

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show("Login successfully!"
                                    , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                MainForm mForm = new MainForm();
                                mForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username/Password"
                                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex
                        , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }

            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_signupBtn_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();
            regForm.Show();
            this.Hide();
        }


        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = login_showPass.Checked ? '\0' : '*';
        }

        private void login_btn_MouseHover(object sender, EventArgs e)
        {
            login_btn.BackColor = Color.DarkBlue;
        }

        private void login_btn_MouseLeave(object sender, EventArgs e)
        {
            login_btn.BackColor = Color.RoyalBlue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void login_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void login_signupBtn_MouseHover(object sender, EventArgs e)
        {
            login_signupBtn.BackColor = Color.DarkBlue;
        }

        private void login_signupBtn_MouseLeave(object sender, EventArgs e)
        {
            login_signupBtn.BackColor = Color.RoyalBlue;
        }
    }
}
