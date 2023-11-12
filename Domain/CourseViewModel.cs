using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CourseViewModel
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxStudents { get; set; }
        public DateTime StartDate { get; set; }

        public int DurationHours { get; set; }
        public OnlineCourse.onlinePlatform Platform { get; set; }

        public string Location { get; set; }
        public string MeetingDays { get; set; }

        public string CourseType { get; set; }
    }
}
