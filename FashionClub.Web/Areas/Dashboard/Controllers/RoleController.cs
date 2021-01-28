using FashionClub.Data;
using FashionClub.Entities;
using FashionClub.Services;
using FashionClub.Web.Areas.Dashboard.Authorize.Services;
using FashionClub.Web.Areas.Dashboard.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FashionClub.Web.Areas.Dashboard.Controllers
{
    [AllowAnonymous]
    public class RoleController : Controller
    {
        private FashionClubSignInManager _signInManager;
        private FashionClubUserManager _userManager;
        private FashionClubRoleManager _roleManager;

        public RoleController()
        {
        }

        public RoleController(FashionClubUserManager userManager, FashionClubSignInManager signInManager,FashionClubRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public FashionClubSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<FashionClubSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public FashionClubUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<FashionClubUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
         public FashionClubRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<FashionClubRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        [HttpGet]
        //[DisplayName("List of Role")]
        public ActionResult Index(string searchTerm)
        {
            RoleListingModel model = new RoleListingModel();
            model.SearchTerm = searchTerm;
            return View(model);
        }
        [HttpGet]
        public PartialViewResult Listing(string searchTerm,int? pageNo)
        {
            pageNo = pageNo > 0 ? pageNo : 1;
            var pageSize = 2;
            
            RoleListingModel model = new RoleListingModel();
            var roles = RoleManager.Roles.ToList();//.AsQueryable();
           
            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(y => y.Name.ToLower().Contains(searchTerm.ToLower())).ToList();//.AsQueryable();
            }
            var totalItems = roles.Count();
            model.Roles = roles.OrderBy(x => x.Name).Skip((pageNo.Value - 1) * pageSize).Take(pageSize).ToList();//.AsQueryable();
            
            model.Pager = new Pager(totalItems, pageNo, pageSize);
            return PartialView("_Listing",model);
        }
        //[DisplayName("Role Creat Update")]
        [HttpGet]
        public ActionResult Action(string Id)
        {     
            RoleActionModel model = new RoleActionModel();
            model.Areas = RoleManager.GetArea();
            model.RouteInfos = RoleManager.GetRoutes();
            //SetRoutes();
            if (!string.IsNullOrEmpty(Id)) //try to edit records
            {
                var role = RoleManager.FindById(Id);
                model.ID = role.Id;
                model.Name = role.Name;
                model.Description = role.Description;
                model.FashionClubRoleRouteInfos = role.RouteInfos;
            }
            return View( model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Action(RoleActionModel model)
        {
            IdentityResult result = null;
            JsonResult json = new JsonResult();
            var routes = new List<RouteInfo>();

            if (model.Selected != null && model.Selected.Count() > 0)
            {
                var routeIds = model.Selected.ToList();
                routes = RoleManager.GetRoutesByIds(routeIds).ToList();
            }

            if (!string.IsNullOrEmpty(model.ID)) //try to edit records 
            {
                //var role = RoleManager.FindById(model.ID);
                //role.RouteInfos.Clear();
                //role.RouteInfos.AddRange(routes.Select(x => x));
                //result = await RoleManager.UpdateAsync(role);
                var role = new FashionClubRole();
                role.Id = model.ID;
                role.Name = model.Name;
                role.Description = model.Description;
                role.RouteInfos = new List<RouteInfo>();
                var outPut = RoleManager.Edit(role, model.Selected.ToList());
                if (outPut)
                {
                    result = IdentityResult.Success;
                }
            }
            else
            {
                var role = new FashionClubRole();
                role.Name = model.Name;
                role.Description = model.Description;
                role.RouteInfos = new List<RouteInfo>();
                role.RouteInfos.AddRange(routes.Select(x => x));
                if (!RoleManager.RoleExists(role.Name))
                {
                  result = await RoleManager.CreateAsync(role);
                }
                result=IdentityResult.Failed("This Role Already Exists");
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            if (!result.Succeeded)
            {
                return json;
            }

            return RedirectToAction("Index", "Role", new { searchTerm = "", pageNo = model.PageNO });
        }
        [HttpGet]
        public ActionResult Details(string Id)
        {
            RoleDetailsModel model = new RoleDetailsModel();
            
            model.Role = RoleManager.FindById(Id);
            model.Users = model.Role.Users.Select(y=>UserManager.FindById(y.UserId)).AsQueryable();
            return View(model);
        }
        [HttpGet]
        public ActionResult bootstrapTree()
        {
            SharedServices.GetActionRoles("Contact", "Roles");
            return View();
        }
        public ActionResult SetRoutes()
        {
            JsonResult json = new JsonResult();
           List<RouteInfo> routeInfo = new List<RouteInfo>();
            DynamicRoleServices services = new DynamicRoleServices();
            var routes = services.GetMenu();
            routeInfo.AddRange(routes.Select(x => new RouteInfo()
            {
                Controller=x.Controller,
                Action=x.Action,
                Url=('/' + x.Area + '/' + x.Controller+'/'+x.Action).Replace("//", "/"),
                Area=x.Area,
                Attributes=x.Attributes,
                ReturnType=x.ReturnType
            }));
            var result = RoleManager.SaveRoutes(routeInfo);
            json.Data = new { Success = result };
            return Json(json, JsonRequestBehavior.AllowGet);
            
        }
       
    }
}