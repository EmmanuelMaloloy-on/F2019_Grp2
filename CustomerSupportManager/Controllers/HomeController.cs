using CustomerSupportManager.Models;
using CustomerSupportManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerSupportManager.Controllers
{
    [Authorize(Roles = "Admin, Technical, Sales")]

    public class HomeController : Controller
    {

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Technical") || User.IsInRole("Sales"))
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return RedirectToAction("Index", "Tickets");
            }
        }

        public ActionResult Dashboard()
        {
            DAO dao = new Services.DAO();
            ViewBag.NewTicketsCount = dao.getTicketCountByStatus()[0].Count;
            ViewBag.UnresolvedTicketsCount = dao.getTicketCountByStatus()[1].Count;

            return View();
        }

        //[Authorize(Roles = "Admin, Technical, Sales")]
        public ActionResult StatusCountChart()
        {
            DAO dao = new DAO();
            List<StatusCountModel> statusCounts = new List<StatusCountModel>();
            statusCounts = dao.getTicketCountByStatus();

            return View(statusCounts);
        }

        public ActionResult CategoryCountChart()
        {
            DAO dao = new DAO();
            List<CategoryCountModel> categoryCounts = new List<CategoryCountModel>();
            categoryCounts = dao.getTicketCountByCategory();

            return View(categoryCounts);
        }

        public ActionResult StatusCountPieChart()
        {
            DAO dao = new DAO();
            List<StatusCountModel> statusCounts = new List<StatusCountModel>();
            statusCounts = dao.getTicketCountByStatus();

            return View(statusCounts);
        }

        public ActionResult CategoryCountPieChart()
        {
            DAO dao = new DAO();
            List<CategoryCountModel> categoryCounts = new List<CategoryCountModel>();
            categoryCounts = dao.getTicketCountByCategory();

            return View(categoryCounts);
        }

        [Authorize(Roles = "Admin, Technical, Sales")]
        public ActionResult Users()
        {
            DAO dao = new DAO();

            List<UserModel> users = new List<UserModel>();
            users = dao.getUsers();
            return View(users);
        }

        [Authorize(Roles = "Admin, Technical, Sales")]
        public ActionResult Customers()
        {
            DAO dao = new DAO();

            List<UserModel> users = new List<UserModel>();
            users = dao.getCustomers();
            return View(users);
        }
    }
}