using InstituteProject.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstituteProject.Controllers.Student
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StudentLogin()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentLogin(string loginusername, string loginpassword)
        {
            if (loginusername != "" || loginpassword != "")
            {
                if (loginusername == "student" && loginpassword == "student")
                {
                    //return View("Index");
                    return RedirectToAction("Index", "Student");
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