using CMS.BusinessLogic.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using CMS.Infrastructure.Models.Admin;

namespace CMS.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category()

        {
            return View();
        }

        [HttpPost]
        public IActionResult Category(ProductCategoriesModel productCategories)
        {
            return View();
        }

        public IActionResult AddUpdateProductCategory([FromBody] ProductCategoriesModel productCategories)
        {
            CategoryService categoryService = new CategoryService();
            categoryService.AddUpdateProductCategory(productCategories);
            return Json(new {message = "Success"});
        }

        public IActionResult GetProductCategoryById(string categoryId)
        {
            CategoryService categoryService = new CategoryService();
            ProductCategoriesModel productCategory = categoryService.GetProductCategoryById(categoryId);
            return Json(new { productCategory  = productCategory , message = "Success"});
        }

        [HttpPost]
        public IActionResult DeleteProductCategoryById(string categoryId)
        {
            CategoryService categoryService = new CategoryService();
            categoryService.DeleteProductCategoryById(categoryId);
            return Json(new { message = "Success" });
        }

        public IActionResult GetProductCategories()
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
            //Paging   
            var data = productCategoriesList.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data  
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

        }
    }
}
