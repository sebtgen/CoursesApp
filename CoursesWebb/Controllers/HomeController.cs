using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoursesWebb.Controllers
{
    public class HomeController : Controller
    {
        SystemClass unS = SystemClass.Instance;
        public IActionResult Index()
        {
            List<Course> courses = unS.ListCourses();
            ViewBag.courses = courses;
            return View(courses);

            //viewbag
        }
    }
}
