using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Domain;

namespace CoursesWebb.Controllers
{
    public class EnrollmentController : Controller
    {
        SystemClass systemInstance = SystemClass.Instance;

        public IActionResult EnrollStudent(int userID, int courseID)
        {
            try { 
            Student student = systemInstance.FindUserByID(userID) as Student;
            Course course = systemInstance.FindCourseID(courseID);

            if (student != null && course != null)
            {
                Enrollment enrollment = new Enrollment(course, student);
                systemInstance.AddEnrollment(student, course, enrollment);
                return View(enrollment);
            }
            else
                {
                    return RedirectToAction("PreEnroll", "User", new { msg = "Course or user is null", type = false });

                }
            }
           catch (Exception ex)
            {
                return RedirectToAction("PreEnroll", "User", new { msg = ex.Message, type = false });

            }

        }

        public IActionResult CheckEnrollments ()
        {
            if (systemInstance.ListEnrollments().Count > 0)
            {
                List<Enrollment> enrollments = systemInstance.ListEnrollments();
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
            if (systemInstance.CheckStudentsEnrolledCourse(courseID).Count > 0)
            {
                users = systemInstance.CheckStudentsEnrolledCourse(courseID);
                return View(users);
            }
            else
            {
                return RedirectToAction("ListCoursesTeach", "Instructor", new { msg = "No students were found for the course" });

            }
        }
    }
}
