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
            AddCourse(new RegularCourse("Test 1", "Description 1", 1, DateTime.Now, "Place 1", "M-F"));
            AddCourse(new RegularCourse("Test 2", "Description 2", 2, DateTime.Now, "Place 2", "M-F"));
            AddCourse(new RegularCourse("Test 3", "Description 3", 3, DateTime.Now, "Place 3", "M-F"));
            AddCourse(new OnlineCourse("Test 4", "Description 4", 3, DateTime.Now, 20, OnlineCourse.onlinePlatform.GOOGLE));
            AddCourse(new OnlineCourse("Test 5", "Description 5", 3, DateTime.Now, 40, OnlineCourse.onlinePlatform.WEBEX));
            AddCourse(new OnlineCourse("Test 6", "Description 6", 3, DateTime.Now, 60, OnlineCourse.onlinePlatform.ZOOM));

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

            AddUser(new Student("test1@gmail.com", "PassTest1", "1234567890", "Tito 1", DateTime.Now/*, courseList1*/, true));
            AddUser(new Student("test2@gmail.com", "PassTest2", "1234567890", "Tito 2", DateTime.Now/*, courseList2*/, false));
            AddUser(new Student("test3@gmail.com", "PassTest3", "1234567890", "Tito 3", DateTime.Now/*, courseList3*/, true));

            AddUser(new Instructor("test4@gmail.com", "PassTest1", "1234567890", "Tito 1", DateTime.Now/*, courseList1*/, courseList1));


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
                enrollments.Add(enrollment);
                course.AddStudent();
                Console.Clear();
                enrollment.ToString();
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

        public void AddCourseToPortfolio(Instructor instructor, Course course)
        {
            try
            {
                instructor.ValidateCourseAlreadyAdded(course);
                instructor.AddCourse(course);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
