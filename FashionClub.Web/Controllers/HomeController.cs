using FashionClub.Services;
using FashionClub.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;

namespace FashionClub.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            HomePageViewModels model = new HomePageViewModels();
            model.Categories = CategoryServices.Instance.Categories().Where(x=>x.PublicationStatus==true);
            model.NewArrivalsProdusts = ProductServices.Instance.Products().Where(x => x.Featured == true).OrderByDescending(y => y.CreteDateTime).Take(10).ToList();
            model.Products = ProductServices.Instance.Products().Where(x=>x.Featured==true);
            model.Brands = BrandServices.Instance.GetBrands().Where(x => x.Featured == true).ToList();
            return View(model);
        }

        [HttpGet]
        public PartialViewResult Modal(int Id)
        {
            ProductViewModel model = new ProductViewModel();
            model.Product = ProductServices.Instance.GetById(Id);
            return PartialView("_Modal", model);
        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            ProductViewModel model = new ProductViewModel();
            model.Product = ProductServices.Instance.GetById(Id);
            var categoryId = model.Product.Categories.Select(x => x.ID).FirstOrDefault();

            model.Products = ProductServices.Instance.Products().Where(i=>i.Categories.Any(j=>j.ID== categoryId)).Take(10).AsQueryable();

            ViewBag.Category = CategoryServices.Instance.Categories();
            return View(model);
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}