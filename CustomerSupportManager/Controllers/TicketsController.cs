using CustomerSupportManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerSupportManager.Controllers
{
    public class TicketsController : Controller
    {
        // GET: Tickets
        public ActionResult Index()
        {
            List<TicketModel> tickets = new List<TicketModel>();
            return View(tickets);
        }
    }
}