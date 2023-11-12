using Domain;


namespace CoursesApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            SystemClass unS = SystemClass.Instance;
            int option = 0;
            
            string[] options = {"Create course", "Register user",  "List courses", "List instructors",
                                "List students", "Enroll student", "Add course to instructor portfolio"};
            do
            {
                
                Menu(options);
                Console.WriteLine(" ");
                option = InputInt("");
                Console.WriteLine(" ");

                switch (option)
                {
                    case 1:
                        CreateCourse();
                        break;
                    case 2:
                        CreateUser();
                        break;
                    case 3:
                        ListCourses();
                        break;
                    case 4:
                        ListInstructors();
                        break;
                    case 5:
                        ListStudents();
                        break;
                    case 6:
                        EnrollCourseStudent();
                        break;
                    case 7:
                        AddCoursePortfolio();
                        break;
                }
            }
            while (option != 0);
        }

        static void Menu(string[] options)
        {
            int number = 1;
            Console.WriteLine("Enter a number or 0 to exit");
            Console.WriteLine("");

            foreach (string option in options)
            {
                Console.WriteLine($"{number} - {option}");
                number++;
            }
        }

        #region Create/register methods
        static void CreateCourse()
        {
            SystemClass unS = SystemClass.Instance;
            Console.Clear();
            CourseType courseType = GetCourseTypeFromUser();

            try
            {
                string title = InputString("Enter course title: ");
                string description = InputString("Enter course description: ");
                int maxStudents = InputInt("Enter number of students: ");
                DateTime testDate = DateTime.Now;

                if (courseType.Equals(CourseType.Regular))
                {
                    string location = InputString("Enter course location: ");
                    string meetingDays = InputString("Enter course meeting days: ");
                    unS.AddCourse(new RegularCourse(
                        title,
                        description,
                        maxStudents,
                        testDate,
                        location,
                        meetingDays));
                    Console.Clear();
                }
                else if (courseType.Equals(CourseType.Online))
                {
                    int duration = InputInt("Enter duration hours: ");
                    OnlineCourse.onlinePlatform coursePlatform = InputEnumOnlineCourse("Enter online platform: (Zoom, Udemy, Coursera, Webex, Google)");
                    unS.AddCourse(new OnlineCourse(
                        title,
                        description,
                        maxStudents,
                        testDate,
                        duration,
                        coursePlatform));
                    Console.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        static void CreateUser()
        {
            SystemClass unS = SystemClass.Instance;
            Console.Clear();
            UserType userType = GetUserTypeFromUser();

            try
            {
                string password = InputString("Enter password: ");
                string email = InputString("Enter email: ");
                string phone = InputString("Enter phone: ");
                string name = InputString("Enter name: ");

                if (userType.Equals(UserType.Student))
                {

                    DateTime birth = DateTime.Now;
                    bool online = InputBool("Enter if its online: ");
                    unS.AddUser(new Student(password, email, phone, name, birth, online));
                }
                else if (userType.Equals(UserType.Instructor))
                {
                    DateTime start = DateTime.Now;
                    List<Course> courses = AddCoursesList();
                    unS.AddUser(new Instructor(password, email, phone, name, start, courses));
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static void EnrollCourseStudent()
        {
            SystemClass unS = SystemClass.Instance;

            try
            {
                User user = ReadUser(GetUserTypeFromUser());

                if (user is Student)
                {
                    Student student = user as Student;
                    Course course = ReadCourse();
                    Enrollment enrollment = new Enrollment(course, student);
                    unS.AddEnrollment(student, course, enrollment);
                    Console.WriteLine(" ");
                    Console.WriteLine($"Student was enrolled to {course.Title} successfully");
                }
                else
                {
                    Console.WriteLine("Invalid user");
                }


            }
            catch (Exception e)
            {
                throw e;
            }
            Console.ReadLine();
        }

        static void AddCoursePortfolio()
        {
            SystemClass unS = SystemClass.Instance;

            User user = ReadUser(GetUserTypeFromUser());

            if (user is Instructor)
            {
                Instructor instructor = user as Instructor;
                Course course = ReadCourse();
                unS.AddCourseToPortfolio(instructor, course);
            }
        }

        static List<Course> AddCoursesList()
        {
            SystemClass unS = SystemClass.Instance;
            List<Course> list = new List<Course>();
            int id;
            bool flag = true;

            do
            {
                Console.Clear();
                ListCourses();
                id = InputInt("Enter course ID to add or type 'quit' to exit: ");

                if (id == -1)
                {
                    flag = false;
                }


                else if (unS.FindCourseID(id) != null)
                {
                    if (!list.Contains(unS.FindCourseID(id)))
                    {

                        list.Add(unS.FindCourseID(id));
                        Console.WriteLine("Course has been added");
                        Console.ReadLine();

                    }

                    else
                    {
                        Console.WriteLine("Course has already been added");
                        Console.ReadLine();
                    }
                }

                else
                {
                    Console.WriteLine("Invalid course ID");
                }


            }
            while (flag);

            return list;
        }
        #endregion

        #region List methods
        static void ListStudents()
        {
            SystemClass unS = SystemClass.Instance;
            Console.Clear();
            foreach (Student student in unS.FindStudents())
            {

                Console.WriteLine(student.ToString());
                Console.WriteLine(" ");

                if (unS.ReturnCoursesStudent(student.Email).Count() > 0)
                {
                    Console.WriteLine("Courses enrolled into: ");
                    Console.WriteLine(" ");

                    foreach (Course c in unS.ReturnCoursesStudent(student.Email))
                    {
                        Console.WriteLine(c.ToString());
                    }
                }

                Console.WriteLine(" ");
            }
            Console.ReadLine();
        }

        static void ListInstructors()
        {
            SystemClass unS = SystemClass.Instance;
            Console.Clear();

            foreach (Instructor i in unS.FindInstructors())
            {
                Console.WriteLine(i.ToString());
            }
        }

        static void ListCourses()
        {
            SystemClass unS = SystemClass.Instance;

            Console.Clear();
            foreach (Course c in unS.ListCourses())
            {
                Console.WriteLine(c.ToString());
            }
        }
        #endregion

        #region Input/read methods
        public enum CourseType
        {
            Regular,
            Online
        }

        public enum UserType
        {
            Student,
            Instructor
        }

        static CourseType GetCourseTypeFromUser()
        {
            while (true)
            {
                string type = InputString("Enter course type (Regular/Online): ").ToLower();

                if (type == "regular")
                {
                    return CourseType.Regular;
                }
                else if (type == "online")
                {
                    return CourseType.Online;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'Regular' or 'Online'.");
                }
            }
        }

        static UserType GetUserTypeFromUser()
        {
            while (true)
            {
                string type = InputString("Enter user type (Student/Instructor: ").ToLower();

                if (type == "student")
                {
                    return UserType.Student;
                }
                else if (type == "instructor")
                {
                    return UserType.Instructor;
                }
                else
                {
                    Console.WriteLine("Invalid input Please enter 'Student' or 'Instructor'.");
                }
            }
        }
        static User ReadUser(UserType userType)
        {
            SystemClass unS = SystemClass.Instance;
            string email;
            bool flag = false;
            string userTypeString = userType == UserType.Student ? "Student" : "Instructor";
            User user = null;

            while (!flag)
            {
                email = InputString($"Enter {userTypeString} email to search: ");
                Console.WriteLine(" ");

                user = userType == UserType.Student ? unS.FindUserByEmail(email, true) : unS.FindUserByEmail(email, false);

                if (user != null)
                {
                    flag = true;
                }
                else
                {
                    Console.WriteLine($"{userTypeString} not found. Try again");
                    Console.WriteLine(" ");
                }
            }

            return user;
        }

        static Course ReadCourse()
        {
            SystemClass unS = SystemClass.Instance;
            Course course = null;
            int id;
            bool flag = false;

            ListCourses();
            Console.WriteLine(" ");
            while (!flag)
            {

                id = InputInt("Enter ID of course to use: ");
                Console.WriteLine(" ");
                course = unS.FindCourseID(id);

                if (course != null)
                {
                    flag = true;
                }
                else
                {
                    Console.WriteLine(" Course not found. Try again");
                    Console.WriteLine(" ");
                }
            }
            return course;

        }

        static int InputInt(string msg)
        {
            bool flag;
            int value;

            do
            {
                Console.Write(msg);
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    return -1;
                }

                flag = int.TryParse(input, out value);

                if (!flag || value < 0)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Invalid input");
                    Console.WriteLine(" ");
                    flag = false;
                }


            }
            while (!flag);
            return value;
        }

        static string InputString(string msg)
        {
            bool flag;
            string value;

            do
            {
                Console.Write(msg);
                value = Console.ReadLine();

                if (value == null)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Invalid input");
                    Console.WriteLine(" ");
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            while (!flag);

            return value;
        }

        static OnlineCourse.onlinePlatform InputEnumOnlineCourse(string msg)
        {
            bool flag;
            OnlineCourse.onlinePlatform value;

            do
            {
                Console.Write(msg);
                string input = Console.ReadLine();

                flag = Enum.TryParse<OnlineCourse.onlinePlatform>(input, out value);

                if (!flag)
                {
                    Console.WriteLine("Invalid online platform");
                }

            }
            while (!flag);
            return value;
        }

        static bool InputBool(string msg)
        {
            bool flag;
            bool boolean;

            do
            {
                Console.Write(msg);
                string value = Console.ReadLine();

                switch (value.ToLower())
                {
                    case "s":
                        value = "true";
                        break;
                    case "si":
                        value = "true";
                        break;
                    case "true":
                        value = "true";
                        break;
                    case "n":
                        value = "false";
                        break;
                    case "no":
                        value = "false";
                        break;
                    case "false":
                        value = "false";
                        break;
                }

                flag = bool.TryParse(value, out boolean);

                if (!flag)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Invalid boolean");
                    Console.WriteLine(" ");

                }


            }
            while (!flag);
            return boolean;
        }
        #endregion








    }
}