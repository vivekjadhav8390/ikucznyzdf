using InstituteProject.App_Code;
using InstituteProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstituteProject.Controllers.Admin
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(string loginusername, string loginpassword)
        {
            if (loginusername != "" || loginpassword != "")
            {
                clsCommon comm = new clsCommon();
                UserModel user = comm.UserLogin(loginusername, loginpassword);
                if (user != null)
                {
                    Session[clsCommon.enmSessions.SessionLoggedInUser.ToString()] = user;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Error = clsCommon.InvalidLoginError;
                    return View();
                }
            }
            else
            {
                ViewBag.Error = clsCommon.BlankLoginError;
                return View();
            }
        }
        public ActionResult ChangePassword()
        {
            return RedirectToAction("Changepassword", "Admin");
        }
        [HttpPost]
        public ActionResult ChangePassword(string newpassword, string confirmpassword)
        {
            if (newpassword != "" || confirmpassword != "")
            {
                if (newpassword != confirmpassword)
                {
                    ViewBag.Error = clsCommon.NotMatchingPassword;
                    return View();
                }
                else
                {
                    UserModel u = (UserModel)Session[clsCommon.enmSessions.SessionLoggedInUser.ToString()];
                    clsCommon comm = new clsCommon();
                    UserModel user = comm.ChangePassword(u, newpassword);
                    if (user != null)
                    {
                        Session[clsCommon.enmSessions.SessionLoggedInUser.ToString()] = user;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ViewBag.Error = clsCommon.InvalidLoginError;
                        return View("StudentLogin", "Admin");
                    }
                }
            }
            else
            {
                ViewBag.Error = clsCommon.PleaseEnterPassword;
                return View();
            }
        }

    }
}