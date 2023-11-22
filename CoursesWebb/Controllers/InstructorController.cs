using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CoursesWebb.Controllers
{
    public class InstructorController : Controller
    {
        SystemClass unS = SystemClass.Instance;
        public IActionResult ListCoursesTeach(string msg)
        {
            List<Course> courses = new List<Course>();
            //change to search logged instructor
            if (unS.ReturnCoursesInstructor("test4@gmail.com").Count()>0)
            {
                courses = unS.ReturnCoursesInstructor("test4@gmail.com");

                if (msg != null)
                {
                    ViewBag.error = msg;
                    return View(courses);
                }
                else
                {
                    return View(courses);
                }
            }
            else
            {
                ViewBag.error = "No courses were found for the instructor";
                return View();
            }
        }

        public IActionResult AddCourseInstructorPortfolio (string msg, bool type)
        {
            if (unS.ListUsers().Count > 0)
            {
                List<User> users = unS.ListUsers();
                ViewBag.users = users;

                if (msg != null && !type)
                {
                    ViewBag.error = msg;
                    return View(users);
                }
                else
                {
                    ViewBag.success = msg;
                    return View(users);
                }
            }
            else
            {
                ViewBag.msg = "No instructors were found";
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddCourseInstructorPortfolio(int userID, int courseID)
        {
                try
            {
                Course course = unS.FindCourseID(courseID);
                User user = unS.FindUserByID(userID);

                if (course != null && user != null)
                {
                        Instructor instructor = user as Instructor;
                        unS.AddCourseToPortfolio(instructor, course);
                         return RedirectToAction("AddCourseInstructorPortfolio", "Instructor", new { msg = "Course has been successfully added" , type = true});
                }
                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("AddCourseInstructorPortfolio", "Instructor", new { msg = ex.Message , type = false});
            }
        }
    }
}
