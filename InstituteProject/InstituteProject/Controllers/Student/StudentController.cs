using InstituteProject.App_Code;
using InstituteProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public ActionResult Register(string UserName, string Password, string Email, string FullName, string Address, string CollegeName, string ContactNo,
          string PerContactNo,string DateOfBirth)
        {
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "Institute_RegisterStudent";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@UserName", UserName);
            sqlcommand.Parameters.AddWithValue("@Email", Email);
            sqlcommand.Parameters.AddWithValue("@Password", Password);
            sqlcommand.Parameters.AddWithValue("@FullName", FullName);
            sqlcommand.Parameters.AddWithValue("@CollegeName", CollegeName);
            sqlcommand.Parameters.AddWithValue("@ContactNo", ContactNo);
            sqlcommand.Parameters.AddWithValue("@PerContactNo", PerContactNo);
            sqlcommand.Parameters.AddWithValue("@Address", Address);
            sqlcommand.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(DateOfBirth));
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
        public ActionResult ChangePassword()
        {
            return RedirectToAction("Changepassword", "Student");
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
                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        ViewBag.Error = clsCommon.InvalidLoginError;
                        return View("StudentLogin", "Student");
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