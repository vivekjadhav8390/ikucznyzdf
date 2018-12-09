using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstituteProject.App_Code
{
    public class clsCommon
    {
        public static string InvalidLoginError = "Invalid username and password.";
        public static string BlankLoginError = "Please enter credentials.";


        public enum enmSessions
        {
            UserName,
            UserType,
            UserID
        }
    }
    
}