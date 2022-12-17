using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using CMS.Infrastructure.Models.Admin;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CMS.DBContext.Admin
{
    public class LoginDBContext
    {

        public DataTable GetValidateUser(LoginModel login)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("GetValidateUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = login.Email;
                        cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = login.Password;

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
