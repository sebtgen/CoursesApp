using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CoursesWebb.Controllers
{
    public class InstructorController : Controller
    {
        SystemClass unS = SystemClass.Instance;
        public IActionResult ListCoursesTeach()
        {
            List<Course> courses = new List<Course>();
            //change to search logged instructor
            if (unS.ReturnCoursesInstructor("test4@gmail.com").Count()>0)
            {
                courses = unS.ReturnCoursesInstructor("test4@gmail.com");
                return View(courses);
            }
            else
            {
                ViewBag.msg = "No courses were found for the instructor";
                return View();
            }
        }

        public IActionResult AddCourseInstructorPortfolio ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourseInstructorPortfolio(int courseID)
        {
            return View();
        }
    }
}
