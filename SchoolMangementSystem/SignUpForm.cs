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

namespace SchoolMangementSystem
{
    public partial class SignUpForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\#Whatsapp_Work\School-Management-System-using-CSharp-main\SchoolMangementSystem\SchoolMangementSystem\SMS.mdf;Integrated Security=True;Connect Timeout=30");

        public SignUpForm()
        {
            InitializeComponent();
        }

        private void EXIT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showPass_CheckedChanged(object sender, EventArgs e)
        {
            password.PasswordChar = showPass.Checked ? '\0' : '*';
        }

        private void SignUpbtn_Click(object sender, EventArgs e)
        {
            if (username.Text == "" || password.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    connect.Open();

                    string checkUser = "SELECT COUNT(*) FROM users WHERE username = @username";

                    using (SqlCommand checkCmd = new SqlCommand(checkUser, connect))
                    {
                        checkCmd.Parameters.AddWithValue("@username", username.Text.Trim());
                        int userCount = (int)checkCmd.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Username already exists.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        else
                        {
                            // 2. Insert the new user
                            string insertData = "INSERT INTO users (username, password) VALUES (@username, @password)";
                            using (SqlCommand cmd = new SqlCommand(insertData, connect))
                            {
                                cmd.Parameters.AddWithValue("@username", username.Text.Trim());
                                cmd.Parameters.AddWithValue("@password", password.Text.Trim()); // Needs secure handling!

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Signup successful!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // You might want to redirect to the login form here
                                LoginForm loginForm = new LoginForm();
                                loginForm.Show();
                                this.Hide();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    connect.Close();
                }
            }
        }
    }
}
