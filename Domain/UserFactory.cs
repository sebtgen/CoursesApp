using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class UserFactory
    {
        public static User CreateUser(UserViewModel model)
        {
            User user;

            switch (model.UserType)
            {
                case "Student":
                    user = new Student
                    (
                        model.Email,
                        model.Password,
                        model.PhoneNumber,
                        model.StudentName,
                        model.DateBirth,
                        model.RemoteOnly
                    );
                    break;
                case "Instructor":
                   

                    user = new Instructor
                    (
                        model.Email,
                        model.Password,
                        model.PhoneNumber,
                        model.InstructorName,
                        model.StartDate,
                        model.CoursesTeach = new List<Course>()
                    );

                    Instructor instructor = (Instructor)user;
                    SystemClass unS = SystemClass.Instance;
                    foreach (var courseID in model.SelectedCourses)
                    {
                        Course course = unS.FindCourseID(courseID);
                        if (course != null)
                        {
                            instructor.CoursesTeach.Add(course);
                        }
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid");
            }
            return user;
        }
    }
}
