using FashionClub.Data;
using FashionClub.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Services
{
    public class FashionClubRoleManager : RoleManager<FashionClubRole>
    {
        public FashionClubRoleManager(IRoleStore<FashionClubRole, string> store) : base(store)
        {
        }
        public static FashionClubRoleManager Create(IdentityFactoryOptions<FashionClubRoleManager> option, IOwinContext context)
        {
            return new FashionClubRoleManager(new RoleStore<FashionClubRole>(context.Get<FashionClubDBContext>()));
        }

        public string SaveRoutes(List<RouteInfo> routeInfos)
        {
            FashionClubDBContext context = new FashionClubDBContext();
            var routes = context.RouteInfos.AsEnumerable();
            var newRoutes = routes != null && routes.Count() > 0 ? routes.Where(x => !routeInfos.Any(y => x.Url == y.Url && x.Attributes == y.Attributes && y.Area == x.Area && y.Controller == x.Controller)) : routeInfos;

            if (newRoutes != null && newRoutes.Count() > 0)
            {

                context.RouteInfos.AddRange(newRoutes);
                var result = context.SaveChanges() > 0;
                if (result)
                {
                    return "Saved Successfully";
                }
                else
                {
                    return "Internal server error";
                }
            }
            else
            {
                return "Already Saved";
            }
        }
        public List<RouteInfo> GetRoutes()
        {
            FashionClubDBContext context = new FashionClubDBContext();
            return context.RouteInfos.ToList();

        }
        public List<RouteInfo> GetRoutesByIds(List<int> Ids)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                return Ids.Select(x => context.RouteInfos.Find(x)).ToList();
            };
        }

        public List<String> GetArea()
        {
            FashionClubDBContext context = new FashionClubDBContext();
            return context.RouteInfos.Select(y => y.Area).Distinct().ToList();

        }

        public bool Edit(FashionClubRole role, List<int> Ids)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                context.Entry(role).State = EntityState.Modified;
                context.Entry(role).Collection(x => x.RouteInfos).Load();
                var routeInfos = Ids.Select(x => context.RouteInfos.Find(x)).ToList();
                role.RouteInfos= routeInfos;
                return context.SaveChanges() > 0;
            }
        }



    }
}
