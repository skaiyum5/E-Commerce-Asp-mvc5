using FashionClub.Web.Areas.Dashboard.Authorize.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FashionClub.Web.Areas.Dashboard.Authorize.Services
{
    public class DynamicRoleServices
    {
        public List<MenuViewModel> GetMenu()
        {
            var projectName = Assembly.GetExecutingAssembly().FullName.Split(',')[0];

            Assembly asm = Assembly.GetAssembly(typeof(MvcApplication));

            var model = asm.GetTypes().
                SelectMany(t => t.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(d => d.ReturnType.Name.Contains("ActionResult")).Select(n => new MenuViewModel()
                {
                    Controller = n.DeclaringType?.Name.Replace("Controller", ""),
                    Action = n.Name,
                    ReturnType = n.ReturnType.Name,
                    //Attributes = string.Join(",", n.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))),
                    Attributes = n.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")).FirstOrDefault(),
                    Area = n.DeclaringType.Namespace.ToString().Replace(projectName + ".", "").Replace("Areas.", "").Replace(".Controllers", "").Replace("Controllers", "")
                });
            return model.ToList();
        }
        
        
    }
}