using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeAttendanceSystem
{
    public partial class EditEmployee : Form
    {

        private bool _dragging;
        private Point _startPoint = new Point(0, 0);

        //Delegate has been added
        public delegate void IdentityHandler(object sender, IdentityEventArgs e);


        //Event of the delegate type has been added. i.e. Object of delegate created
        public event IdentityHandler IdentityUpdated;
        public EditEmployee()
        {
            InitializeComponent();
            comboBoxDepartment.Items.Add("Administrative");
            comboBoxDepartment.Items.Add("Finance");
            comboBoxDepartment.Items.Add("Customer service");
            comboBoxDepartment.Items.Add("Marketing");
            comboBoxDepartment.Items.Add("IT");
            comboBoxDepartment.SelectedIndex = 0;
        }

        public void LoadData(string id, string name, string lastname, string address, string contact, string email, string desigination,
        string department, string dateOfJoin)
        {
            txtIdNo.Text = id;
            txtFirstName.Text = name;
            txtLastName.Text = lastname;
            txtAddress.Text = address;
            txtContact.Text = contact;
            txtEmail.Text = email;
            txtDesignation.Text = desigination;
            comboBoxDepartment.Text = department;
            dateTimePicker.Text = dateOfJoin;


        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = new Point(e.X, e.Y);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            var p = PointToScreen(e.Location);
            Location = new Point(p.X - this._startPoint.X, p.Y - this._startPoint.Y);
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var id = txtIdNo.Text;
            var name = txtFirstName.Text;
            var address = txtAddress.Text;
            var contactNo = txtContact.Text;
            var email = txtEmail.Text;
            var desigination = txtDesignation.Text;
            var department = comboBoxDepartment.Text;
            var dateOfJoin = dateTimePicker.Value.ToString("yyyy-MM-dd");
            var lastname = txtLastName.Text;
            

            {
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
                // string query = "INSERT INTO user(`id`, `first_name`) VALUES (NULL, '" + textBox1.Text + "', '" + textBox2.Text + "')";
                string query = "Update employees_info Set `FirstName`='" + name + "',`LastName`='" + lastname + "'," +
                    "`Address`='" + address + "',`Contact`='" + contactNo + "',`Email`='" + email + "',`Designation`='" + desigination + "',`Department`='" + department + "'," +
                    "`JoinDate`='" + dateOfJoin + "' Where `Id` = " + id+" ";
                // Update student set studentID=2, studentName = "Khadak Sharma", studentAddress = "Parel" Where studentID = 1;
                // `FirstName`, `LastName`, `Address`, `Contact`, `Email`, `Designation`, `Department`, `JoinDate`
                //Which could be translated manually to :
                // INSERT INTO user(`id`, `first_name`, `last_name`, `address`) VALUES (NULL, 'Bruce', 'Wayne', 'Wayne Manor')
                System.Console.WriteLine(dateTimePicker.Value.ToString("yyyy-MM-dd"));
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                //commandDatabase.Parameters.Add("@dateParamter", MySqlDbType.Date);
               // commandDatabase.Parameters["@dateParamter"].Value = dateTimePicker.Value;
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;
                try
                {
                    // Open the database
                    databaseConnection.Open();
                    Console.WriteLine("success");

                    reader = commandDatabase.ExecuteReader();

                    // All succesfully executed, now do something

                    // IMPORTANT : 
                    // If your query returns result, use the following processor :

                    //MySqlDataReader myReader = commandDatabase.ExecuteReader();

                    MessageBox.Show("User succesfully registered");

                    // Finally close the connection
                    databaseConnection.Close();
                    var args = new IdentityEventArgs(id, name,lastname, address, contactNo, email, desigination, department, dateOfJoin);

                    //Event has be raised with update arguments of delegate
                    IdentityUpdated?.Invoke(this, args);

                    this.Hide();
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            // Validation(txtFullName, "Full name");
        }

        private void sql()
        {

            {
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
                // string query = "INSERT INTO user(`id`, `first_name`) VALUES (NULL, '" + textBox1.Text + "', '" + textBox2.Text + "')";
                string query = "INSERT INTO persons(`id`, `name`) VALUES (id + ', ' + name')";

                // Which could be translated manually to :
                // INSERT INTO user(`id`, `first_name`, `last_name`, `address`) VALUES (NULL, 'Bruce', 'Wayne', 'Wayne Manor')

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;
                try
                {
                    // Open the database
                    databaseConnection.Open();
                    Console.WriteLine("success");

                    reader = commandDatabase.ExecuteReader();

                    // All succesfully executed, now do something

                    // IMPORTANT : 
                    // If your query returns result, use the following processor :

                    //MySqlDataReader myReader = commandDatabase.ExecuteReader();

                    MessageBox.Show("User succesfully registered");

                    // Finally close the connection
                    databaseConnection.Close();

                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }
            }
        }

        
    }
}
