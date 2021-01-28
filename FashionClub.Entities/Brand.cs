using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Entities
{
    public class Brand :BaseEntity
    {
        public bool Featured { get; set; }
        public int? PictureID { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual List<Product> Products { get; set; }
        

    }
}
