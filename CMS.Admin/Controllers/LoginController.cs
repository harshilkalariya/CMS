using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Infrastructure.Models;
using CMS.BusinessLogic.Admin;
using System.Data;
using CMS.Admin.Models;

namespace CMS.Admin.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            LoginService service = new LoginService();
            DataTable result = service.GetValidateUser(login);
            if (result.Rows.Count > 0)
            {
                SessionManagement.Set(this.HttpContext.Session, key: "UserId", value: result.Rows[0]["Id"]);
                SessionManagement.Set(this.HttpContext.Session, key: "Firstname", value: result.Rows[0]["Firstname"]);
                //SessionManagement.Get(this.HttpContext.Session, key: "Firstname");
                string name = SessionManagement.Get<string>(this.HttpContext.Session,"Firstname");

                HttpContext.Session.Clear();
                string newName = SessionManagement.Get<string>(this.HttpContext.Session, "Firstname");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
