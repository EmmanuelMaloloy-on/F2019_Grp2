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

        public MessageModel()
        {
            TicketId = 0;
            Message = "";
        }

        public MessageModel(int ticketId, string message)
        {
            TicketId = ticketId;
            Message = message;
        }
    }

}