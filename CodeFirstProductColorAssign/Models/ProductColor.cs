using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstProductColorAssign.Models
{
    public class ProductColor
    {
        public Int64 ProductColorID { get; set; }
        public Int64 ProductID { get; set; }
        public Int64 ColorID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Color Color { get; set; }
    }
}