using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SystemClass
    {
        private static SystemClass instance;
        List<Course> courses = new List<Course>();
        List<User> users = new List<User>();
        List<Enrollment> enrollments = new List<Enrollment>();
        List<Operator> operators = new List<Operator>();
        public static SystemClass Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SystemClass();
                }
                return instance;
            }
        }

        private SystemClass()
        {
            AddCourse(new RegularCourse("Test 1", "Description 1", 1, new DateTime(2025, 09, 25), "Place 1", "M-F"));
            AddCourse(new RegularCourse("Test 2", "Description 2", 2, new DateTime(2024, 05, 21), "Place 2", "M-F"));
            AddCourse(new RegularCourse("Test 3", "Description 3", 3, new DateTime(2024, 02, 11), "Place 3", "M-F"));
            AddCourse(new OnlineCourse("Test 4", "Description 4", 3, new DateTime(2025, 02, 04), 20, OnlineCourse.onlinePlatform.GOOGLE));
            AddCourse(new OnlineCourse("Test 5", "Description 5", 3, new DateTime(2024, 11, 19), 40, OnlineCourse.onlinePlatform.WEBEX));
            AddCourse(new OnlineCourse("Test 6", "Description 6", 3, new DateTime(2024, 02, 01), 60, OnlineCourse.onlinePlatform.ZOOM));

            List<Course> courseList1 = new List<Course>
            {
                courses[0],
                courses[1],
                courses[2]
            };

            List<Course> courseList2 = new List<Course>
            {
                courses[3],
                courses[4],
                courses[5]
            };

            List<Course> courseList3 = new List<Course>
            {
                courses[1],
                courses[3],
                courses[5]
            };

            AddUser(new Student("test1@gmail.com", "PassTest1", "1234567890", "Tito 1", new DateTime(1900, 03, 12)/*, courseList1*/, true));
            AddUser(new Student("test2@gmail.com", "PassTest2", "1234567890", "Tito 2", new DateTime(1901, 05, 27)/*, courseList2*/, false));
            AddUser(new Student("test3@gmail.com", "PassTest3", "1234567890", "Tito 3", new DateTime(1902, 07, 29)/*, courseList3*/, true));

            AddUser(new Instructor("test4@gmail.com", "PassTest1", "1234567890", "Tito 1", DateTime.Now/*, courseList1*/, courseList1));

            AddUser(new Instructor("test5@gmail.com", "PassTest1", "1234567890", "Tito 1", DateTime.Now/*, courseList1*/, courseList3));

            AddUser(new Operator("operator@gmail.com", "operator", "1234567890", "Tito 1"));

            AddEnrollment((Student)users[0], courses[0], new Enrollment(courses[0], (Student)users[0]));
            AddEnrollment((Student)users[0], courses[1], new Enrollment(courses[1], (Student)users[0]));

            AddEnrollment((Student)users[1], courses[0], new Enrollment(courses[0], (Student)users[1]));
            AddEnrollment((Student)users[1], courses[1], new Enrollment(courses[1], (Student)users[1]));
        }


        public enum CourseType
        {
            Regular,
            Online
        }

        public void ValidateUserAlreadyAdded (User user)
        {
            foreach (User u in users)
            {
                if (u.Email.Equals(user.Email.Trim()))
                {
                    throw new Exception("User has already been registered");
                }
            }
           
        }

        public void AddCourse(Course course)
        {
            try
            {
                course.ValidateCourse();
                courses.Add(course);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddUser(User user)
        {
            ValidateUserAlreadyAdded(user);

            if (user.GetType() == typeof(Instructor))
            {
                Instructor instructor = user as Instructor;

                try
                {
                    users.Add(instructor);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            else if (user.GetType() == typeof(Student))
            {
                Student student = user as Student;

                try
                {
                    student.ValidateStudent();
                    users.Add(student);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else if (user.GetType() == typeof(Operator))
            {
                users.Add(user);
            }
        }

        public List<Course> ListCourses()
        {
            List<Course> aux = new List<Course>();
            foreach (Course c in courses)
            {
                aux.Add(c);
            }
            return aux;
        }

        public Course FindCourseID(int id)
        {
            foreach (Course course in courses)
            {
                if (course.CourseID == id)
                {
                    return course;
                }
            }
            return null;
        }

        public void AddEnrollment(Student student, Course course, Enrollment enrollment)
        {
            try
            {
                Enrollment.ValidateStudentAlreadyEnrolled(student, course, enrollments);
                enrollments.Add(enrollment);
                course.AddStudent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User FindUserByEmail(string email, bool isStudent)
        {
            foreach (User u in users)
            {

                if (u.Email.Equals(email))
                {
                    if (u.GetType() == typeof(Student) && isStudent)
                    {
                        return u;
                    }
                    else if (u.GetType() == typeof(Instructor) && !isStudent)
                    {
                        return u;

                    }
                }
            }
            return null;
        }

        public User FindUserByID (int id)
        {
            foreach (User u in users)
            {
                if (u.UserID == id)
                {
                    return u;
                }
            }
            return null;
        }

        public User FindUserByEmailPass(string email, string password)
        {
            foreach (User u in users)
            {
                if (u.Email == email.ToLower() && u.Password == password)
                {
                    return u;
                }
            }
            return null;
        }

        public List<Instructor> FindInstructors()
        {
            List<Instructor> aux = new List<Instructor>();

            foreach (User u in users)
            {
                if (u.GetType() == typeof(Instructor))
                {
                    Instructor instructor = u as Instructor;
                    aux.Add(instructor);
                }

            }
            return aux;
        }

        public List<Student> FindStudents()
        {
            List<Student> aux = new List<Student>();

            foreach (User u in users)
            {
                if (u.GetType() == typeof(Student))
                {
                    Student student = u as Student;
                    aux.Add(student);
                }
            }
            return aux;
        }

        public List<User> ListUsers ()
        {
            List<User> aux = users;
            return aux;
        }

        public List<Course> ReturnCoursesStudent(string email)
        {
            List<Course> aux = new List<Course>();

            foreach (Enrollment e in enrollments)
            {
                if (e.EnrolledStudent.Email == email)
                {
                    aux.Add(e.EnrolledCourse);
                }
            }
            return aux;
        }

        public List<Course> ReturnCoursesInstructor(Instructor instructor)
        {
            List<Course> aux = new List<Course>();

           foreach (User u in users)
            {
                if (u.GetType() == typeof(Instructor) && u.Equals(instructor))
                {
                    Instructor i = u as Instructor;
                    aux = i.CoursesTeach;
                }
            }
           return aux;
        }

        public List<User> CheckStudentsEnrolledCourse (int id)
        {
            List<User> aux = new List<User>();

            foreach (Enrollment e in enrollments)
            {
                if (e.EnrolledCourse.CourseID == id)
                {
                    aux.Add(e.EnrolledStudent);
                }
            }
            return aux;
        }

        public List<Enrollment> ListEnrollments ()
        {
            List<Enrollment> aux = new List<Enrollment>();

            foreach (Enrollment e in enrollments)
            {
                aux.Add(e);
            }
            return aux;
        }

        public void AddCourseToPortfolio(Instructor instructor, Course course)
        {
            try
            {
                instructor.ValidateCourseAlreadyAdded(course);
                instructor.AddCourse(course);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void RemoveCourseFromPortfolio(Instructor instructor, List<int> list)
        {
            foreach (var ID in list)
            {
                foreach(Course course in courses)
                {
                    if (course.CourseID == ID)
                    {
                        instructor.RemoveCourse(course);
                    }
                }
            }
        }
    }
}
