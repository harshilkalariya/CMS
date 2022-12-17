using CMS.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace CMS.DBContext.Admin
{
    public class ProductCategoriesDbContext
    {
        public DataTable GetProductCategories()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("GetCategories", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        var dataReader = cmd.ExecuteReader();
                        dataTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log
            }

            return dataTable;
        }

        public void AddUpdateProductCategory(ProductCategoriesModel productCategories)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("AddUpdateProductCategory", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int);
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@IsDeleted", SqlDbType.Bit);

                        cmd.Parameters["@Id"].Value = productCategories.Id;
                        cmd.Parameters["@Name"].Value = productCategories.Name;
                        cmd.Parameters["@IsDeleted"].Value = productCategories.IsDeleted;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log
            }
        }


        public DataTable GetProductCategoryById(string categoryId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("GetProductCategoryById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int);
                        cmd.Parameters["@Id"].Value = categoryId;

                        con.Open();
                        var dataReader = cmd.ExecuteReader();
                        dataTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log
            }

            return dataTable;
        }

        public void DeleteProductCategoryById(string categoryId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteProductCategoryById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int);
                        cmd.Parameters["@Id"].Value = categoryId;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log
            }
        }

        private string GetConnectionString()
        {
            var cb = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = cb.Build();
            return configuration.GetValue<string>("ConnectionStrings:DefaultConn");
        }
    }
}
