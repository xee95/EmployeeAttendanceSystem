using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAttendanceSystem
{
    class AttendanceList
    {

        public int empid;
            public string EmpName;
            public string EmpDepartment;
           

        /******
         * * 
         * * Method will Calculate the total wage of employee.
         * * 
         * *******/

        public  AttendanceList(int id,string name, string department)
            {
                this.empid = id;
                this.EmpName = name;
                this.EmpDepartment = department;
              
            }
        }
    }

