using CustomerSupportManager.Models;
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

        public ActionResult CustomerMessager()
        {
            return View("CustomerMessager");
        }

        public ActionResult AdminMessager()
        {
            return View("AdminMessager");
        }

        public void ProcessCustomerMessage(TicketModel ticketModel)
        {

        }

        public void ProcessAdminMessage(TicketModel ticketModel)
        {

        }
    }
}