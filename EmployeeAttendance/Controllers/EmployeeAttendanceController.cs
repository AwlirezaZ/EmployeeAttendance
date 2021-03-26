using EmployeeAttendance.Data;
using EmployeeAttendance.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EmployeeAttendance.Controllers
{
    public class EmployeeAttendanceController : Controller
    {
        EmployeeAttendanceDbContext db = new EmployeeAttendanceDbContext();
        // GET: EmployeeAttendance
        public ActionResult Index()
        {
            var model = AttendanceMapper.Map(db.Attendances.Include(a => a.Employee).ToList());
            return View(model);
        }
        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult AddAttendance()
        {
            var model = new AttendanceRecordViewModel()
            {
                Employees = db.Employees.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult AddAttendance(string fullNameAndId, DateTime date , DateTime time,string attendanceType,string description)
        {
            if (ModelState.IsValid)
            {
                string[] list = fullNameAndId.Split('.');
                Employee employee = db.Employees.Find(int.Parse(list[0]));
                string dateTime = date.ToShortDateString() + " " + time.ToLongTimeString();
                var attendance = new Attendance()
                {
                    DateTime = DateTime.Parse(dateTime),
                    Employee = employee,
                    EmployeeId = employee.EmployeeId,
                    AttendanceType = attendanceType,
                    Note = description
                };
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult ShowSpecificEmployee(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            var employee = db.Employees.Find(id);
            if (employee == null) return HttpNotFound();
            //var employeeAttendances = db.Attendances.Where(a => a.Employee == employee).ToList();
            var employeeAttendances = db.Attendances.Where(a => a.EmployeeId == id).ToList();
            var viewModel = AttendanceMapper.Map(employeeAttendances);
            ViewBag.fullName = employee.GetFullName();
            return View(viewModel);                        
        }
        public ActionResult FilterAttendances(string nameAndId,DateTime? fromDate,DateTime? toDate)
        {
            List<Attendance> attendances = new List<Attendance>();
            int id = 0;
            if (nameAndId[1] == '.')
            {
            string[] list = nameAndId.Split('.');
                id = int.Parse(list[0]);
            }
            if (fromDate != null && nameAndId[1] == '.' && toDate != null)
                attendances = 
                    db.Attendances.Where(x => x.DateTime > fromDate)
                    .Where(x => x.DateTime < toDate)
                    .Where(x => x.EmployeeId == id)
                    .Include(x => x.Employee).ToList();
            else if (fromDate != null && nameAndId[1] == '.') attendances = attendances.Where(x => x.DateTime > fromDate).ToList();
            else if (toDate != null && nameAndId[1] == '.')
                attendances = 
                    db.Attendances.Where(x => x.DateTime < toDate)
                    .Where(x => x.EmployeeId == id)
                    .Include(x => x.Employee).ToList();
            else if (toDate != null && fromDate != null)
                attendances = 
                    db.Attendances.Where(x => x.DateTime < toDate)
                    .Where(x => x.DateTime > fromDate)
                    .Include(x => x.Employee).ToList();
            else if (toDate != null)
                attendances = 
                    db.Attendances.Where(x => x.DateTime < toDate)
                    .Include(x => x.Employee).ToList();
            else if (fromDate != null)
                attendances = 
                    db.Attendances.Where(x => x.DateTime > fromDate)
                    .Include(x => x.Employee).ToList();
            var model = AttendanceMapper.Map(attendances);

            return View("~/Views/EmployeeAttendance/Index.cshtml",model);
        }
    }
}