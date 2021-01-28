using FashionClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionClub.Web.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IQueryable<Product> Products { get; set; }
    }
    public class ListOFProductsViewModel
    {
        public List<Product> Products { get; set; }
        public List<int> CardProductIds { get; set; }
    }

}