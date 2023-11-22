using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Domain;

namespace CoursesWebb.Controllers
{
    public class EnrollmentController : Controller
    {
        SystemClass unS = SystemClass.Instance;

        public IActionResult EnrollStudent(int userID, int courseID)
        {
            Student student = unS.FindUserByID(userID) as Student;
            Course course = unS.FindCourseID(courseID);

            if (student != null && course != null)
            {
                Enrollment enrollment = new Enrollment(course, student);
                unS.AddEnrollment(student, course, enrollment);
                return View(enrollment);
            }
            else
            {
                ViewBag.msg = "Student or course not found";
                return View();

            }
        }

        public IActionResult CheckEnrollments ()
        {
            if (unS.ListEnrollments().Count > 0)
            {
                List<Enrollment> enrollments = unS.ListEnrollments();
                return View(enrollments);
            }
            else
            {
                ViewBag.msg = "There are no enrollments at the moment";
                return View();
            }
        }

        [HttpPost]
        public IActionResult CheckEnrollmentByEmail (int courseID)
        {
            List<User> users = new List<User>();
            if (unS.CheckStudentsEnrolledCourse(courseID).Count > 0)
            {
                users = unS.CheckStudentsEnrolledCourse(courseID);
                return View(users);
            }
            else
            {
                ViewBag.msg = "No students were found";
                return View();

            }
        }
    }
}
