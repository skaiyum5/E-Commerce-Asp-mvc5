using FashionClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionClub.Web.Areas.Dashboard.Models
{
    public class CategoryListModel
    {
       public List<Category> Categories { get; set; }
        public CategoryListModel()
        {
            Categories = new List<Category>();
        }
    }
    public class CategoryActionModel
    { 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
        public bool PublicationStatus { get; set; }
        public int? PictureID { get; set; }
        public Picture Picture { get; set; }
    }
    public class CategoryDeleteModel
    {
        public int ID { get; set; }
    }
   
    public class SubCategoryListModel
    {
        public string SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public Pager Pager { get; set; }
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public SubCategoryListModel()
        {
            Categories = new List<Category>();
            SubCategories = new List<SubCategory>();
        }
    }

    public class SubCategoryActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] CategoryIds { get; set; }
        // public string CategoryIDs { get; set; }
        public string SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public int? PageNo { get; set; }
        public MultiSelectList Categories { get; set; }
        public virtual List<Category> SelectedCategories { get; set; }
        public bool PublicationStatus { get; set; }
        public SubCategoryActionModel()
        {
            SelectedCategories = new List<Category>();
        }
        
    }
    public class SubCategoryDeleteModel
    {
        public int ID { get; set; }
    }
}