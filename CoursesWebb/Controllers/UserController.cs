using Microsoft.AspNetCore.Mvc;
using Domain;

namespace CoursesWebb.Controllers
{
    public class UserController : Controller
    {
        SystemClass unS = SystemClass.Instance;

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser (UserViewModel model)
        {
            try
            {
                User user = UserFactory.CreateUser(model);
                unS.AddUser(user);
                return View();
            }
            catch (Exception ex) {
                ViewBag.Mensaje = $"Test";
            return View();
            }
        }
    }
}
