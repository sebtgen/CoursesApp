using Microsoft.AspNetCore.Mvc;
using Domain;
namespace CoursesWebb.Controllers
{

    public class CourseController : Controller
    {
        SystemClass unS = SystemClass.Instance;

        public IActionResult CreateCourse ()
        {
        return View();
        }        

        [HttpPost]
        public IActionResult CreateCourse(CourseViewModel model)
        {
            try
            {
                Course course = CourseFactory.CreateCourse(model);

                unS.AddCourse(course);
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Test";
                return View();

            }
        }
    }
}
