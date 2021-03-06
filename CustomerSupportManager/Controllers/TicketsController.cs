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

    public class TicketsController : Controller
    {
        public ActionResult Index()
        {
            //List<TicketModel> tickets = new List<TicketModel>();
            //return View(tickets);

            // get ticket list from DAO
            DAO dao = new DAO();

            List<TicketModel> tickets = new List<TicketModel>();
            if (User.IsInRole("Admin"))
            {
                tickets = dao.getTickets();

                return View("Index", tickets);
            }
            else if (User.IsInRole("Technical"))
            {
                tickets = dao.getTicketsByCategory("Technical");

                return View("Index", tickets);
            }
            else if (User.IsInRole("Sales"))
            {
                tickets = dao.getTicketsByCategory("Sales");

                return View("Index", tickets);
            }
            else if (User.IsInRole("Customer"))
            {
                string userId = User.Identity.GetUserId();
                //int userIdInt = Int32.Parse(userId);
                tickets = dao.getTicketsByCustomerId(userId);

                return View("CustomerIndex", tickets);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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

        [Authorize(Roles = "Admin, Customer")]
        public ActionResult OpenNew()
        {
            string userId = User.Identity.GetUserId();
            TicketModel ticketModel = new TicketModel(-1, userId, "", "New", "", DateTime.Now);
            //DAO dao = new DAO();
            //int id = dao.createOrUpdateTicket(ticketModel);

            if (User.IsInRole("Admin"))
            {
                return View("OpenNew", ticketModel);
            }
            else
            {
                return View("CustomerOpenNew", ticketModel);
            }
        }

        public ActionResult Edit(int id)
        {
            DAO dao = new DAO();
            TicketModel ticket = dao.getTicket(id);
            return View("TicketForm", ticket);

        }

        public ActionResult Delete(int id)
        {
            DAO dao = new DAO();
            dao.deleteTicket(id);
           return RedirectToAction("Index");

        }

        public ActionResult ProcessCreate(TicketModel ticketModel)
        {
            DAO dao = new DAO();
            int Id = dao.createOrUpdateTicket(ticketModel);
            //return View("Details", ticketModel);

            //MessagerController messager = new MessagerController();
            //return messager.Index();

            if (User.IsInRole("Customer"))
            {
            return RedirectToAction("CustomerMessager", "Messager", new { ticketId = Id });
            }
            else
            {
            return RedirectToAction("AdminMessager", "Messager", new { ticketId = Id });
            }
        }

        public ActionResult SearchTitle(string searchPhrase)
        {
            DAO dao = new DAO();

            List<TicketModel> searchResults = dao.searchForTicket(searchPhrase);

            return View("Index", searchResults);
        }

        public ActionResult Unresolved()
        {
            DAO dao = new DAO();
            List<TicketModel> tickets = new List<TicketModel>();

            if (User.IsInRole("Admin"))
            {
                tickets = dao.getTicketsByStatus("Unresolved", "%");
            }
            else
            {
                string userId = User.Identity.GetUserId();
                string currentrole = dao.getRoleById(userId);

                tickets = dao.getTicketsByStatus("Unresolved", currentrole);
            }

            return View("Index", tickets);
        }

        public ActionResult Solved()
        {
            DAO dao = new DAO();
            List<TicketModel> tickets = new List<TicketModel>();

            if (User.IsInRole("Admin") || User.IsInRole("Customer"))
            {
                tickets = dao.getTicketsByStatus("Solved", "%");
            }
            else
            {
                string userId = User.Identity.GetUserId();
                string currentrole = dao.getRoleById(userId);

                tickets = dao.getTicketsByStatus("Solved", currentrole);
            }

            return View("Index", tickets);
        }

        public ActionResult Error()
        {
            DAO dao = new DAO();
            List<TicketModel> tickets = new List<TicketModel>();

            if (User.IsInRole("Admin") || User.IsInRole("Customer"))
            {
                tickets = dao.getTicketsByStatus("Error", "%");
            }
            else
            {
                string userId = User.Identity.GetUserId();
                string currentrole = dao.getRoleById(userId);

                tickets = dao.getTicketsByStatus("Error", currentrole);
            }

            return View("Index", tickets);
        }

        public ActionResult New()
        {
            DAO dao = new DAO();
            List<TicketModel> tickets = new List<TicketModel>();

            if (User.IsInRole("Admin") || User.IsInRole("Customer"))
            {
                tickets = dao.getTicketsByStatus("New", "%");
            }
            else
            {
                string userId = User.Identity.GetUserId();
                string currentrole = dao.getRoleById(userId);

                tickets = dao.getTicketsByStatus("New", currentrole);
            }

            return View("Index", tickets);
        }

        public ActionResult Technical()
        {
            DAO dao = new DAO();

            List<TicketModel> tickets = dao.getTicketsByCategory("Technical");

            return View("Index", tickets);
        }

        public ActionResult Sales()
        {
            DAO dao = new DAO();

            List<TicketModel> tickets = dao.getTicketsByCategory("Sales");

            return View("Index", tickets);
        }

        public ActionResult Inquiry()
        {
            DAO dao = new DAO();

            List<TicketModel> tickets = dao.getTicketsByCategory("Inquiry");

            return View("Index", tickets);
        }
    }
}