using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerSupportManager.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public TicketModel()
        {
            Id = -1;
            CustomerId = "";
            Category = "";
            Status = "";
            Title = "";
            Date = DateTime.Now;
        }

        public TicketModel(int id, string customerId, string category, string status, string title, DateTime date)
        {
            Id = id;
            CustomerId = customerId;
            Category = category;
            Status = status;
            Title = title;
            Date = date;
        }
    }

}