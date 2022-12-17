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
    public class UserManagementDbContext
    {

        public DataTable GetUserRoles()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserRoles", con))
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

        private string GetConnectionString()
        {
            var cb = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = cb.Build();
            return configuration.GetValue<string>("ConnectionStrings:DefaultConn");
        }
    }
}
