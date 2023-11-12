using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class Course
    {
        #region Attributes
        string title;
        string description;
        int maxStudents;
        int currentStudents;
        int courseID;
        static int lastID = 0;
        DateTime startDate;
        #endregion
        public string CourseType { get; protected set; }

        protected Course(string courseType)
        {
            CourseType = courseType;
        }
        #region Properties
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int MaxStudents
        {
            get { return maxStudents; }
            set { maxStudents = value; }
        }
        public int CourseID
        {
            get { return courseID; }
            set { courseID = value; }
        }
        public int LastID
        {
            get { return lastID; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
        }

        public int CurrentStudents
        {
            get { return currentStudents; }
            set { currentStudents = value; }
        }
        #endregion
        #region Constructor

        public Course () { }
        public Course(string title, string description, int maxStudents, DateTime startDate)
        {
            this.courseID = lastID;
            lastID++;
            this.title = title;
            this.description = description;
            this.maxStudents = maxStudents;
            this.startDate = startDate;
        }
        #endregion

        #region Methods

        public void ValidateCourse()
        {
            ValidateStrings();
            ValidateMaxStudents();
            ValidateStartDate();
        }

        private void ValidateStrings()
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new Exception("Title cannot be empty");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new Exception("Description cannot be empty");
            }
        }

        private void ValidateMaxStudents()
        {
            if (maxStudents > 100)
            {
                throw new Exception("Max students cannot be more than 100");
            }
        }

        public void AddStudent()
        {
            this.currentStudents++;
        }

        private void ValidateStartDate()
        {
            if (StartDate.Year > DateTime.Now.Year)
            {
                throw new Exception("Start date cant be in the future");
            }
        }
        public override string ToString()
        {
            return $"Title: {Title}\n" +
                   $"Description: {Description}\n" +
                   $"Max students: {MaxStudents}\n" +
                   $"Course ID: {CourseID}\n" +
                   $"Start date: {StartDate}\n";
        }
        #endregion
    }
}
