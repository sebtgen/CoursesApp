using Microsoft.AspNetCore.Mvc;
using Domain;
using Microsoft.VisualBasic;

namespace CoursesWebb.Controllers
{
    public class UserController : Controller
    {
        SystemClass systemInstance = SystemClass.Instance;

        public IActionResult CreateUser(string msg, bool type)
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
        public IActionResult CreateUser(UserViewModel model)
        {
            try
            {
                User user = UserFactory.CreateUser(model);
                systemInstance.AddUser(user);
                return RedirectToAction("Index", "Home", new { msg = "User has been successfully created", type = true });
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateUser", "User", new { msg = ex.Message, type = false });
            }
        }

        public IActionResult ListUsers ()
        {
            List<User> users = systemInstance.ListUsers();
            ViewBag.users = users;
            return View(users);
        }


        public IActionResult ListCoursesStudents ()
        {
            List<Course> courses = new List<Course>();

            if (systemInstance.ReturnCoursesStudent("test1@gmail.com").Count() > 0)
            {
                courses = systemInstance.ReturnCoursesStudent("test1@gmail.com");
                return View(courses);
            }
            else
            {
                ViewBag.msg = "No courses were found";
                return View();
            }
        }

        public IActionResult PreEnroll(string msg, bool type)
        {
            if (systemInstance.ListUsers().Count > 0)
            {
                List<User> users = systemInstance.ListUsers();
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
                ViewBag.msg = "No students were found";
                return View();
            }
       
        }

        [HttpPost]
        public IActionResult PreEnroll(int userID, int courseID)
        {
            return RedirectToAction("EnrollStudent", "Enrollment", new { userID, courseID });
        }

    }
}
