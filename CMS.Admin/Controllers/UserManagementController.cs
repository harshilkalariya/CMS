using CMS.BusinessLogic.Admin;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

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
            UserManagementService userManagement = new UserManagementService();
            userManagement.GetUserRoles();

            List<string> data = new List<string>();
            
            var jsonData = new { recordsFiltered = 21, recordsTotal = 40, data = data };
            return Json(jsonData);
        }
    }
}
