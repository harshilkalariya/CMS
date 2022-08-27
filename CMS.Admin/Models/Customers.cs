using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Admin.Interfaces;

namespace CMS.Admin.Models
{
    public class Customers : ICustomers
    {
        public int count = 5;
        public void add()
        {
            Console.WriteLine(++count);
        }

        public void delete()
        {
            Console.WriteLine("Delete: ");
        }
    }
}
