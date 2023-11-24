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
        bool remoteOnly;

        public Student() { }
        public Student(string email, string password, string phoneNumber, string studentName, DateTime dateBirth, bool remoteOnly) : base(password, email, phoneNumber)
        {
            this.studentName = studentName;
            this.dateBirth = dateBirth;
            this.remoteOnly = remoteOnly;
        }

        public string StudentName { get => studentName; set => studentName = value; }
        public DateTime DateBirth { get => dateBirth; set => dateBirth = value; }
        public bool RemoteOnly { get => remoteOnly; set => remoteOnly = value; }

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
                $"Remote only: {remoteOnly}";
        }
    }
}
