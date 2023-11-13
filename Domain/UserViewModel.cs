using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public string StudentName { get; set; }
        public DateTime DateBirth { get; set; }
        public bool RemoteOnly { get; set; }

        public string InstructorName { get; set; }
        public DateTime StartDate { get; set; }
        public List<Course> CoursesTeach { get; set; }

        public string UserType { get; set; }


    }
}
