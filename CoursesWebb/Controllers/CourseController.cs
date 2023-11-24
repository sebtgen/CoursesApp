using Microsoft.AspNetCore.Mvc;
using Domain;
namespace CoursesWebb.Controllers
{

    public class CourseController : Controller
    {
        SystemClass systemInstance = SystemClass.Instance;

        public IActionResult CreateCourse (string msg, bool type)
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

        [HttpPost]
        public IActionResult CreateCourse(CourseViewModel model)
        {
            try
            {
                Course course = CourseFactory.CreateCourse(model);
                systemInstance.AddCourse(course);
                return RedirectToAction("CreateCourse", "Course", new { msg = "Course has been successfully created", type = true });
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateCourse", "Course", new { msg = ex.Message, type = false });

            }
        }

        public IActionResult ListCourses ()
        {
            List<Course> courses = systemInstance.ListCourses();
            ViewBag.courses = courses;
            return View(courses);
        }





    }
}
