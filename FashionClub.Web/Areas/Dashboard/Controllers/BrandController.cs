using FashionClub.Entities;
using FashionClub.Services;
using FashionClub.Web.Areas.Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionClub.Web.Areas.Dashboard.Controllers
{
    
    public class BrandController : Controller
    {
        [HttpGet]

        [DynamicRoleAuthorize]
        public ActionResult Index(string searchTerm)
        {
            BrandListingModel model = new BrandListingModel();
            model.SearchTerm = searchTerm;
            return View(model);
        }
        [HttpGet]
        public ActionResult Action(int? ID,string searchTerm, int? pageNo)
        {
            BrandActionModel model = new BrandActionModel();
            //For Edit
            if (ID.HasValue && ID > 0)
            {
                var brand = BrandServices.Instance.GetByID(ID.Value);
                model.ID = brand.ID;
                model.Name = brand.Name;
                model.Description = brand.Description;
                model.Featured = brand.Featured;
                model.PictureID = brand.PictureID;
                model.Picture = brand.Picture;
                model.SearchTerm = searchTerm;
                model.PageNO = pageNo;
            }
            return PartialView("_Action", model);
        }
        [HttpPost]
        public ActionResult Action(BrandActionModel model)
        {
            //For Edit
            if (model.ID > 0)
            {
                Brand brand = new Brand();
                brand.ID = model.ID;
                brand.Name = model.Name;
                brand.Description = model.Description;
                brand.PictureID = model.PictureID;
                brand.Featured = model.Featured;
                BrandServices.Instance.Update(brand);
            }
            else //For Create
            {
                Brand brand = new Brand();
                brand.Name = model.Name;
                brand.Description = model.Description;
                brand.Featured = model.Featured;
                brand.PictureID = model.PictureID;
                BrandServices.Instance.Create(brand);
            }
            //BrandListingModel data = new BrandListingModel();

            //data.Brands = BrandServices.Instance.SearchBrands();
            //return PartialView("_Listing", data);
            return ListingView(model.SearchTerm,model.PageNO);
        }
        [HttpGet]
        public ActionResult Delete(int? Id, string searchTerm, int? pageNo)
        {

            var result = BrandServices.Instance.Delete(Id.Value);
            if (result)
            {
               return ListingView(searchTerm, pageNo);
            }
            return Json("Internal server Error", JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult ListingView(string searchTerm,int? pageNo)
        {
            var itemPerPage = 2;
            pageNo = pageNo ?? 1;
            BrandListingModel model = new BrandListingModel();
            model.SearchTerm = searchTerm;
            model.Brands = BrandServices.Instance.SearchBrands(searchTerm, pageNo.Value, itemPerPage);
            var totalItems = BrandServices.Instance.SearchBrandsCount(searchTerm, pageNo.Value, itemPerPage);
            model.Pager = new Pager(totalItems, pageNo, itemPerPage);
            return PartialView("_Listing", model);
        }

    }
}