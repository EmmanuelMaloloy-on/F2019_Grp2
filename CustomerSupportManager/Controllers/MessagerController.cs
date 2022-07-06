using CustomerSupportManager.Models;
using CustomerSupportManager.Services;
using Microsoft.AspNet.Identity;
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
                ViewBag.TicketTitle = getTicketTitle(ticketId);
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

            string userId = User.Identity.GetUserId();
            messageModel.UserId = userId;

            return View("CustomerMessager", messageModel);
        }

        [Authorize(Roles = "Admin, Technical, Sales")]
        public ActionResult AdminMessager(int ticketId)
        {

            DAO dao = new DAO();
            if (ticketId > 0)
            {
                ViewBag.Messages = dao.getMessages(ticketId);
                ViewBag.TicketTitle = getTicketTitle(ticketId);
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

            string userId = User.Identity.GetUserId();
            messageModel.UserId = userId;

            return View("AdminMessager", messageModel);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult ProcessCustomerMessage(MessageModel messageModel)
        {
            DAO dao = new DAO();
            dao.addMessage(messageModel);

            int ticketId = messageModel.TicketId;

            return RedirectToAction("CustomerMessager", new { ticketId });
        }

        public ActionResult ProcessAdminMessage(MessageModel messageModel)
        {
            DAO dao = new DAO();
            dao.addMessage(messageModel);

            int ticketId = messageModel.TicketId;

            return RedirectToAction("AdminMessager", new { ticketId });
        }

        public string getTicketTitle(int ticketId)
        {
            DAO dao = new DAO();
            TicketModel ticket = dao.getTicket(ticketId);
            return ticket.Title;
        }
    }
};