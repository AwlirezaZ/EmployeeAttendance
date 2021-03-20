using EmployeeAttendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EmployeeAttendance.Data
{
    public class EmployeeAttendanceDbContext : DbContext
    {
        public EmployeeAttendanceDbContext() : base("name =EmployeeAttendanceContext")
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}