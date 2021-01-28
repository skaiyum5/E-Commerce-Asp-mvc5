using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Entities
{
    public class Color  
    {
        public int ID { get; set; }
        public int Argb { get; set; }
        //public System.Drawing.Color Color { get { return new System.Drawing.Color.FromArgb(this.Argb); } set {
        //        if (value != null)
        //            this.Argb = value.ToArgb();
        //        else
        //            this.Argb = 0;
        //    }
        //}
    }
}
