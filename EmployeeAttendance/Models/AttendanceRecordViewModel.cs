using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeAttendance.Models
{
    public class AttendanceRecordViewModel
    {
        public AttendanceRecordViewModel()
        {

        }

        //public List<Attendance> Attendances { get; set; }
        //public List<Day> Days { get; set; }

        public AttendanceRecordViewModel(Attendance attendance)
        {
            if (attendance.DateTime.HasValue)
            {
                Date = attendance.DateTime.Value.ToShortDateString();
                Time = attendance.DateTime.Value.TimeOfDay;
                EmployeeId = attendance.EmployeeId;
                AttendanceId = attendance.AttendanceId;
            }
            FullName = attendance.Employee.GetFullName();
            Note = attendance.Note;
            AttendanceType = attendance.AttendanceType;
            Employee = attendance.Employee;
        }

        public string Date { get; set; }
        //public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        //public DateTime? Time { get; set; }
        public string FullName { get; set; }
        public string Note { get; set; }
        public string AttendanceType { get; set; }
        public int EmployeeId { get; set; }
        public int AttendanceId { get; set; }
        public Employee Employee { get; set; }
        public List<Employee> Employees { get; set; }
    }
    public static class AttendanceMapper
    {
        public static List<AttendanceRecordViewModel> Map(List<Attendance> attendances)
        {
            if (attendances == null)
                return new List<AttendanceRecordViewModel>();
            return attendances.Select(a => new AttendanceRecordViewModel(a)).ToList();

            //var data = new List<AttendanceRecordViewModel>();
            //foreach(var attendance in attendances)
            //{
            //    data.Add(new AttendanceRecordViewModel(attendance));
            //}
            //return data;
        }
    }
}