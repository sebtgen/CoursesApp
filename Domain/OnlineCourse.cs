using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OnlineCourse : Course
    {
        public enum onlinePlatform { ZOOM, UDEMY, COURSERA, WEBEX, GOOGLE }
        int durationHours;
        onlinePlatform platform;

        public int DurationHours { get => durationHours; set => durationHours = value; }
        public onlinePlatform Platform { get => platform; set => platform = value; }

        public OnlineCourse() { }
        public OnlineCourse(string title, string description, int maxStudents, DateTime startDate, int durationHours, onlinePlatform platform) : base(title, description, maxStudents, startDate)
        {
            this.durationHours = durationHours;
            this.platform = platform;
        }

        public override string ToString()
        {
            return base.ToString() + $"Duration hours: {DurationHours}\n" + $"Platform: {Platform}\n";
        }
    }
}
