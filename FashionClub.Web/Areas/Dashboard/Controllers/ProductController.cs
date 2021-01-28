using FashionClub.Data;
using FashionClub.Entities;
using FashionClub.Services;
using FashionClub.Web.Areas.Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace FashionClub.Web.Areas.Dashboard.Controllers
{
    public class ProductController : Controller
    {
        // GET: Dashboard/Product
        [HttpGet]
        public ActionResult Index(string SearchTerm, int? CategoryId, int? SubCategoryId, int? PageNO)
        {
            var Page = PageNO ?? 1;
            var pageSize = 5;
            ProductListingModel model = new ProductListingModel();
            model.Products = ProductServices.Instance.Products(SearchTerm, CategoryId, SubCategoryId, pageSize, Page);
            model.SearchTerm = SearchTerm;
            model.CategoryId = CategoryId;
            model.SubCategoryId = SubCategoryId;
            ViewBag.Categories = CategoryServices.Instance.GetAll();
            ViewBag.SubCategories = SubCategoryServices.Instance.GetAll();
            return View(model);
        }

        public JsonResult GetFaculties(int u)
        {
            //var facultyList =  context.SubCategories.Select(x => x.Categories
            //                  .Where(y => y.ID == u)
            //                  .Select(z => new SelectListItem { Value = z.ID.ToString(), Text = z.Name }
            //                  )).ToList();
            var facultyList = new List<SelectListItem>{
                                                        new SelectListItem { Value = "2", Text = "shirt" },
                                                        new SelectListItem { Value = "3", Text = "pant" }
                                                    };


            return Json(facultyList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SubCategory(List<int> Ids)
        {
            var subCategories = SubCategoryServices.Instance.GetByCategoryIds(Ids).ToList();
            var subCategoryList = subCategories.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }).ToList();
            //return PartialView("_SubCategory", model);
            return Json(subCategoryList, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Create()
        {
            ProductActionModel model = new ProductActionModel();
            //model.Categories = GetUSelectCategory();
            var categorys = CategoryServices.Instance.GetAll();

            //model.Categories = categorys.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }).ToList();
            //model.SubCategories = new List<SelectListItem>{new SelectListItem { Value = "", Text = "First Select Parent Category",Disabled= true } };

            model.Categories = CategoryServices.Instance.GetAll();
            //model.SubCategories = SubCategoryServices.Instance.GetAll();
            model.Brands = BrandServices.Instance.GetBrands();
            model.Sizes = SizeServices.Instance.GetAll();

            return View(model);


        }

        //private List<SelectListItem> GetUSelectCategory()
        //{
        //    FashionClubDBContext context = new FashionClubDBContext();
        //    return context.Categories
        //             .Select(x => new SelectListItem
        //             {
        //                 Value = x.ID.ToString(),
        //                 Text = x.Name
        //             }).ToList();
        //}

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductActionModel model)
        {
            if (model != null && model.SizeIDs != null)
            {
                List<int> sizeIDs = model.SizeIDs.ToList();
                var categoryIds = model.CategoryIDs.ToList();
                var subCategoryIds = model.SubCategoryIDs.ToList();

                var pictures = new List<ProductPicture>();
                if (model.PictureIds != null && model.PictureIds.Count() > 0)
                {
                    var pictureIds = model.PictureIds.Split(',').Select(ID => int.Parse(ID)).ToList();
                    pictures.AddRange(pictureIds.Select(Id => new ProductPicture() { PictureID = Id }).ToList());
                }

                Product product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    DiscountPercentage = model.DiscountPercentage,
                    AfterDiscountPrice = model.AfterDiscountPrice,
                    Quantity = model.Quantity,
                    BrandID = model.BrandID,
                    Summary = model.Summary,
                    Description = model.Description,
                    Featured = model.Featured,
                    CreteDateTime = DateTime.Now,
                    Sizes = new List<Size>(),
                    ProductPictures = pictures,
                    Thumbnail = model.Thumbnail
                };
                //model.Sizes = context.Sizes.Where(x => sizeIDs.Contains(x.ID)).ToList();
                var result = ProductServices.Instance.Create(product, categoryIds, subCategoryIds, sizeIDs);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Create");
        }
        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            var product = ProductServices.Instance.GetById(Id.Value);

            ProductActionModel model = new ProductActionModel();
            model.ID = product.ID;
            model.Name = product.Name;
            model.Summary = product.Summary;
            model.Price = product.Price;
            model.DiscountPercentage = product.DiscountPercentage;
            model.AfterDiscountPrice = product.AfterDiscountPrice;
            model.Quantity = product.Quantity;
            model.Description = product.Description;
            model.Featured = product.Featured;
            model.CreteDateTime = product.CreteDateTime;
            model.BrandID = product.BrandID;
            model.ProductPictures = product.ProductPictures;
            model.Thumbnail = product.Thumbnail;

            var sizeIds = new List<int>();
            sizeIds = product.Sizes.Select(x => x.ID).ToList();

            //foreach (var size in product.Sizes)
            //{
            //    sizeIds.Add(size.ID);
            //}
            //var selectedCategory = CategoryServices.Instance.GetByID(product.CategoryID);

            //ViewBag.Category = new SelectList(CategoryServices.Instance.GetAll(), "ID", "Name",product.CategoryID);

            //var category = CategoryServices.Instance.GetCategpryWithSubCategoryByID(product.CategoryID);
            //ViewBag.SubCategorys = new SelectList(category.SubCategories, "ID", "Name",product.SubCategoryID);

            ViewBag.Brand = new SelectList(BrandServices.Instance.GetBrands(), "ID", "Name", product.BrandID);
            ViewBag.Categories = new MultiSelectList(CategoryServices.Instance.GetAll(), "ID", "Name", product.Categories.Select(x => x.ID).ToList());

            var subCategories = SubCategoryServices.Instance.GetByCategoryIds(product.Categories.Select(x => x.ID).ToList());
            if (subCategories != null && subCategories.Count() > 0)
            {
                ViewBag.SubCategories = new MultiSelectList(subCategories, "ID", "Name", product.SubCategories.Select(x => x.ID).ToList());
            }
            else
            {
                ViewBag.SubCategories = new List<SelectListItem> { new SelectListItem { Value = "", Text = "First Select Parent Category", Disabled = true } };
            }

            ViewBag.Sizes = new MultiSelectList(SizeServices.Instance.GetAll(), "ID", "Name", sizeIds);

            //return View(product);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductActionModel model)
        {
            var pictureIds = new List<int>();
            if (model.PictureIds != null && model.PictureIds.Count() > 0)
            {
                pictureIds = model.PictureIds.Split(',').Select(x => int.Parse(x)).ToList();
            }

            Product product = new Product();
            product.ID = model.ID;
            product.Name = model.Name;
            product.Summary = model.Summary;
            product.Description = model.Description;
            product.Price = model.Price;
            product.DiscountPercentage = model.DiscountPercentage;
            product.AfterDiscountPrice = model.AfterDiscountPrice;
            product.Quantity = model.Quantity;
            product.BrandID = model.BrandID;
            product.Featured = model.Featured;
            product.CreteDateTime = model.CreteDateTime;
            product.UpdateDateTime = DateTime.Now;
            product.Sizes = new List<Size>();
            if (model.Thumbnail > 0)
            {
                product.Thumbnail = model.Thumbnail;
            }
            else
            {
                product.Thumbnail = pictureIds.FirstOrDefault();
            }

            product.ProductPictures = new List<ProductPicture>();
            product.ProductPictures.AddRange(pictureIds.Select(ID => new ProductPicture() { PictureID = ID, ProductID = model.ID }).ToList());

            //var sizeIds = model.SizeIDs.ToList();
            //var categoryIds = model.CategoryIDs.ToList();
            //var subCategoryIds = model.SubCategoryIDs.ToList();
            //var data = ProductServices.Instance.Update(product, sizeIds);

            var data = ProductServices.Instance.Update(product, model.CategoryIDs, model.SubCategoryIDs, model.SizeIDs);
            if (data)
            {
                return Json("true", JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            var product = ProductServices.Instance.GetById(Id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(int? Id)
        {
            //if (Id==null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            JsonResult json = new JsonResult();

            var result = ProductServices.Instance.Delete(Id.Value);
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

        //public ActionResult SubCategory(int u)
        // {
        //     var facultyList = new List<SelectListItem>{
        //                                                 new SelectListItem { Value = "2", Text = "shirt" },
        //                                                 new SelectListItem { Value = "3", Text = "pant" }
        //                                             };

        //     return Json(facultyList, JsonRequestBehavior.AllowGet);
        // }
    }
}