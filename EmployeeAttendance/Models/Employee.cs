using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EmployeeAttendance.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [DisplayName("نام")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }

        //public int DayId { get; set; }
        public List<Attendance> Attendances { get; set; }

        public string GetFullName()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}