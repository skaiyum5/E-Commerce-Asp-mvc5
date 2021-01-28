  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Entities
{
    public class Product :BaseEntity
    {
        public string Summary { get; set; } 
        public decimal Price { get; set; }
        public int? DiscountPercentage { get; set; }
        public decimal? AfterDiscountPrice { get; set; }
        public int? Quantity { get; set; }
        public DateTime CreteDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public int Thumbnail { get; set; }
        public bool Featured { get; set; }


        public int? BrandID { get; set; }
        public virtual Brand Brand { get; set; }

        public virtual List<Size> Sizes { get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
        public virtual List<ProductPicture> ProductPictures { get; set; }
       

    }
}
