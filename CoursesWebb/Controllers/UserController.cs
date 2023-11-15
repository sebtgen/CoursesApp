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

        public IActionResult EnrollStudent()
        {
            List<User> users = unS.ListUsers();
            ViewBag.users = users;
            return View();
        }

        [HttpPost]
        public IActionResult EnrollStudent(int userID, int courseID)
        {
            Student student = unS.FindUserByID(userID) as Student;
            Course course = unS.FindCourseID(courseID);

            if (student != null && course != null)
            {
                Enrollment enrollment = new Enrollment(course, student);
                unS.AddEnrollment(student, course, enrollment);
            }
            return View();
        }

    }
}
