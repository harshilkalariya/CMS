using CMS.DBContext.Admin;
using CMS.Infrastructure.Models;
using CMS.Infrastructure.CommonHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace CMS.BusinessLogic.Admin
{
    public class CategoryService
    {
        public List<ProductCategoriesModel> GetProductCategories()
        {
            ProductCategoriesDbContext dbContext = new ProductCategoriesDbContext();
            DataTable dt = dbContext.GetProductCategories();
            List<ProductCategoriesModel> productCategories = Helpers.ConvertDataTableToModelList<ProductCategoriesModel>(dt);
            return productCategories;
        }

        public void AddUpdateProductCategory(ProductCategoriesModel productCategories)
        {
            ProductCategoriesDbContext dbContext = new ProductCategoriesDbContext();
            dbContext.AddUpdateProductCategory(productCategories);
        }

        public ProductCategoriesModel GetProductCategoryById(string categoryId)
        {
            ProductCategoriesDbContext dbContext = new ProductCategoriesDbContext();
            DataTable dt = dbContext.GetProductCategoryById(categoryId);
            ProductCategoriesModel productCategory = Helpers.ConvertDataTableToModelList<ProductCategoriesModel>(dt).FirstOrDefault();
            return productCategory;
        }

        public void DeleteProductCategoryById(string categoryId)
        {
            ProductCategoriesDbContext dbContext = new ProductCategoriesDbContext();
            dbContext.DeleteProductCategoryById(categoryId);
        }
    }
}
