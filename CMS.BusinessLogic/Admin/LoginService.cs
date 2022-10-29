using System;
using System.Collections.Generic;
using System.Text;
using CMS.Infrastructure.Models;
using CMS.DBContext.Admin;
using System.Data;

namespace CMS.BusinessLogic.Admin
{
    public class LoginService
    {
        public DataTable GetValidateUser(LoginModel login)
        {
            LoginDBContext dbContext = new LoginDBContext();
            DataTable dt = dbContext.GetValidateUser(login);
            return dt;
        }
    }
}
