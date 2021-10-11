using EmployeeAttendanceSystem;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace EmployeeAttendencetSystem
{
    /***********
     * *
     *  This class will help to calculate the and display payroll of employee
     * *
     **********/
    partial class AttendenceForm : Form
    {
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);


        private List<AttendanceList> list;

        public AttendenceForm(List<AttendanceList> list)
        {
            InitializeComponent();
            this.list = list;
            AddEmployeeToDataGridView();
          //  payrollDataGridView.FirstDisplayedScrollingRowIndex = payrollDataGridView.RowCount - 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulatePositionComboBox();
            //PopulateDataGridView();
        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            
            PopulatePositionComboBox();
          //  AddEmployeeToDataGridView();
            // cbxPosition.Index[0];

        }
        void PopulatePositionComboBox()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
            string query = "SELECT * FROM attendance WHERE 1";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            //commandDatabase.CommandTimeout = 60;
            //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM attendance", sqlCon);
            try
            {
                // Open the database
                databaseConnection.Open();
                MySqlDataAdapter commandDatabase = new MySqlDataAdapter(query, databaseConnection);

                DataTable dtbl = new DataTable();
                commandDatabase.Fill(dtbl);
                cbxPosition.ValueMember = "attendance_id";
                cbxPosition.DisplayMember = "attendance";
                DataRow topItem = dtbl.NewRow();
                topItem[0] = 0;
                topItem[1] = "-Select-";
                dtbl.Rows.InsertAt(topItem, 0);
                cbxPosition.DataSource = dtbl;
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
            databaseConnection.Close();
        }
        private void dgvEmployee_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            var current_date = monthCalendar1.SelectionStart.Date.ToString("yyyy-MM-dd");
           // var dateOfJoin = dateTimePicker.Value.ToString("yyyy-MM-dd");

            if (payrollDataGridView.CurrentRow != null)
            {
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
               // string query = "SELECT * FROM attendance WHERE 1";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);

                // using (SqlConnection sqlCon = new SqlConnection(connectionString))
                try
                {
                    // Open the database
                    databaseConnection.Open();
                    // MySqlDataAdapter commandDatabase = new MySqlDataAdapter(query, databaseConnection);

                    DataGridViewRow dgvRow = payrollDataGridView.CurrentRow;
                    MySqlCommand sqlCmd = new MySqlCommand("country_hos", databaseConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    // if (dgvRow.Cells["txtEmployeeID"].Value == DBNull.Value)//Insert
                    // sqlCmd.Parameters.AddWithValue("@EmployeeID", 0);
                    string id = dgvRow.Cells["id"].Value.ToString();
                    // Console.WriteLine(id);
                    MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM record WHERE `REC_EMP_ID`='"+id+"' AND " +
                        "`attendance_date`= '"+current_date+ "';", databaseConnection);
                    int count = Convert.ToInt32(cmd1.ExecuteScalar());
                    Console.WriteLine(count);
                    //else//update
                    if (count < 1)
                    {
                        // sqlCmd.Parameters.AddWithValue("@REC_ID", 1);
                        MySqlCommand cmd = new MySqlCommand("SELECT COUNT(REC_ID) FROM record", databaseConnection);
                        int i = Convert.ToInt32(cmd.ExecuteScalar());
                        i++;
                        sqlCmd.Parameters.AddWithValue("@rec_id", i);

                        // sqlCmd.Parameters.AddWithValue("@Name", dgvRow.Cells["name"].Value == DBNull.Value ? "" : dgvRow.Cells["name"].Value.ToString());
                        sqlCmd.Parameters.AddWithValue("@ATTENDANCE", Convert.ToInt32(dgvRow.Cells["cbxPosition"].Value == DBNull.Value ? "0" : dgvRow.Cells["cbxPosition"].Value.ToString()));
                        //sqlCmd.Parameters.AddWithValue("@empid", 1);
                        sqlCmd.Parameters.AddWithValue("@empid", dgvRow.Cells["id"].Value == DBNull.Value ? "" : dgvRow.Cells["id"].Value.ToString());
                        sqlCmd.Parameters.AddWithValue("@attendance_date", current_date);
                        sqlCmd.ExecuteNonQuery();
                        // PopulateDataGridView();
                    }
                    else
                    {
                        MessageBox.Show( "Duplicate Entry");
                    }
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Method to add calculated vlues from list to dataGridView
        /// </summary>
        public void AddEmployeeToDataGridView()
        {
             foreach (AttendanceList emloyeeList in list)
            {
                int row = payrollDataGridView.Rows.Count - 1;
                payrollDataGridView.Rows.Add();
                payrollDataGridView.Rows[row].Cells[0].Value = (emloyeeList.empid);
                payrollDataGridView.Rows[row].Cells[1].Value = (emloyeeList.EmpName);
                payrollDataGridView.Rows[row].Cells[2].Value = (emloyeeList.EmpDepartment);
                
            }
            
        }

        /// <summary>
        /// Method to sort rows based on TotalWage
        /// </summary>
      


      

        /// <summary>
        /// Method to sort rows based on Name
        /// </summary>
      

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /*
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
        */
       

        private void payrollDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblMainMenu_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void header_Click(object sender, EventArgs e)
        {

        }

       
    }
}
