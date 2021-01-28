using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Entities
{
    public class ProductPicture
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int PictureID { get; set; }
        public Product Product { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
