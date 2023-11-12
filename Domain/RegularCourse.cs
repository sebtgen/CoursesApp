using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RegularCourse : Course
    {
        string location;
        string meetingDays;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public string MeetingDays
        {
            get { return meetingDays; }
            set { meetingDays = value; }
        }

        public RegularCourse() { }
        public RegularCourse(string title, string description, int maxStudents, DateTime startDate, string location, string meetingDays) : base(title, description, maxStudents, startDate)
        {
            this.location = location;
            this.meetingDays = meetingDays;
        }
    }
}
