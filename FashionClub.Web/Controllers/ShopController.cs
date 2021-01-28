using FashionClub.Entities;
using FashionClub.Services;
using FashionClub.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionClub.Web.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult CardProducts()
        {
            ListOFProductsViewModel model = new ListOFProductsViewModel();

            var cookieProductIds = Request.Cookies["CardProducts"];
            if (cookieProductIds!=null)
            {
                var cardProductIds = cookieProductIds.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.Products = ProductServices.Instance.GetByIds(cardProductIds).ToList();
                model.CardProductIds = cardProductIds;
            }

            return PartialView("_CardProducts",model);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}