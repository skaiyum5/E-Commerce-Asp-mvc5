using FashionClub.Web.Areas.Dashboard.Models;
using FashionClub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionClub.Entities;

namespace FashionClub.Web.Areas.Dashboard.Controllers
{
    public class SubCategoryController : Controller
    {
        // GET: Dashboard/SubCategory
        [HttpGet]
        public ActionResult Index()
        {
            SubCategoryListModel model = new SubCategoryListModel();
            model.Categories = CategoryServices.Instance.GetAll();
            return View(model);
        }
        public PartialViewResult Listing(string searchTerm, int? categoryId, int? pageNo)
        {
            var page = pageNo ?? 1;
            var pageSize = 2;
            var totalItems = SubCategoryServices.Instance.SearchSubCategoriesCount(searchTerm, categoryId);
            SubCategoryListModel model = new SubCategoryListModel();
            model.SubCategories = SubCategoryServices.Instance.SearchSubCategories(searchTerm, categoryId, page, pageSize);
            model.SearchTerm = searchTerm;
            model.CategoryId = categoryId;
            model.Pager = new Pager(totalItems, page, pageSize);
            return PartialView("_Listing", model);
        }
        [HttpGet]
        public ActionResult Action(int? ID,int? pageNO)
        {
            SubCategoryActionModel model = new SubCategoryActionModel();
            model.Categories=new MultiSelectList(CategoryServices.Instance.GetAll(),"ID","Name");
            model.SelectedCategories = new List<Category>();
            if (ID.HasValue && ID > 0)//for Edit
            {
                var subCategory = SubCategoryServices.Instance.GetByID(ID.Value);
                
                model.ID = subCategory.ID;
                model.Name = subCategory.Name;
                model.Description = subCategory.Description;
                model.PublicationStatus = subCategory.PublicationStatus;
                model.SelectedCategories = new List<Category>();
                model.SelectedCategories = subCategory.Categories;
                model.PageNo = pageNO;
                
                var categoryIds = subCategory.Categories.Select(x => x.ID).ToList();
                model.Categories = new MultiSelectList(CategoryServices.Instance.GetAll(), "ID", "Name",categoryIds);
                
            }

            return PartialView("_Action", model);
        }
        [HttpPost]
        public ActionResult Action(SubCategoryActionModel model)
        {

            //var CategoryIDs = !string.IsNullOrEmpty(subCategoryModel.CategoryIDs) ? subCategoryModel.CategoryIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();
            var CategoryIds = model.CategoryIds.ToList();
            if (model.ID > 0)
            {
                ////For Edit
                SubCategory subCategory = new SubCategory();
                subCategory.ID = model.ID;
                subCategory.Name = model.Name;
                subCategory.Description = model.Description;
                subCategory.PublicationStatus = model.PublicationStatus;
                subCategory.Categories = new List<Category>();
                SubCategoryServices.Instance.Update(subCategory, CategoryIds);
            }
            else
            {
                ////For Create
                SubCategory subCategory = new SubCategory();
                subCategory.Name = model.Name;
                subCategory.Description = model.Description;
                subCategory.PublicationStatus = model.PublicationStatus;
                subCategory.Categories = new List<Category>();
                //subCategory.Categories.AddRange(categories.Select(x => new Category { ID = x.ID }));
                SubCategoryServices.Instance.Create(subCategory, CategoryIds);

            }
            return RedirectToAction("Listing","SubCategory",new {searchTerm=model.SearchTerm,categoryId=model.CategoryId,pageNo=model.PageNo });
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            SubCategoryDeleteModel model = new SubCategoryDeleteModel();
            model.ID = id;
            return PartialView("_Delete", model);
        }
        [HttpPost]
        public ActionResult Delete(SubCategoryDeleteModel model)
        {
            if (ModelState.IsValid && model.ID > 0)
            {
                SubCategoryServices.Instance.Delete(model.ID);
            }

            return RedirectToAction("Listing");
        }
    }
}