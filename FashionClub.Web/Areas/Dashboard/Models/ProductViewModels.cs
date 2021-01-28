using FashionClub.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionClub.Web.Areas.Dashboard.Models
{
    public class ProductActionModel
    {
        public int[] CategoryIDs { get; set; }
        public int[] SubCategoryIDs { get; set; }
        public int[] SizeIDs { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
        public int? DiscountPercentage { get; set; }
        public decimal? AfterDiscountPrice { get; set; }
        public int? Quantity { get; set; }
        public int? BrandID { get; set; }
        public bool Featured { get; set; }
        public string PictureIds { get; set; }
        public int Thumbnail { get; set; }
        public DateTime CreteDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }


        public virtual List<Category> Categories { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
        public virtual List<Brand> Brands { get; set; }
        public virtual List<Size> Sizes { get; set; }
        public virtual List<ProductPicture> ProductPictures { get; set; }

        //public List<SelectListItem> Categories { get; set; }
        //public List<SelectListItem> SubCategories { get; set; }
        public ProductActionModel()
        {
            this.Categories = new List<Category>();
            this.SubCategories = new List<SubCategory>();
            this.Brands = new List<Brand>();
            this.Sizes = new List<Size>();
            this.ProductPictures = new List<ProductPicture>();
        }
    }

    public class ProductListingModel
    {
        public string SearchTerm { get; set; }
        public int PageNO { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public List<Product> Products { get; set; }
    }
    public class ProductDetailsModel
    {
        [Display(Name="Product Name")]
        public string Name { get; set; }
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
        public int? DiscountPercentage { get; set; }
        public decimal? AfterDiscountPrice { get; set; }
        public int? Quantity { get; set; }
        [Display(Name = "Product Brand")]
        public virtual Brand Brand { get; set; }
        [Display(Name = "Product Sizes")]
        public virtual List<Size> Sizes { get; set; }
        public DateTime CreteDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        [Display(Name = "Product Pictures")]
        public virtual List<Picture> Pictures { get; set; }
        [Display(Name = "Product Category")]
        public virtual Category Category { get; set; }
        [Display(Name = "Product SubCategory")]
        public virtual SubCategory SubCategory { get; set; }
        public bool Featured { get; set; }
    }
}