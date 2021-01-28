using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Entities
{
    public class Category : BaseEntity
    {
        public virtual List<SubCategory> SubCategories { get; set; }
        public bool PublicationStatus { get; set; }
        public int? PictureID { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
