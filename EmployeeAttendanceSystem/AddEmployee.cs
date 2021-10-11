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
    public partial class AddEmployee : Form
    {

        private bool _dragging;
        private Point _startPoint = new Point(0, 0);

        //Delegate has been added
        public delegate void IdentityHandler(object sender, IdentityEventArgs e);


        //Event of the delegate type has been added. i.e. Object of delegate created
        public event IdentityHandler IdentityUpdated;
        public AddEmployee()
        {
            InitializeComponent();
            comboBoxDepartment.Items.Add("Administrative");
            comboBoxDepartment.Items.Add("Finance");
            comboBoxDepartment.Items.Add("Customer service");
            comboBoxDepartment.Items.Add("Marketing");
            comboBoxDepartment.Items.Add("IT");
            comboBoxDepartment.SelectedIndex = 0;
            //sql();
        }

        public void LoadData(string id, string name, string lastname,string address, string contact, string email, string desigination,
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
            var firstname = txtFirstName.Text;
            var address = txtAddress.Text;
            var contactNo = txtContact.Text;
            var email = txtEmail.Text;
            var desigination = txtDesignation.Text;
            var department = comboBoxDepartment.Text;
            var dateOfJoin = dateTimePicker.Value.ToString("yyyy-MM-dd"); ;
            var lastname = txtLastName.Text;

            //'" + id+"' ,
            {
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
                // string query = "INSERT INTO user(`id`, `first_name`) VALUES (NULL, '" + textBox1.Text + "', '" + textBox2.Text + "')";
                string query = "INSERT INTO employees_info ( `id`,`FirstName`, `LastName`, `Address`, `Contact`, `Email`, `Designation`, `Department`, `JoinDate`)" +
                    " VALUES ( '" + id + "' ,'" + firstname+ "','" + lastname + "','" + address + "','" + contactNo + "','" + email + "','" + desigination + "'" +
                    ",'" + department + "','" + dateOfJoin + "')";

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
                   // var args = new IdentityEventArgs(id, name, address, contactNo, email, desigination, department, dateOfJoin, wageRate, hourWorked);

                    //Event has be raised with update arguments of delegate
                   // IdentityUpdated?.Invoke(this, args);

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
               // string query = "INSERT INTO table1(`id`, `name`) VALUES (id + ', ' + name')";

                // Which could be translated manually to :
                // INSERT INTO user(`id`, `first_name`, `last_name`, `address`) VALUES (NULL, 'Bruce', 'Wayne', 'Wayne Manor')

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
              
                //MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                //commandDatabase.CommandTimeout = 60;
               // MySqlDataReader reader;
                try
                {
                    // Open the database  ORDER BY id DESC LIMIT 1
                    databaseConnection.Open();
                    // MySqlCommand cmd = new MySqlCommand("SELECT COUNT(Id) FROM employees_info", databaseConnection);
                    MySqlCommand cmd = new MySqlCommand("SELECT Id FROM employees_info ORDER BY id DESC LIMIT 1", databaseConnection);
                    //String id
                    //int i = Convert.ToInt32(cmd.ExecuteScalar());
                   int i; //= Convert.ToString(cmd.ExecuteScalar());
                   // Console.WriteLine("success");
                    cmd.CommandTimeout = 60;
                    MySqlDataReader reader;
                   
                        // Open the database
                       // databaseConnection.Open();
                        Console.WriteLine("success");

                        reader = cmd.ExecuteReader();

                        
                      
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                               
                                Console.WriteLine(reader.GetValue(0) );
                           // Console.WriteLine(reader.GetString(1));
                            // // string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                              i = Convert.ToInt32(reader.GetString(0));
                            Console.WriteLine(i);
                            i++;
                             txtIdNo.Text = i.ToString();
                        }

                    }

                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                        // showattendanceGrid.FirstDisplayedScrollingRowIndex = showattendanceGrid.RowCount - 1;
                        //  Console.WriteLine(showattendanceGrid.RowCount);

                        // Finally close the connection
                        databaseConnection.Close();

                    
                    
                  
                  //  databaseConnection.Close();
                    //i++;
                  //  txtIdNo.Text = i;
                    
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            sql();
        }

        private void lblFullName_Click(object sender, EventArgs e)
        {

        }
    }
}
