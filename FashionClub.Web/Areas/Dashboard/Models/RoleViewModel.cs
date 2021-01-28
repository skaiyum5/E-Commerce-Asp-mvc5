using FashionClub.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionClub.Web.Areas.Dashboard.Models
{
    public class RoleActionModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PageNO { get; set; }
        public List<RouteInfo> RouteInfos { get; set; }
        public List<string> Areas { get; set; }
        public int[] Selected { get; set; }
        public virtual List<RouteInfo> FashionClubRoleRouteInfos { get; set; }
    }
    public class RoleListingModel
    {
        public string UserID { get; set; }
        public IEnumerable<FashionClubUser> Users { get; set; }
        public string SearchTerm { get; set; }
        //public virtual IQueryable<FashionClubRole> Roles { get; set; }
        public virtual List<FashionClubRole> Roles { get; set; }
        public string RoleID { get; set; }
        public Pager Pager { get; set; }
        public int UsersCount { get; set; }
    }
    public class RoleDetailsModel
    {
        public string UserID { get; set; }
        public IQueryable<FashionClubUser> Users { get; set; }
        public virtual FashionClubRole Role { get; set; }
        public string RoleID { get; set; }
    }
}