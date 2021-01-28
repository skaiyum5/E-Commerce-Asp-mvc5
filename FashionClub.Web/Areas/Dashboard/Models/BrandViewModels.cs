using FashionClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionClub.Web.Areas.Dashboard.Models
{
    public class BrandActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Featured { get; set; }
        public int? PictureID { get; set; }
        public Picture Picture { get; set; }
        public string SearchTerm { get; set; }
        public int? PageNO { get; set; }
        public virtual List<Product> Products { get; set; }

    }
    public class BrandListingModel
    {
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
        public List<Brand> Brands { get; set; }

        public BrandListingModel()
        {
            Brands = new List<Brand>();
        }
    }
}