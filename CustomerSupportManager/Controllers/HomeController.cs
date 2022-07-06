using CustomerSupportManager.Models;
using CustomerSupportManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerSupportManager.Controllers
{
    
    public class HomeController : Controller
    {
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

        [Authorize(Roles = "Admin, Technical, Sales")]
        public ActionResult Dashboard()
        {
            return View();
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
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}