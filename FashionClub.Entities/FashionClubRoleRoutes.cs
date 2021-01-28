using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FashionClub.Entities
{
    public class FashionClubRoleRoutes
    {
        public int ID { get; set; }
       // public virtual String FashionClubRole_Id { get; set; }
        public int RouteInfoID { get; set; }
        public virtual FashionClubRole FashionClubRole { get; set; }
        public virtual RouteInfo RouteInfo { get; set; }
    }
}
