using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerSupportManager.Models
{
    public class MessageModel
    {
        public int TicketId { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }

        public MessageModel()
        {
            TicketId = 0;
            Message = "";
            UserId = "";
        }

        public MessageModel(int ticketId, string message, string userId)
        {
            TicketId = ticketId;
            Message = message;
            UserId = userId;
        }
    }

}