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
    partial class ShowAttendance : Form
    {

        private List<ByName> list;
        //public List<AttendanceList> list;
        public ShowAttendance(List<ByName> list)
        {
            InitializeComponent();
            this.list = list;
            // comboBox1.SelectedIndex = 0;
           // DataRow topItem = comboBox1.NewRow();
          //  topItem[0] = 0;
          //  topItem[1] = "-Select-";
          //  comboBox1.Rows.InsertAt(topItem, 0);
          comboBox1.Items.Insert(0, "-Select-");
            GetList();
            comboBox1.SelectedIndex = 0;
           // showattendanceGrid.FirstDisplayedScrollingRowIndex = showattendanceGrid.RowCount - 1;

        }


        public void GetList()
        {
            foreach (ByName emloyeeList in list)
            {


                comboBox1.Items.Add(emloyeeList.EmpName);
            }
        }
        public void GetEmployees(String Selected_date)
        {
            string current_date = Selected_date;
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
            // string query = "INSERT INTO user(`id`, `first_name`) VALUES (NULL, '" + textBox1.Text + "', '" + textBox2.Text + "')";
            string query = "SELECT emp.Id,emp.FirstName,emp.Department,att.attendance,rec.attendance_date FROM employees_info as emp" +
                " INNER JOIN record as rec ON emp.Id = rec.REC_EMP_ID " +
                "INNER JOIN attendance as att ON rec.attendance_type= att.attendance_id " +
                "WHERE rec.attendance_date= '" + current_date + "' ";
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
                showattendanceGrid.Rows.Clear();
                showattendanceGrid.Columns[4].DefaultCellStyle.Format = "yyyy-MM-dd";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // As our database, the array will contain : ID 0, FIRST_NAME 1,LAST_NAME 2, ADDRESS 3
                        // Do something with every received database ROW
                        Console.WriteLine(reader.GetString(0) + " - " + reader.GetString(1));
                        // string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                        var count = showattendanceGrid.Rows.Count - 1;
                        showattendanceGrid.Rows.Add();
                        showattendanceGrid.Rows[count].Cells[0].Value = reader.GetString(0);
                        showattendanceGrid.Rows[count].Cells[1].Value = reader.GetString(1);
                        showattendanceGrid.Rows[count].Cells[2].Value = reader.GetString(2);
                        showattendanceGrid.Rows[count].Cells[3].Value = reader.GetString(3);
                       
                        object date = reader.GetValue(4);
                        showattendanceGrid.Rows[count].Cells[4].Value = Convert.ToDateTime(date).ToString("yyyy-MM-dd");

                        count++;

                    }
                }
                
                else
                {
                    Console.WriteLine("No rows found.");
                }
               // showattendanceGrid.FirstDisplayedScrollingRowIndex = showattendanceGrid.RowCount - 1;
               // Console.WriteLine(showattendanceGrid.RowCount);
                // Finally close the connection
                databaseConnection.Close();
                getvalue();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            
                this.Close();
            
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            String current_date = monthCalendar1.SelectionStart.Date.ToString("yyyy-MM-dd");
            GetEmployees(current_date);
            //System.Console.WriteLine(monthCalendar1.SelectionRange.Start.ToShortDateString());
           
            //PopulateDataGridView();
        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            GetEmployees(monthCalendar1.SelectionStart.Date.ToString("yyyy-MM-dd"));
            //System.Console.WriteLine(monthCalendar1.SelectionRange.Start.ToShortDateString());

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        public void getvalueSearch()
        {
            int searched = comboBox1.SelectedIndex;

            // String daate = monthCalendar1.SelectionStart.Date.ToString("yyyy-MM-dd");
           System.Console.WriteLine(searched);
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
            string query = "SELECT Count(rec.attendance_type)FROM record as rec INNER JOIN attendance as att ON rec.attendance_type= att.attendance_id where rec.attendance_type =1 And `REC_EMP_ID` = '" + searched + "'";
            // string query = "SELECT `attendance_type`, COUNT(*) FROM record WHERE  `attendance_date`='" + daate + "' GROUP BY `attendance_type` HAVING COUNT(*) > 1";
            // Which could be translated manually to :
            // INSERT INTO user(`id`, `first_name`, `last_name`, `address`) VALUES (NULL, 'Bruce', 'Wayne', 'Wayne Manor')
            string query2 = "SELECT Count(rec.attendance_type)FROM record as rec INNER JOIN attendance as att ON rec.attendance_type= att.attendance_id where rec.attendance_type =2 And rec.REC_EMP_ID= '" + searched + "'";
            string query3 = "SELECT Count(rec.attendance_type)FROM record as rec INNER JOIN attendance as att ON rec.attendance_type= att.attendance_id where rec.attendance_type =3 And rec.REC_EMP_ID= '" + searched + "'";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection);
            MySqlCommand commandDatabase3 = new MySqlCommand(query3, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            commandDatabase2.CommandTimeout = 60;
            commandDatabase3.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                // Open the database
                databaseConnection.Open();
                Console.WriteLine("success");

                // reader = commandDatabase.ExecuteReader();
                int i = Convert.ToInt32(commandDatabase.ExecuteScalar());
                // List<string> str = new List<string>();
                // All succesfully executed, now do something
                presentlbl.Text = i.ToString();
                int i2 = Convert.ToInt32(commandDatabase2.ExecuteScalar());
                lblabsent.Text = i2.ToString();
                int i3 = Convert.ToInt32(commandDatabase3.ExecuteScalar());
                lblleave.Text = i3.ToString();

                // IMPORTANT : 
                // If your query returns result, use the following processor :


                // showattendanceGrid.FirstDisplayedScrollingRowIndex = showattendanceGrid.RowCount - 1;
                //  Console.WriteLine(showattendanceGrid.RowCount);

                // Finally close the connection
                databaseConnection.Close();
                // databaseConnection.Open();
                /*  if (str[0] != null) { 
                  presentlbl.Text = str[0];
                  }else { presentlbl.Text = ""; }
                  if (str[1] != null)
                  {
                      lblabsent.Text = str[1];
                  }
                  else { lblabsent.Text = ""; }
                  if (str[2] != null)
                  {
                      lblleave.Text = str[2];
                  }
                  else { lblleave.Text = ""; } */
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }
        private void search_Click(object sender, EventArgs e)
        {
            // string current_date = Selected_date;
            var searched = comboBox1.Text;
          //  System.Console.WriteLine(searched)
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
            // string query = "INSERT INTO user(`id`, `first_name`) VALUES (NULL, '" + textBox1.Text + "', '" + textBox2.Text + "')";
            string query = "SELECT emp.Id,emp.FirstName,emp.Department,att.attendance,rec.attendance_date FROM employees_info as emp" +
                " INNER JOIN record as rec ON emp.Id = rec.REC_EMP_ID " +
                "INNER JOIN attendance as att ON rec.attendance_type= att.attendance_id " +
                "WHERE emp.FirstName = '" + searched + "' ";
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
                showattendanceGrid.Rows.Clear();
                showattendanceGrid.Columns[4].DefaultCellStyle.Format = "yyyy-MM-dd";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // As our database, the array will contain : ID 0, FIRST_NAME 1,LAST_NAME 2, ADDRESS 3
                        // Do something with every received database ROW
                        Console.WriteLine(reader.GetString(0) + " - " + reader.GetString(1));
                        // string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                        var count = showattendanceGrid.Rows.Count - 1;
                        showattendanceGrid.Rows.Add();
                        showattendanceGrid.Rows[count].Cells[0].Value = reader.GetString(0);
                        showattendanceGrid.Rows[count].Cells[1].Value = reader.GetString(1);
                        showattendanceGrid.Rows[count].Cells[2].Value = reader.GetString(2);
                        showattendanceGrid.Rows[count].Cells[3].Value = reader.GetString(3);

                        object date = reader.GetValue(4);
                        showattendanceGrid.Rows[count].Cells[4].Value = Convert.ToDateTime(date).ToString("yyyy-MM-dd");

                        count++;

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
                getvalueSearch();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
            

        }

        public void getvalue()
        {
            //var searched = comboBox1.Text;

            String daate = monthCalendar1.SelectionStart.Date.ToString("yyyy-MM-dd");
            //  System.Console.WriteLine(searched)
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=employee_data;";
            string query = "SELECT Count(rec.attendance_type)FROM record as rec INNER JOIN attendance as att ON rec.attendance_type= att.attendance_id where rec.attendance_type =1 And rec.attendance_date= '" + daate + "'";
            // string query = "SELECT `attendance_type`, COUNT(*) FROM record WHERE  `attendance_date`='" + daate + "' GROUP BY `attendance_type` HAVING COUNT(*) > 1";
            // Which could be translated manually to :
            // INSERT INTO user(`id`, `first_name`, `last_name`, `address`) VALUES (NULL, 'Bruce', 'Wayne', 'Wayne Manor')
            string query2 = "SELECT Count(rec.attendance_type)FROM record as rec INNER JOIN attendance as att ON rec.attendance_type= att.attendance_id where rec.attendance_type =2 And rec.attendance_date= '" + daate + "'";
            string query3 = "SELECT Count(rec.attendance_type)FROM record as rec INNER JOIN attendance as att ON rec.attendance_type= att.attendance_id where rec.attendance_type =3 And rec.attendance_date= '" + daate + "'";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection);
            MySqlCommand commandDatabase3 = new MySqlCommand(query3, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            commandDatabase2.CommandTimeout = 60;
            commandDatabase3.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                // Open the database
                databaseConnection.Open();
                Console.WriteLine("success");

               // reader = commandDatabase.ExecuteReader();
                int i = Convert.ToInt32(commandDatabase.ExecuteScalar());
                // List<string> str = new List<string>();
                // All succesfully executed, now do something
                presentlbl.Text = i.ToString();
                int i2 = Convert.ToInt32(commandDatabase2.ExecuteScalar());
                lblabsent.Text = i2.ToString();
                int i3 = Convert.ToInt32(commandDatabase3.ExecuteScalar());
                lblleave.Text = i3.ToString();

                // IMPORTANT : 
                // If your query returns result, use the following processor :


                // showattendanceGrid.FirstDisplayedScrollingRowIndex = showattendanceGrid.RowCount - 1;
                //  Console.WriteLine(showattendanceGrid.RowCount);

                // Finally close the connection
                databaseConnection.Close();
               // databaseConnection.Open();
                /*  if (str[0] != null) { 
                  presentlbl.Text = str[0];
                  }else { presentlbl.Text = ""; }
                  if (str[1] != null)
                  {
                      lblabsent.Text = str[1];
                  }
                  else { lblabsent.Text = ""; }
                  if (str[2] != null)
                  {
                      lblleave.Text = str[2];
                  }
                  else { lblleave.Text = ""; } */
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNameAsc_Click(object sender, EventArgs e)
        {
            SortByNameAssending();
        }
        private void SortByNameAssending()
        {
            int row = showattendanceGrid.Rows.Count - 1;
            for (int i = 0; i < row; i++)
            {
                for (int j = i + 1; j < row; j++)
                {
                    if (string.Compare(showattendanceGrid.Rows[i].Cells[1].Value.ToString(), showattendanceGrid.Rows[j].Cells[1].Value.ToString()) > 0)
                    {
                        var tempid = showattendanceGrid.Rows[i].Cells[0].Value;
                        var tempName = showattendanceGrid.Rows[i].Cells[1].Value;
                        var tempDepartment = showattendanceGrid.Rows[i].Cells[2].Value;
                        var tempAttendace = showattendanceGrid.Rows[i].Cells[3].Value;


                        showattendanceGrid.Rows[i].Cells[0].Value = showattendanceGrid.Rows[j].Cells[0].Value;
                        showattendanceGrid.Rows[i].Cells[1].Value = showattendanceGrid.Rows[j].Cells[1].Value;
                        showattendanceGrid.Rows[i].Cells[2].Value = showattendanceGrid.Rows[j].Cells[2].Value;
                        showattendanceGrid.Rows[i].Cells[3].Value = showattendanceGrid.Rows[j].Cells[3].Value;


                        showattendanceGrid.Rows[j].Cells[0].Value = tempid;
                        showattendanceGrid.Rows[j].Cells[1].Value = tempName;
                        showattendanceGrid.Rows[j].Cells[2].Value = tempDepartment;
                        showattendanceGrid.Rows[j].Cells[3].Value = tempAttendace;

                    }
                }
            }
        }
        private void SortByNameDescending()
        {
            int row = showattendanceGrid.Rows.Count - 1;
            for (int i = 0; i < row; i++)
            {
                for (int j = i + 1; j < row; j++)
                {
                    if (string.Compare(showattendanceGrid.Rows[i].Cells[1].Value.ToString(), showattendanceGrid.Rows[j].Cells[1].Value.ToString()) < 0)
                    {
                        var tempid = showattendanceGrid.Rows[i].Cells[0].Value;
                        var tempName = showattendanceGrid.Rows[i].Cells[1].Value;
                        var tempDepartment = showattendanceGrid.Rows[i].Cells[2].Value;
                        var tempAttendace = showattendanceGrid.Rows[i].Cells[3].Value;


                        showattendanceGrid.Rows[i].Cells[0].Value = showattendanceGrid.Rows[j].Cells[0].Value;
                        showattendanceGrid.Rows[i].Cells[1].Value = showattendanceGrid.Rows[j].Cells[1].Value;
                        showattendanceGrid.Rows[i].Cells[2].Value = showattendanceGrid.Rows[j].Cells[2].Value;
                        showattendanceGrid.Rows[i].Cells[3].Value = showattendanceGrid.Rows[j].Cells[3].Value;


                        showattendanceGrid.Rows[j].Cells[0].Value = tempid;
                        showattendanceGrid.Rows[j].Cells[1].Value = tempName;
                        showattendanceGrid.Rows[j].Cells[2].Value = tempDepartment;
                        showattendanceGrid.Rows[j].Cells[3].Value = tempAttendace;
                       

                    }
                }
            }
        }

        private void btnNameDsc_Click(object sender, EventArgs e)
        {
            SortByNameDescending();
        }
    }
}
