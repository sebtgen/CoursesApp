using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Instructor : User
    {
        string instructorName;
        DateTime startDate;
        List<Course> coursesTeach;
        public string InstructorName { get => instructorName; set => instructorName = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public List<Course> CoursesTeach { get => coursesTeach; set => coursesTeach = value; }

        public Instructor() { }
        public Instructor(string email, string password, string phoneNumber, string instructorName, DateTime startDate, List<Course> coursesTeach)
            : base(password, email, phoneNumber)
        {
            this.instructorName = instructorName;
            this.startDate = startDate;
            this.coursesTeach = coursesTeach;
        }


        public void ValidateInstructor()
        {
            base.ValidateUser();
            ValidateName();
            ValidateStartDate();
        }

        private void ValidateName()
        {
            if (!string.IsNullOrEmpty(instructorName))
            {
                throw new Exception("Name cannot be empty");
            }
        }

        private void ValidateStartDate()
        {
            if (StartDate.Year > DateTime.Now.Year)
            {
                throw new Exception("Start date cant be in the future");
            }
        }

        public string FindCID()
        {
            string list = string.Empty;

            foreach (Course c in coursesTeach)
            {
                list += ($"{c.Title} ");
            }
            return list;
        }

        public void ValidateCourseAlreadyAdded(Course course)
        {
            if (coursesTeach.Contains(course))
            {
                throw new Exception("Course is already in the portfolio");
            }
        }

        public void AddCourse(Course course)
        {
            coursesTeach.Add(course);
        }

        public override string ToString()
        {
            string list = FindCID();
            return base.ToString() + $"Instructor name: {instructorName}\n" +
                $"Courses in portfolio: {list}\n";
        }
    }
}
