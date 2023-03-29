using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstProductColorAssign.Models
{
    public class ColorVM
    {
        public Int64 ProductID { get; set; }
        public string ProductName { get; set; }
        public Int64 ColorCount { get; set; }

    }
}