using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoursesWebb.Controllers
{
    public class HomeController : Controller
    {
        SystemClass systemInstance = SystemClass.Instance;
        public IActionResult Index(string msg, bool type)
        {
            if (!type)
            {
                ViewBag.error = msg;
                return View();
            }
            else
            {
                ViewBag.success = msg;
                return View();
            }
        }

        public IActionResult Login(string msg)
        {
            if (msg != null)
            {
                ViewBag.error = msg;
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpPost]

        public IActionResult Login (string email, string password)
        {
            if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(password))
            {
                User user = systemInstance.FindUserByEmailPass(email, password);

                if (user != null)
                {
                    HttpContext.Session.Clear();
                    HttpContext.Session.SetString("userEmail", user.Email);

                    if (user.GetType() == typeof(Student))
                    {
                        Student student = user as Student;
                        HttpContext.Session.SetString("userRole", "student");

                    }
                    else if (user.GetType() == typeof(Instructor))
                    {
                        Instructor instructor = user as Instructor;
                        HttpContext.Session.SetString("userRole", "instructor");
                    }
                    else
                    {
                        HttpContext.Session.SetString("userRole", "operator");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Home", new { msg = "Incorrect credentials"});

                }
            }
            else
            {
                return RedirectToAction("Login", "Home", new { msg = "Empty fields" });

            }
        }

        public IActionResult Logout ()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
