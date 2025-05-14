using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeDetails.Models;
using EmployeeDetails.Service;

namespace MVCDemo5.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employee;
        private readonly ILogger<EmployeeController> _logger;
        

        // Constructor with dependencies injected
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employee = employeeService;
            _logger = logger;
           
        }

        // Display data as JSON using DI in Action
        public IActionResult Display([FromServices] IEmployeeService es)
        {
            var model = es.GetAllEmployee();
            return Json(model);
        }

        // Simple view
        public ActionResult Dispview()
        {
            return View();
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            var model = _employee.GetAllEmployee();
            return View(model);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var model = _employee.GetEmployee(id);
            if (model == null)
            {
                Response.StatusCode = 404;
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
            return View(model);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            LoadDepartments();
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employee.Add(employee);
                return RedirectToAction(nameof(Index));
            }

            LoadDepartments(employee.DepartmentId);
            return View(employee);
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _employee.GetEmployee(id);
            if (model == null)
            {
                return NotFound();
            }

            LoadDepartments(model.DepartmentId);
            return View(model);
        }

        // Helper method to load dropdowns
        private void LoadDepartments(int? selectedId = null)
        {
            var departments = _employee.GetAllDepartment();
            ViewData["DepartmentId"] = new SelectList(departments, "Id", "Name", selectedId);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var model = _employee.Update(emp);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(emp);
                }
            }
            IEnumerable<Department> DepartmentName = _employee.GetAllDepartment();
            ViewData["DepartmentId"] = new SelectList(DepartmentName, "Id", "Name", emp.DepartmentId);
            return View(emp);
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _employee.GetEmployee(id);
            if (model == null)
                return NotFound();
            return View(model);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteData(int id)
        {
            try
            {
                var model = _employee.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: EmployeeController/SendEmail
        [HttpPost]
        public async Task<IActionResult> SendEmail(string subject, string message, string sendOption, int? employeeId, string? departmentName)
        {
            // Get all employees
            var employees = _employee.GetAllEmployee();

            // Select recipients based on the send option
            IEnumerable<string> recipients = sendOption switch
            {
                "all" => employees.Select(e => e.Email),
                "individual" => employees.Where(e => e.Id == employeeId).Select(e => e.Email),
                "department" => employees.Where(e => e.Department.Name == departmentName).Select(e => e.Email),
                _ => new List<string>()
            };

            // Send email to all recipients
           

            TempData["Message"] = "Emails sent successfully!";
            return RedirectToAction("Index");
        }


        public ActionResult Login()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Account");
            }
            var model = _employee.GetAllEmployee();
            return View(model);
        }

    }
}
