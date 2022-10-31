using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers
{
    public class UserManagementController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserRoles()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetUserRoles()
        {
            return View();
        }
    }
}
