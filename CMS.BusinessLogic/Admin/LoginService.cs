using System;
using System.Collections.Generic;
using System.Text;
using CMS.DBContext.Admin;
using System.Data;
using CMS.Infrastructure.Models.Admin;

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
