using System;
using System.Collections.Generic;
using System.Linq;
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
                    {
                        Email = model.Email,
                        Password = model.Password,
                        PhoneNumber = model.PhoneNumber,
                        StudentName = model.StudentName,
                        DateBirth = model.DateBirth,
                        RemoteOnly = model.RemoteOnly
                    };
                    break;
                case "Instructor":
                    user = new Instructor
                    {
                        Email = model.Email,
                        Password = model.Password,
                        PhoneNumber = model.PhoneNumber,
                        InstructorName = model.InstructorName,
                        StartDate = model.StartDate,
                        CoursesTeach = model.CoursesTeach
                    };
                    break;
                default:
                    throw new ArgumentException("Invalid");
            }
            return user;
        }
    }
}
