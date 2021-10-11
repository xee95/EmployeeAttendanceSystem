using EmployeeAttendencetSystem;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace EmployeeAttendanceSystem
{

    public partial class EmployeeAttendanceMainForum : Form
    {
        private bool _dragging;
        private Point _startPoint = new Point(0, 0);
        private int _row;

        public EmployeeAttendanceMainForum()
        {
            InitializeComponent();
            GetEmployees();
          //  openChildForm(new login());
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            //loginpanel.Controls.Add(childForm);
          //  loginpanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            const string message = "You are about to close application. Are you sure you want to continue?";
            const string caption = "Confirm close";
            var result = MessageBox.Show(message, caption,
                                     MessageBoxButtons.OKCancel,
                                     MessageBoxIcon.Question);
            if (result == DialogResult.OK)
                this.Close();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void addEmployee_Click(object sender, EventArgs e)
        {
            var addEmp = new AddEmployee();
            // addEmp.IdentityUpdated += this.SaveRecord;
            addEmp.ShowDialog();
        }
        private void payroll_Click(object sender, EventArgs e)
        {
            GeneratePayroll();
        }

        private void show_attendance_Click(object sender, EventArgs e)
        {
            showattendancelist();
        }

        public void showattendancelist()
        {
            var attendanceLists = new List<ByName>();

            try
            {
                for (var i = 0; i < dataGridView.Rows.Count - 1; i++)
                {
                   // var id = Convert.ToInt32(dataGridView.Rows[i].Cells[0].Value);
                    var name = Convert.ToString(dataGridView.Rows[i].Cells[1].Value);


                    attendanceLists.Add(new ByName(name));
                }
                var show_emp = new ShowAttendance(attendanceLists);
                show_emp.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        //This method will add all the required values to generate payroll in wageDetails list
        public void GeneratePayroll()
        {
            var attendanceList = new List<AttendanceList>();

            try
            {
                for (var i = 0; i < dataGridView.Rows.Count - 1; i++)
                {
                    var id = Convert.ToInt32(dataGridView.Rows[i].Cells[0].Value);
                    var name = Convert.ToString(dataGridView.Rows[i].Cells[1].Value);
                    var department = Convert.ToString(dataGridView.Rows[i].Cells[6].Value);

                    attendanceList.Add(new AttendanceList(id,name, department));
                }
                var attendanceform = new AttendenceForm(attendanceList);
                attendanceform.ShowDialog();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {

            
              var row = dataGridView.CurrentCell.RowIndex;
              var id = Convert.ToString(dataGridView.Rows[row].Cells[0].Value);
              var firstname = Convert.ToString(dataGridView.Rows[row].Cells[1].Value);
                var lastname = Convert.ToString(dataGridView.Rows[row].Cells[2].Value);
                var address = Convert.ToString(dataGridView.Rows[row].Cells[3].Value);
                var contact = Convert.ToString(dataGridView.Rows[row].Cells[4].Value);
                var email = Convert.ToString(dataGridView.Rows[row].Cells[5].Value);
                var desigination = Convert.ToString(dataGridView.Rows[row].Cells[6].Value);
                var department = Convert.ToString(dataGridView.Rows[row].Cells[7].Value);
                 var dateOfJoin = Convert.ToString(dataGridView.Rows[row].Cells[8].Value);
                //var dateOfJoin = Convert.ToDateTime(dataGridView.Rows[row].Cells[8].Value).ToString("dd/mm/yyyy");

                var addEmp = new EditEmployee();
            addEmp.LoadData(id, firstname,lastname, address, contact, email, desigination, department, dateOfJoin);
            addEmp.IdentityUpdated += this.UpdateRecord;
            addEmp.ShowDialog();
        }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void UpdateRecord(object sender, IdentityEventArgs e)
        {
            dataGridView.Rows[_row].Cells[0].Value = e.Id;
            dataGridView.Rows[_row].Cells[1].Value = e.FirstName;
            dataGridView.Rows[_row].Cells[2].Value = e.LastName;
            dataGridView.Rows[_row].Cells[3].Value = e.Address;
            dataGridView.Rows[_row].Cells[4].Value = e.Contact;
            dataGridView.Rows[_row].Cells[5].Value = e.Email;
            dataGridView.Rows[_row].Cells[6].Value = e.Designation
                ;
            dataGridView.Rows[_row].Cells[7].Value = e.Department;
            dataGridView.Rows[_row].Cells[8].Value = e.DateOfJoin;



        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            GetEmployees();
        }
        public void GetEmployees()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
            // string query = "INSERT INTO user(`id`, `first_name`) VALUES (NULL, '" + textBox1.Text + "', '" + textBox2.Text + "')";
            string query = "SELECT * FROM employees_info WHERE 1";
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
                dataGridView.Rows.Clear();
                dataGridView.Columns[8].DefaultCellStyle.Format = "dd/MM/yyyy";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // As our database, the array will contain : ID 0, FIRST_NAME 1,LAST_NAME 2, ADDRESS 3
                        // Do something with every received database ROW
                        Console.WriteLine(reader.GetString(0) + " - " + reader.GetString(1));
                        // string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                        var count = dataGridView.Rows.Count - 1;
                        dataGridView.Rows.Add();
                        dataGridView.Rows[count].Cells[0].Value = reader.GetString(0);
                        dataGridView.Rows[count].Cells[1].Value = reader.GetString(1);
                        dataGridView.Rows[count].Cells[2].Value = reader.GetString(2);
                        dataGridView.Rows[count].Cells[3].Value = reader.GetString(3);
                        dataGridView.Rows[count].Cells[4].Value = reader.GetString(4);
                        dataGridView.Rows[count].Cells[5].Value = reader.GetString(5);
                        dataGridView.Rows[count].Cells[6].Value = reader.GetString(6);
                        dataGridView.Rows[count].Cells[7].Value = reader.GetString(7);
                       object date = reader.GetValue(8);
                        dataGridView.Rows[count].Cells[8].Value = Convert.ToDateTime(date).ToString("dd/MM/yyyy");

                        count++;

                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }

                // Finally close the connection
                databaseConnection.Close();

            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }
    
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {


                var row = dataGridView.CurrentCell.RowIndex;
                var id = Convert.ToString(dataGridView.Rows[row].Cells[0].Value);
              //  int idint = Convert.ToString(dataGridView.Rows[row].Cells[0].Value)
                var firstname = Convert.ToString(dataGridView.Rows[row].Cells[1].Value);
                var lastname = Convert.ToString(dataGridView.Rows[row].Cells[2].Value);
                var address = Convert.ToString(dataGridView.Rows[row].Cells[3].Value);
                var contact = Convert.ToString(dataGridView.Rows[row].Cells[4].Value);
                var email = Convert.ToString(dataGridView.Rows[row].Cells[5].Value);
                var desigination = Convert.ToString(dataGridView.Rows[row].Cells[6].Value);
                var department = Convert.ToString(dataGridView.Rows[row].Cells[7].Value);
                var dateOfJoin = Convert.ToString(dataGridView.Rows[row].Cells[8].Value);
                //var dateOfJoin = Convert.ToDateTime(dataGridView.Rows[row].Cells[8].Value).ToString("dd/mm/yyyy");
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
                // string query = "INSERT INTO user(`id`, `first_name`) VALUES (NULL, '" + textBox1.Text + "', '" + textBox2.Text + "')";
                string query = "DELETE FROM employees_info WHERE `Id` =" + id+"";
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
                    databaseConnection.Close();
                    MessageBox.Show("deleted");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EmployeeAttendanceMainForum_Load(object sender, EventArgs e)
        {

        }
        private void sideBarPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void headerPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void form_load(object sender, EventArgs e)
        {
           
        }

        private void logoPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblLogo_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
