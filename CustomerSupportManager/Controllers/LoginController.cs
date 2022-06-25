using CustomerSupportManager.Models;
using CustomerSupportManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerSupportManager.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        public ActionResult Index()
        {
            return Login();
        }
        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult AdminLogin()
        {
            return View("AdminLogin");
        }

        public string AttemptAdminLogin(AdminUserModel adminUserModel)
        {
            DAO dao = new DAO();
            Boolean success = dao.authenticateAdmin(adminUserModel);

            if (success)
            {
                return "Login Successful";
            }
            else
            {
                return "Login Failed";
            }
        }

        public string AttemptLogin(UserModel userModel)
        {
            DAO dao = new DAO();
            Boolean success = dao.authenticateUser(userModel);

            if (success)
            {
                return "Login Successful";
            }
            else
            {
                return "Login Failed";
            }
        }
    }
}