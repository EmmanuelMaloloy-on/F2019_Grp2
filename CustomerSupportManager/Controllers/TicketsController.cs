using CustomerSupportManager.Models;
using CustomerSupportManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerSupportManager.Controllers
{
    public class TicketsController : Controller
    {
        public ActionResult Index()
        {
            //List<TicketModel> tickets = new List<TicketModel>();
            //return View(tickets);

            // get ticket list from DAO
            DAO dao = new DAO();

            List<TicketModel> tickets = new List<TicketModel>();

            tickets = dao.getTickets();

            return View("Index", tickets);
        }

        public ActionResult Details(int id)
        {
            DAO dao = new DAO();
            TicketModel ticket = dao.getTicket(id);

            return View("Details", ticket);
        }

        public ActionResult Create()
        {
            return View("TicketForm");
        }

        public ActionResult OpenNew()
        {
            TicketModel ticketModel = new TicketModel(-1, 2, " ", "New", " ");
            //DAO dao = new DAO();
            //int id = dao.createOrUpdateTicket(ticketModel);

            return View("OpenNew", ticketModel);
        }

        public ActionResult Edit(int id)
        {
            DAO dao = new DAO();
            TicketModel ticket = dao.getTicket(id);
            return View("TicketForm", ticket);
        }

        public ActionResult ProcessCreate(TicketModel ticketModel)
        {
            DAO dao = new DAO();
            dao.createOrUpdateTicket(ticketModel);
            return View("Details", ticketModel);

            //MessagerController messager = new MessagerController();
            //return messager.Index();
        }
    }
}