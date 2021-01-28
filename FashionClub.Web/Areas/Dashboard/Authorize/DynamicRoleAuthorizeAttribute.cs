using FashionClub.Data;
using FashionClub.Entities;
using FashionClub.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionClub.Web.Areas.Dashboard.Controllers
{
    public class DynamicRoleAuthorizeAttribute : AuthorizeAttribute
    {
        public new string[] Roles { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            if (Roles == null)
            {
                return true;
            }
            if (Roles.Length == 0)
            {
                return true;
            }
            if (Roles.Any(httpContext.User.IsInRole))
            {
                return true;
            }
            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string attribute = "Http"+ filterContext.HttpContext.Request.HttpMethod;//.ActionDescriptor.GetCustomAttributes.Select(a => a.GetType().Name.Replace("Attribute", "")).FirstOrDefault();
            string url = filterContext.HttpContext.Request.Path;
            this.Roles = SharedServices.Instance.GetRoleByContrller_Action(actionName, controllerName, attribute, url);// GetRoles.GetActionRoles(actionName, controllerName);
            //if (!string.IsNullOrWhiteSpace(roles))
            //{
            //    this.Roles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //}
            base.OnAuthorization(filterContext);
        }
    }
}