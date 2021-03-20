using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeAttendance.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        //[DisplayName("ساعت و تاریخ")]
        //[Required(ErrorMessage = "لطفا زمان را مشخص کنید")]
        //[DataType(DataType.DateTime)]
        public DateTime? DateTime { get; set; }
        public string AttendanceType { get; set; }
        public string Note { get; set; }
        //[DataType(DataType.Time)]


        //[DisplayName("پایان فعالیت")]
        //[Required(ErrorMessage = "لطفا زمان پایان فعالیت را مشخص کنید")]
        //public DateTime CheckOut { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}