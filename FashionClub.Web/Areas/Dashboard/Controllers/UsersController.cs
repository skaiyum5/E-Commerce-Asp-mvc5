using FashionClub.Entities;
using FashionClub.Services;
using FashionClub.Web.Areas.Dashboard.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FashionClub.Web.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
    {
        private FashionClubSignInManager _signInManager;
        private FashionClubUserManager _userManager;
        private FashionClubRoleManager _roleManager;

        public UsersController()
        {
        }

        public UsersController(FashionClubUserManager userManager, FashionClubSignInManager signInManager, FashionClubRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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

        // GET: Dashboard/Users
        [HttpGet]
        public ActionResult Index()//string searchTerm, string roleId, int? pageNo)
        {
        //    pageNo = pageNo.HasValue && pageNo > 0 ? pageNo : 1;
        //    int pageSize = 5;
            UsersListingViewModel model = new UsersListingViewModel();
        //    var users = UserManager.Users.AsQueryable();
        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        users = users.Where(x => searchTerm.Contains(x.FullName.ToLower()) || searchTerm.Contains(x.UserName.ToLower()) || searchTerm.Contains(x.Email.ToLower())).AsQueryable();
        //    }
        //    if (!string.IsNullOrEmpty(roleId))
        //    {
        //        users = users.Where(x => x.Roles.Any(y => y.RoleId.Contains(roleId))).AsQueryable();
        //    }
        //    model.Users = users.OrderBy(x => x.Id).Skip((pageNo.Value - 1) * pageSize).Take(pageSize).ToList();
            var roles = RoleManager.Roles.ToList();
            model.Roles = roles;
            ViewBag.Roles =  roles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return View(model);
        }
        [HttpGet]
        public PartialViewResult Listing(string searchTerm,string roleId,int? pageNo)
        {
            pageNo = pageNo.HasValue&& pageNo>0? pageNo: 1;
            int pageSize = 5;
            UsersListingViewModel model = new UsersListingViewModel();
            var users = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(x=>x.FullName.ToLower().Contains(searchTerm.ToLower()) || x.UserName.ToLower().Contains(searchTerm.ToLower()) || x.Email.ToLower().Contains(searchTerm.ToLower())).AsQueryable();
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                users = users.Where(x => x.Roles.Any(y => y.RoleId.Contains(roleId))).AsQueryable();
            }
            model.Users = users.OrderBy(x => x.Id).Skip((pageNo.Value - 1) * pageSize).Take(pageSize).ToList();
            var roles = RoleManager.Roles.ToList();
            model.Roles = roles;
            ViewBag.Roles =  roles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return PartialView("_Listing",model);
        }
        [HttpGet]
        public async Task<ActionResult> Details(string Id)
        {
            var user = await UserManager.FindByIdAsync(Id);
            UserDetailsViewModel model = new UserDetailsViewModel();
            model.FashionClubUser = user;
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(FashionClubUser fashionClubUser)
        {
            JsonResult json = new JsonResult();
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(fashionClubUser.Id);
                user.FullName = fashionClubUser.FullName;
                user.PhoneNumber = fashionClubUser.PhoneNumber;
                user.ZipCode = fashionClubUser.ZipCode;
                user.Address = fashionClubUser.Address;

               var result= UserManager.Update(user);
            }


            json.Data =" " ;
            return json;
        }
        [HttpGet]
        public async Task<ActionResult> UserRoles(string Id,bool isDetails=false)
        {
            var user = await UserManager.FindByIdAsync(Id);
            var roleIDS = user.Roles.Select(x => x.RoleId).ToList();
            var roles = RoleManager.Roles.AsQueryable();

            UserRolesViewModel model = new UserRolesViewModel();
            
            model.Roles = roles.Where(x=>!roleIDS.Contains(x.Id));
            model.UserRoles= roles.Where(x => roleIDS.Contains(x.Id));
            model.UserID = Id;
            if (isDetails)
            {
                return PartialView("_UserRoleForDetails", model);
            }
            else
            {
                return PartialView("_UserRole",model);
            }

        }
        [HttpPost]
        public async Task<JsonResult> UserRoles(string userId, string roleId,string[] roles, bool isDelete = false)
        {
            IdentityResult result = null;
            JsonResult json = new JsonResult();

            if (roles != null && roles.Count() > 0)
            {
                result = await UserManager.AddToRolesAsync(userId, roles);
            }
            if (isDelete)
            {
                var role = await RoleManager.FindByIdAsync(roleId);
                result = await UserManager.RemoveFromRoleAsync(userId, role.Name);
            }
            if (!string.IsNullOrEmpty(roleId) && isDelete==false)
             {
                var role = await RoleManager.FindByIdAsync(roleId);
                result = await UserManager.AddToRoleAsync(userId, role.Name);
             }
            

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            return json;
        }
    }
}