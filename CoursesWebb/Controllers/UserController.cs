using Microsoft.AspNetCore.Mvc;
using Domain;

namespace CoursesWebb.Controllers
{
    public class UserController : Controller
    {
        SystemClass unS = SystemClass.Instance;

        public IActionResult CreateUser()
        {
            List<User> users = unS.ListUsers();
            ViewBag.users = users;
            return View(users);
        }

        [HttpPost]
        public IActionResult CreateUser(UserViewModel model)
        {
            try
            {
                User user = UserFactory.CreateUser(model);
                unS.AddUser(user);
                List<User> users = unS.ListUsers();
                ViewBag.users = users;
                return View(users);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Test";
                return RedirectToAction("CreateUser");
            }
        }
    }
}
