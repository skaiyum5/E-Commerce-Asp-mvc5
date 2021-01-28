using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Entities
{
    public class FashionClubRole:IdentityRole
    {
        public FashionClubRole(string name):base(name)
        {

        }
        public FashionClubRole()
        {

        }
        public string Description { get; set; }
        public virtual List<RouteInfo> RouteInfos { get; set; }

    }
}
