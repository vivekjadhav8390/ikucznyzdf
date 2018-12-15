using InstituteProject.App_Code;
using InstituteProject.Models;
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

        [HttpPost]
        public ActionResult StudentLogin(string loginusername, string loginpassword)
        {
            if (loginusername != "" || loginpassword != "")
            {
                clsCommon comm = new clsCommon();
                UserModel user = comm.UserLogin(loginusername, loginpassword);
                if (user != null)
                {
                    Session[clsCommon.enmSessions.SessionLoggedInUser.ToString()] = user;
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