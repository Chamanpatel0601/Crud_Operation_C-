////using EmployeeDetails.Models;
////using EmployeeDetails.Service;
////using Microsoft.AspNetCore.Mvc;

////namespace MVCDemo5.Controllers
////{
////    public class AccountController : Controller
////    {
////        private readonly IEmployeeService _employeeService;

////        public AccountController(IEmployeeService employeeService)
////        {
////            _employeeService = employeeService;
////        }

////        [HttpGet]
////        public IActionResult Login()
////        {
////            return View();
////        }

////        [HttpPost]
////        public IActionResult Login(User user)
////        {
////            var loggedUser = _employeeService.Login(user.Email, user.Password);
////            if (loggedUser != null)
////            {
////                HttpContext.Session.SetString("UserEmail", user.Email);
////                return RedirectToAction("Index", "Employee");
////            }

////            ViewBag.Message = "Invalid Email or Password!";
////            return View();
////        }

////        public IActionResult Logout()
////        {
////            HttpContext.Session.Clear();
////            return RedirectToAction("Login", "Account");
////        }

////        [HttpGet]
////        public IActionResult Register()
////        {
////            return View();
////        }

////        [HttpPost]
////        public IActionResult Register(User user)
////        {
////            if (ModelState.IsValid)
////            {
////                _employeeService.AddUser(user);
////                return RedirectToAction("Login");
////            }
////            return View(user);
////        }
////    }
////}







//using Microsoft.AspNetCore.Mvc;
//using EmployeeDetails.Service;
//using EmployeeDetails.Models;

//namespace EmployeeDetails.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly IEmployeeService _employeeService;

//        public AccountController(IEmployeeService employeeService)
//        {
//            _employeeService = employeeService;
//        }

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Login(string email, string password)
//        {
//            var employee = _employeeService.GetAllEmployee()
//                            .FirstOrDefault(e => e.Email == email && e.Password == password);

//            if (employee != null)
//            {
//                HttpContext.Session.SetInt32("EmployeeId", employee.Id);
//                return RedirectToAction("Index", "Employee");
//            }

//            TempData["Error"] = "Invalid Email or Password";
//            return View();
//        }

//        public IActionResult Logout()
//        {
//            HttpContext.Session.Clear();
//            return RedirectToAction("Login", "Account");
//        }





//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Register(User user)
//        {
//            if (ModelState.IsValid)
//            {
//                _employeeService.AddUser(user);
//                return RedirectToAction("Login");
//            }
//            return View(user);
//        }
//    }
//}







using Microsoft.AspNetCore.Mvc;
using EmployeeDetails.Service;
using EmployeeDetails.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeeDetails.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public AccountController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // ---------------- LOGIN ----------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Email and Password are required.";
                return View();
            }

            var user1 = _employeeService.GetAllUser()
                                .FirstOrDefault(e => e.Email == email && e.Password == password);

            if (user1 != null)
            {
                // Login success
                //HttpContext.Session.SetInt32("EmployeeId", employee.Id);
                //HttpContext.Session.SetString("EmployeeName", employee.Name); // optional
                return RedirectToAction("Index", "Employee"); // After login, go to Employee Index
            }
            else
            {
                // Login failed
                TempData["Error"] = "Invalid Email or Password!";
                return View();
            }
        }

        // ---------------- LOGOUT ----------------
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        // ---------------- REGISTER ----------------
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _employeeService.AddUser(user);
                TempData["Success"] = "Registration successful. Please login.";
                return RedirectToAction("Login");
            }
            return View(user);
        }
    }
}
