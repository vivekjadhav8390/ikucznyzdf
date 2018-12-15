using InstituteProject.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstituteProject.Controllers.Center
{
    public class CenterController : Controller
    {
        // GET: Center
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CenterLogin()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CenterLogin(string loginusername, string loginpassword)
        {
            if (loginusername != "" || loginpassword != "")
            {
                if (loginusername == "center" && loginpassword == "center")
                {
                    return RedirectToAction("Index", "Center");
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