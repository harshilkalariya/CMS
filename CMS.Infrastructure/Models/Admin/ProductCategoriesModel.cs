using System;

namespace CMS.Infrastructure.Models.Admin
{
    public class ProductCategoriesModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
