using CMS.BusinessLogic.Admin;
using CMS.Infrastructure.Models.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Admin.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManagementController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

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
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

            // Skip number of Rows count  
            var start = Request.Form["start"].FirstOrDefault();

            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault();

            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

            // Sort Column Direction (asc, desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            // Search Value from (Search box)  
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10, 20, 50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;

            int skip = start != null ? Convert.ToInt32(start) : 0;

            int recordsTotal = 0;

            CategoryService categoryService = new CategoryService();
            List<ProductCategoriesModel> productCategoriesList = categoryService.GetProductCategories();

            //total number of rows counts   
            recordsTotal = productCategoriesList.Count();
            var roles = roleManager.Roles;
            var data = roles.ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        [HttpGet]
        public async Task<IActionResult> CreateUserRole(UserRolesModel userRolesModel)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = userRolesModel.Name
            };

            IdentityResult result = await roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserRoleById([FromBody] IdentityRole identityRole)
        {
            var role = await roleManager.FindByIdAsync(identityRole.Id);
            if (role == null)
            {
                return Json(new { message = "Error" });
            }
            else
            {
                IdentityResult result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return Json(new { message = "Success" });
                }
                else
                {
                    return Json(new { message = "Error" });
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateUserRole([FromBody] UserRolesModel rolesModel)
        {
            if (string.IsNullOrEmpty(rolesModel.Id))
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = rolesModel.Name
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return Json(new { message = "Success" });
                }
                else
                {
                    return Json(new { message = "Error" });
                }
            }
            else
            {
                return Json(new { message = "Error" });
            }
        }

        public async Task<IActionResult> GetUserRoleById(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return Json(new { message = "Error" });
            }
            else
            {
                return Json(new { message = "Success", data = role });
            }
        }
    }
}
