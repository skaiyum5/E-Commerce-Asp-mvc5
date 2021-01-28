using FashionClub.Web.Areas.Dashboard.Controllers;
using System.Web;
using System.Web.Mvc;

namespace FashionClub.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new DynamicRoleAuthorizeAttribute());
        }
    }
}
