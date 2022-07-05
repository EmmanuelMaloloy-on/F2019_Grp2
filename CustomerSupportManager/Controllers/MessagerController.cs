using CustomerSupportManager.Models;
using CustomerSupportManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerSupportManager.Controllers
{
    [Authorize]

    public class MessagerController : Controller
    {
        // GET: UserTicket
        public ActionResult Index(int ticketId)
        {
            // if admin show only admin messager button
            return CustomerMessager(ticketId);
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

        [Authorize(Roles = "Admin, Technical, Sales")]
        public ActionResult AdminMessager(int ticketId)
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

            return View("AdminMessager", messageModel);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult ProcessCustomerMessage(MessageModel messageModel)
        {
            DAO dao = new DAO();
            string message = messageModel.Message;
            int ticketId = messageModel.TicketId;
            dao.addMessage(ticketId, message);

            return RedirectToAction("CustomerMessager", new { ticketId });
        }

        public ActionResult ProcessAdminMessage(MessageModel messageModel)
        {
            DAO dao = new DAO();
            string message = messageModel.Message;
            int ticketId = messageModel.TicketId;
            dao.addMessage(ticketId, message);

            return RedirectToAction("AdminMessager", new { ticketId });
        }
    }
};