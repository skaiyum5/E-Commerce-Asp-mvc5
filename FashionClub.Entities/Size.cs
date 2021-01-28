using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Entities
{
    public class Size:BaseEntity
    {
        public virtual List<Product> Products { get; set; }
    }
}
