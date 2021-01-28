using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionClub.Web.Areas.Dashboard.Authorize.Models
{
    public class MenuViewModel
    {
        public string ProjectName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ReturnType { get; set; }
        public string Attributes { get; set; }
        public string Area { get; set; }
    }
}