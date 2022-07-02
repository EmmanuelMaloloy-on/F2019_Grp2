using CustomerSupportManager.Models;
using CustomerSupportManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerSupportManager.Controllers
{
    public class MessagerController : Controller
    {
        // GET: UserTicket
        public ActionResult Index()
        {
            // if admin show only admin messager button
            return View();
        }

        public ActionResult CustomerMessager(int ticketId)
        {

            DAO dao = new DAO();
            if (ticketId > 0)
            {
                ViewBag.Messages = dao.getMessages(ticketId);
            }
            else
            {
                List<string> defaultmessage = new List<string>();
                defaultmessage.Add("No messages");
                ViewBag.Messages = defaultmessage;
            }

            //ViewBag.TicketId = ticketId;

            MessageModel messageModel = new MessageModel();
            messageModel.TicketId = ticketId;

            return View("CustomerMessager", messageModel);
        }

        public ActionResult AdminMessager(int ticketId)
        {
            return View("AdminMessager");
        }

        public ActionResult ProcessCustomerMessage(MessageModel messageModel)
        {
            DAO dao = new DAO();
            string message = messageModel.Message;
            int ticketId = messageModel.TicketId;
            dao.addMessage(ticketId, message);

            return CustomerMessager(ticketId);
        }

        public void ProcessAdminMessage(int ticketId)
        {

        }
    }
};