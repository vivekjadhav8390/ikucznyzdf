using InstituteProject.App_Code;
using InstituteProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public ActionResult Register(string UserName, string Password, string Email, string InstituteName, string Address, string InstituteorSchool, string ContactPerson,
          string NumberOfSeats)
        {
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "Institute_RegisterCenter";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@UserName", UserName);
            sqlcommand.Parameters.AddWithValue("@Email", Email);
            sqlcommand.Parameters.AddWithValue("@Password", Password);
            sqlcommand.Parameters.AddWithValue("@InstituteName", InstituteName);
            sqlcommand.Parameters.AddWithValue("@ContactOfPerson", ContactPerson);
            sqlcommand.Parameters.AddWithValue("@ContactNo", "");
            sqlcommand.Parameters.AddWithValue("@OfficeNo", "");
            sqlcommand.Parameters.AddWithValue("@Address", Address);
            sqlcommand.Parameters.AddWithValue("@InstituteOrSchool", InstituteorSchool);
            sqlcommand.Parameters.AddWithValue("@NoOfSeats", NumberOfSeats);
            DataSet ds = clsDAL.GetDataSet(sqlcommand);
            if (clsCommon.IsNotEmptyDataSet(ds))
            {
                if (clsCommon.IsNotEmptyDataTable(ds.Tables[0]))
                {
                    ViewBag.Error = "Registration Successful.";
                }
                else
                {
                    if (clsCommon.IsNotEmptyDataTable(ds.Tables[1]))
                    {
                        ViewBag.Error = ds.Tables[1].Rows[0]["Error"].ToString();
                    }
                    else
                    {
                        ViewBag.Error = "Registration Failed.";
                    }
                }
            }
            else
            {
                ViewBag.Error = "Registration Failed.";
            }
            return View();
        }

        [HttpPost]
        public ActionResult CenterLogin(string loginusername, string loginpassword)
        {
            if (loginusername != "" || loginpassword != "")
            {
                clsCommon comm = new clsCommon();
                UserModel user = comm.UserLogin(loginusername, loginpassword);
                if (user != null)
                {
                    Session[clsCommon.enmSessions.SessionLoggedInUser.ToString()] = user;
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
        public ActionResult ChangePassword()
        {
            return RedirectToAction("Changepassword", "Center");
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
                        return RedirectToAction("Index", "Center");
                    }
                    else
                    {
                        ViewBag.Error = clsCommon.InvalidLoginError;
                        return View("CenterLogin", "Center");
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