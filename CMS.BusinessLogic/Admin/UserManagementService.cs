using CMS.DBContext.Admin;
using CMS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CMS.BusinessLogic.Admin
{
    public class UserManagementService
    {
        public DataTable GetUserRoles()
        {
            UserManagementDbContext dbContext = new UserManagementDbContext();
            DataTable dt = dbContext.GetUserRoles();
            return dt;
        }
    }
}
