using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstProductColorAssign.Models
{
    public class Color
    {
        public Int64 ColorID { get; set; }
        public string ColorName { get; set; }
        public virtual List<ProductColor>ProductColors { get; set; }
    }
}