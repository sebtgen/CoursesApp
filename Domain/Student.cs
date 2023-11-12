using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Student : User
    {
        string studentName;
        DateTime dateBirth;
        //List<Course> coursesEnroll;
        bool remoteOnly;

        public Student(string password, string email, string phoneNumber, string studentName, DateTime dateBirth, /*List<Course> courses,*/ bool remoteOnly) : base(password, email, phoneNumber)
        {
            this.studentName = studentName;
            this.dateBirth = dateBirth;
            //this.coursesEnroll = courses;
            this.remoteOnly = remoteOnly;
        }

        public string StudentName { get => studentName; set => studentName = value; }
        public DateTime DateBirth { get => dateBirth; set => dateBirth = value; }
        //public List<Course> CoursesEnroll { get => coursesEnroll; set => coursesEnroll = value; }
        public bool RemoteOnly { get => remoteOnly; set => remoteOnly = value; }

        //public string FindCID()
        //{
        //    string list = string.Empty;

        //    foreach (Course c in coursesEnroll)
        //    {
        //        list += ($"{c.Title} ");
        //    }
        //    return list;
        //}

        public void ValidateStudent()
        {
            base.ValidateUser();
            ValidateName();
            ValidateDate();
        }

        private void ValidateName()
        {
            if (string.IsNullOrEmpty(studentName))
            {
                throw new Exception("Student name cannot be empty");
            }
        }

        private void ValidateDate()
        {
            if (DateBirth.Year > DateTime.Now.Year)
            {
                throw new Exception("Date of birth cant be in the future");
            }
        }


        public override string ToString()
        {
            //string list = FindCID();
            return base.ToString() +
                $"Student name: {studentName}\n" +
                $"Date of birth: {dateBirth}\n" +
                //$"Courses enrolled: {list}\n" +
                $"Remote only: {remoteOnly}";
        }
    }
}
