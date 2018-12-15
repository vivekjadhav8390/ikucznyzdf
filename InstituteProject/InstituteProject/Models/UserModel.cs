using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstituteProject.Models
{
    public class UserModel
    {
        public int ID;
        public string UserName;
        public string EmailAddress;
        public int UserType;
        public string UserTypeDesc;
        public bool IsActive;
    }
}