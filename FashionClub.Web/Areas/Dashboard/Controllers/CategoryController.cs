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
    public class CategoryController : Controller
    {
        // GET: Dashboard/Category
        [HttpGet]
        public ActionResult Index()
        {
            CategoryListModel model = new CategoryListModel();
            model.Categories= CategoryServices.Instance.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            CategoryActionModel model = new CategoryActionModel();
            if (ID.HasValue)
            {
              var category= CategoryServices.Instance.GetByID(ID.Value);
                model.ID = category.ID;
                model.Name = category.Name;
                model.Description = category.Description;
                model.PictureID = category.PictureID;
                model.Picture = category.Picture;
                model.PublicationStatus = category.PublicationStatus;
            }
            return PartialView("_Action",model);
        }
        [HttpPost]
        public ActionResult Action(CategoryActionModel model)
        {
            CategoryListModel data = new CategoryListModel();
            
            Category category = new Category();
            category.Name = model.Name;
            category.Description = model.Description;
            category.PictureID = model.PictureID;
            category.PublicationStatus = model.PublicationStatus;
            if (model.ID>0)//for Edit
            {
                category.ID = model.ID;
                CategoryServices.Instance.Update(category);
            }
            else
            {
              CategoryServices.Instance.Create(category);
            }

            data.Categories = CategoryServices.Instance.GetAll();
            return PartialView("_Listing",data);
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            CategoryDeleteModel model = new CategoryDeleteModel();
            model.ID = ID;
            return PartialView("_Delete",model);
        }
        [HttpPost]
        public ActionResult Delete(CategoryDeleteModel model)
        {
            JsonResult json = new JsonResult();
            var Id = model.ID;
            var result = CategoryServices.Instance.Delete(Id);
            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to Success!" };

            }
            return json;
        }


    }
}