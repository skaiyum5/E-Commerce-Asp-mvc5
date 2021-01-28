using FashionClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionClub.Web.Areas.Dashboard.Models
{
    public class SizeActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SearchTerm { get; set; }
        public int? PageNo { get; set; }

    }
    public class SizeListingModel
    {
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
        public List<Size> Sizes { get; set; }

    }
}