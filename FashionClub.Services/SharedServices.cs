using FashionClub.Data;
using FashionClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace FashionClub.Services
{
    public class SharedServices
    {
        #region singleTon

        public static SharedServices Instance
        {
            get
            {
                if (instance == null) instance = new SharedServices();
                {
                    return instance;
                }

            }
        }

        private static SharedServices instance { get; set; }

        public SharedServices()
        {

        }

        #endregion
        public bool SavePicture(List<Picture> pictures)
        {
            using(FashionClubDBContext context=new FashionClubDBContext())
            {
                context.Pictures.AddRange(pictures);
                return context.SaveChanges()>0;
            }
        }

        public static string GetActionRoles(string action, string controller)
        {
            XElement rootElement = XElement.Load(HttpContext.Current.Server.MapPath("/") + "ActionRoles.xml");
            XElement controllerElement = findElementByAttribute(rootElement, "Controller", controller);
            if (controllerElement != null)
            {
                XElement actionElement = findElementByAttribute(controllerElement, "Action", action);
                if (actionElement != null)
                {
                    return actionElement.Value;
                }
            }
            return "";
        }
        public string[] GetRoleByContrller_Action(string action, string controller,string attribute, string url)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                string[] rolesName= { };
                var routes = context.RouteInfos.AsQueryable();
                if (routes!=null)// && routes.Count()>0
                {
                    var routeInfoId = routes.Where(x => x.Url.Contains(url) && x.Attributes.ToLower().Contains(attribute.ToLower())).Select(x => x.ID).FirstOrDefault();
                    var roles = context.FashionClubRoles.Where(x => x.RouteInfos.Any(y => y.ID == routeInfoId)).AsQueryable();
                    rolesName = roles.Select(x => x.Name).ToArray();
                    //var routeId = routes.Where(x => x.Url.Contains(url) && x.Attributes.ToLower().Contains(attribute.ToLower())).Select(x => x.ID).FirstOrDefault();
                    //var roles = context.FashionClubRoleRoutes.Where(x => x.RouteInfoID == routeId).Select(y => y.FashionClubRole).ToList();//Where(x => x.RouteInfoID == routeId).Select(y => y.).ToList();
                    //rolesName = roles.Select(x => x.Name).ToArray();

                }

                return rolesName;
            }
        }

        public static XElement findElementByAttribute(XElement xElement, string tagName, string attribute)
        {
            return xElement.Elements(tagName).FirstOrDefault(x => x.Attribute("name").Value.Equals(attribute, StringComparison.OrdinalIgnoreCase));
        }

    }
}
