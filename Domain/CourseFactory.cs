using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                    course = new OnlineCourse
                    {
                        Title = model.Title,
                        Description = model.Description,
                        MaxStudents = model.MaxStudents,
                        DurationHours = model.DurationHours,
                        Platform = model.Platform
                    };
                    break;
                case "Regular":
                    course = new RegularCourse
                    {
                        Title = model.Title,
                        Description = model.Description,
                        MaxStudents = model.MaxStudents,
                        Location = model.Location,
                        MeetingDays = model.MeetingDays
                    };
                    break;
                default:
                    throw new ArgumentException("Invalid");
            }

            return course;
        }
    }
}