using FashionClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionClub.Web.Models
{
    public class HomePageViewModels
    {
        public IQueryable<Category> Categories { get; set; }
        public List<Product> NewArrivalsProdusts { get; set; }
        public IQueryable<Product> Products { get; set; }
        public List<Brand> Brands { get; set; }
        public HomePageViewModels()
        {
            Brands = new List<Brand>();
            NewArrivalsProdusts = new List<Product>();
        }
    }
}