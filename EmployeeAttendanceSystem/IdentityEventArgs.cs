using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAttendanceSystem
{
    public class IdentityEventArgs : EventArgs
    {
        public IdentityEventArgs(string id, string firstName, string lastName,string address, string contact, string email,
            string designation, string department, string dateOfJoin)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.Address = address;
            this.Contact = contact;
            this.Email = email;
            this.Designation = designation;
            this.Department = department;
            this.DateOfJoin = dateOfJoin;
            this.LastName = lastName;
            
        }

        public string Id { get; }
        public string FirstName { get; }
        public string Address { get; }
        public string Contact { get; }
        public string Email { get; }
        public string Designation { get; }
        public string Department { get; }
        public string DateOfJoin { get; }
        public string LastName { get; }
      
    }
}

