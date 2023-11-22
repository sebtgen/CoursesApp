using Microsoft.AspNetCore.Mvc;
using Domain;

namespace CoursesWebb.Controllers
{
    public class UserController : Controller
    {
        SystemClass unS = SystemClass.Instance;

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserViewModel model)
        {
            try
            {
                User user = UserFactory.CreateUser(model);
                unS.AddUser(user);
                List<User> users = unS.ListUsers();
                ViewBag.users = users;
                return View(users);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Test";
                return RedirectToAction("CreateUser");
            }
        }

        public IActionResult ListUsers ()
        {
            List<User> users = unS.ListUsers();
            ViewBag.users = users;
            return View(users);
        }

        public IActionResult PreEnroll()
        {
            List<User> users = unS.ListUsers();
            ViewBag.users = users;
            return View();
        }

        public IActionResult ListCoursesStudents ()
        {
            List<Course> courses = new List<Course>();

            if (unS.ReturnCoursesStudent("test1@gmail.com").Count() > 0)
            {
                courses = unS.ReturnCoursesStudent("test1@gmail.com");
                return View(courses);
            }
            else
            {
                ViewBag.msg = "No courses were found";
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
