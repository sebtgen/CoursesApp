﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Enrollment
    {
        Course enrolledCourse;
        Student enrolledStudent;
        DateTime enrollDate;
        int enrollID;
        static int lastEnrollID = 0;

        public Course EnrolledCourse { get => enrolledCourse; set => enrolledCourse = value; }
        public Student EnrolledStudent { get => enrolledStudent; set => enrolledStudent = value; }
        public DateTime EnrollDate { get => enrollDate; set => enrollDate = value; }
        public int EnrollID { get => enrollID; set => enrollID = value; }

        public Enrollment(Course enrolledCourse, Student enrolledStudent)
        {
            EnrolledCourse = enrolledCourse;
            EnrolledStudent = enrolledStudent;
            EnrollDate = DateTime.Now;
            enrollID = lastEnrollID;
            lastEnrollID++;
        }

        public override string ToString()
        {
            return $"Course name: {enrolledCourse.Title}\n" +
                   $"Student name: {enrolledStudent.StudentName}\n" +
                   $"Enrollment date: {enrollDate}\n";
        }

    }
}
