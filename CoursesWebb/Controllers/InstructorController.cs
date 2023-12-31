﻿using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CoursesWebb.Controllers
{
    public class InstructorController : Controller
    {
        SystemClass systemInstance = SystemClass.Instance;
        public IActionResult ListCoursesTeach(string msg)
        {
            List<Course> courses = new List<Course>();
            Instructor instructor = systemInstance.FindUserByEmail(HttpContext.Session.GetString("userEmail"), false) as Instructor;
            if (systemInstance.ReturnCoursesInstructor(instructor).Count()>0)
            {
                courses = systemInstance.ReturnCoursesInstructor(instructor);

                if (msg != null)
                {
                    ViewBag.error = msg;
                    return View(courses);
                }
                else
                {
                    return View(courses);
                }
            }
            else
            {
                ViewBag.error = "No courses were found for the instructor";
                return View();
            }
        }

        public IActionResult AddCourseInstructorPortfolio (string msg, bool type)
        {
            if (systemInstance.ListUsers().Count > 0)
            {
                List<User> users = systemInstance.ListUsers();
                ViewBag.users = users;

                if (msg != null && !type)
                {
                    ViewBag.error = msg;
                    return View(users);
                }
                else
                {
                    ViewBag.success = msg;
                    return View(users);
                }
            }
            else
            {
                ViewBag.msg = "No instructors were found";
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddCourseInstructorPortfolio(int userID, int courseID)
        {
                try
            {
                Course course = systemInstance.FindCourseID(courseID);
                User user = systemInstance.FindUserByID(userID);

                if (course != null && user != null)
                {
                        Instructor instructor = user as Instructor;
                        systemInstance.AddCourseToPortfolio(instructor, course);
                         return RedirectToAction("AddCourseInstructorPortfolio", "Instructor", new { msg = "Course has been successfully added" , type = true});
                }
                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("AddCourseInstructorPortfolio", "Instructor", new { msg = ex.Message , type = false});
            }
        }

        public IActionResult RemoveCourseInstructorPortfolio (string msg, bool type)
        {
            if (systemInstance.ListUsers().Count > 0)
            {
                List<User> users = systemInstance.ListUsers();
                ViewBag.users = users;

                if (msg != null && !type)
                {
                    ViewBag.error = msg;
                    return View(users);
                }
                else
                {
                    ViewBag.success = msg;
                    return View(users);
                }
            }
            else
            {
                ViewBag.msg = "No instructors were found";
                return View();
            }
        }

        [HttpPost]

        public IActionResult RemoveCourseInstructorPortfolio (int userID)
        {
            if (userID != null) 
            {
                TempData["instructorID"] = userID;
                Instructor instructor = systemInstance.FindUserByID(userID) as Instructor;

                if (instructor != null)
                {
                    List<Course> courses = systemInstance.ReturnCoursesInstructor(instructor);
                    if (courses.Count > 0)
                    {
                        ViewBag.courses = courses;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("RemoveCourseInstructorPortfolio", "Instructor", new { msg = "No courses were found", type = false });

                    }
                }
                else
                {
                    return RedirectToAction("RemoveCourseInstructorPortfolio", "Instructor", new { msg = "Invalid instructor", type = false });

                }

            }
            else
            {
                return RedirectToAction("RemoveCourseInstructorPortfolio", "Instructor", new { msg = "Invalid user ID", type = false });

            }
        }

        [HttpPost]
        public IActionResult RemoveCourse (List<int> selectedCourses, int instructorID)
        {
            Instructor instructor = systemInstance.FindUserByID(instructorID) as Instructor;
            systemInstance.RemoveCourseFromPortfolio(instructor, selectedCourses);
            return View();
        }
    }
}
