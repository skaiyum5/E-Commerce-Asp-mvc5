using FashionClub.Entities;
using FashionClub.Services;
using FashionClub.Web.Areas.Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FashionClub.Web.Areas.Dashboard.Controllers
{
    public class SizeController : Controller
    {
        // GET: Dashboard/Size
        [HttpGet]
        public ActionResult Index(string searchTerm)
        {
            SizeListingModel model = new SizeListingModel();
            model.SearchTerm = searchTerm;
            return View(model);
        }
        [HttpGet]
        public ActionResult Action(int? Id, string searchTerm, int? pageNo)
        {
            SizeActionModel model = new SizeActionModel();
            //For Edit
            if (Id.HasValue && Id > 0)
            {
                var size = SizeServices.Instance.GetByID(Id.Value);
                model.ID = size.ID;
                model.Name = size.Name;
                model.Description = size.Description;
                model.SearchTerm = searchTerm;
                model.PageNo = pageNo;
            }
            return PartialView("_Action", model);
        }
        [HttpPost]
        public ActionResult Action(SizeActionModel model)
        {
            //For Edit
            if (model.ID > 0)
            {
                Size size = new Size();
                size.ID = model.ID;
                size.Name = model.Name;
                size.Description = model.Description;
                SizeServices.Instance.Update(size);
            }
            else //For Create
            {
                Size size = new Size();
                size.Name = model.Name;
                size.Description = model.Description;
                SizeServices.Instance.Create(size);
            }
            return ListingView(model.SearchTerm,model.PageNo);
        }
        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            
            var result = SizeServices.Instance.Delete(Id.Value);
            if (result)
            {
                return ListingView("",1);
            }
            return Json("Internal server Error",JsonRequestBehavior.AllowGet);
            //else
            //{
            //    return HttpNotFoundResult;

            //}
           
        }
        
        public PartialViewResult ListingView(string searchTerm, int? pageNo)
        {
            var pageSize = 2;
            var page = pageNo ?? 1;
            var totalItems = SizeServices.Instance.SearchSizesCount(searchTerm, page, pageSize);
            SizeListingModel model = new SizeListingModel();
            model.SearchTerm = searchTerm;
            model.Pager = new Pager(totalItems, page, pageSize);
            model.Sizes = SizeServices.Instance.SearchSizes(searchTerm, page, pageSize);

            return PartialView("_Listing", model);
        }
        
    }
}