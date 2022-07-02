using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerSupportManager.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        //public string Product { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        //public List<string> Messages { get; set; } = new List<string>();

        public TicketModel()
        {
            Id = -1;
            CustomerId = -1;
            Category = "";
            Status = "";
        }

        public TicketModel(int id, int customerId, string category, string status)
        {
            Id = id;
            CustomerId = customerId;
            Category = category;
            Status = status;
        }
    }

}