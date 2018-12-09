using InstituteProject.App_Code;
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
                if (loginusername == "admin" && loginpassword == "admin")
                {
                    //return View("Index");
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

    }
}