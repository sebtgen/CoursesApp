using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Domain
{
    public static class CourseFactory
    {
        
        public static Course CreateCourse(CourseViewModel model)
        {
            Course course;

            switch (model.CourseType)
            {
                case "Online":
                    course = new OnlineCourse(model.Title, model.Description, model.MaxStudents, model.StartDate, model.DurationHours, model.Platform);
                    break;
                case "Regular":
                    course = new RegularCourse(model.Title, model.Description, model.MaxStudents, model.StartDate, model.Location, model.MeetingDays);
                    break;
                default:
                    throw new ArgumentException("Invalid course");
            }

            return course;
        }
    }


}
