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
using MySql.Data.MySqlClient;
namespace EmployeeAttendanceSystem
{
    public partial class login : UserControl
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            {
                string username = txtuser.Text;
                string password = txtpass.Text;
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
                // string query = "INSERT INTO user(`id`, `first_name`) VALUES (NULL, '" + textBox1.Text + "', '" + textBox2.Text + "')";
                string query = "SELECT COUNT(*) FROM login_admin where username = '" + username + "' and password = '" + password + "'";
                // Which could be translated manually to :
                // INSERT INTO user(`id`, `first_name`, `last_name`, `address`) VALUES (NULL, 'Bruce', 'Wayne', 'Wayne Manor')
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);

                try
                {
                    // Open the database
                    databaseConnection.Open();
                    MySqlDataAdapter commandDatabase = new MySqlDataAdapter(query, databaseConnection);

                    DataTable dtbl = new DataTable();
                    commandDatabase.Fill(dtbl);
                    if (dtbl.Rows[0][0].ToString() == "1")
                    {
                       //  var Empdat = new EmployeeAttendanceMainForum();
                     //   Empdat.loginpanel.Hide();
                        this.Hide();
                        // Empdat.ShowDialog();
                        //var Log = new login();


                    }
                    else
                    {
                        MessageBox.Show("incorrect username or password", "alter", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }
                databaseConnection.Close();

            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void changed(object sender, EventArgs e)
        {
            if (txtuser.Text == "Username")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.White;
            }
        }

        private void leave(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                txtuser.Text = "Username";
                txtuser.ForeColor = Color.Gray;
            }
        }

        private void pass_leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "Password";
                txtpass.ForeColor = Color.Gray;
            }
        }

        private void pass_enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "Password")
            {
                txtpass.Text = "";
                txtpass.ForeColor = Color.White;
            }
        }
    }
}
