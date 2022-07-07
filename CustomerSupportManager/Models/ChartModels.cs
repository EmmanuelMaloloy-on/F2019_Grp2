using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerSupportManager.Models
{
    public class StatusCountModel
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class CategoryCountModel
    {
        public string Category { get; set; }
        public int Count { get; set; }
    }
}