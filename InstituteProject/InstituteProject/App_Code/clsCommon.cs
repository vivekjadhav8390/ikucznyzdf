using InstituteProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InstituteProject.App_Code
{
    public class clsCommon
    {
        public static string InvalidLoginError = "Invalid username and password.";
        public static string BlankLoginError = "Please enter credentials.";
        public static string NotMatchingPassword = "Not mathcing confirm password.";
        public static string PleaseEnterPassword = "Please enter password";


        public enum enmSessions
        {
            SessionLoggedInUser
        }
        #region Validations
        public static bool HasValue(string value)
        {
            if (value.ToString().Trim().Length > 0)
            {
                return true;
            }
            return false;
        }
        public static bool IsNotEmptyDataSet(DataSet ds)
        {
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsNotEmptyDataTable(DataTable dt)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion Validations
        public UserModel UserLogin(string username, string password)
        {
            UserModel u = null;
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "Institute_UserLogin";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@UserName", username);
            sqlcommand.Parameters.AddWithValue("@Password", password);

            DataSet ds = clsDAL.GetDataSet(sqlcommand);
            if (IsNotEmptyDataSet(ds))
            {
                if (IsNotEmptyDataTable(ds.Tables[0]))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        u = new UserModel();
                        u.ID = Convert.ToInt16(dr["ID"].ToString());
                        u.UserName = dr["UserName"].ToString();
                        u.EmailAddress = dr["EmailAddress"].ToString();
                        u.UserType = Convert.ToInt16(dr["UserType"].ToString());
                        u.UserTypeDesc = dr["UserTypeDesc"].ToString();
                        if (dr["IsActive"] != DBNull.Value)
                        {
                            if (HasValue(dr["IsActive"].ToString()))
                            {
                                u.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                            }
                        }
                    }
                }
            }
            return u;
        }

        public UserModel ChangePassword(UserModel objuser, string newpassword)
        {
            UserModel u = null;
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.CommandText = "Institute_UserLogin";
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@UserID", objuser.ID);
            sqlcommand.Parameters.AddWithValue("@NewPassword", newpassword);

            DataSet ds = clsDAL.GetDataSet(sqlcommand);
            if (IsNotEmptyDataSet(ds))
            {
                if (IsNotEmptyDataTable(ds.Tables[0]))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        u = new UserModel();
                        u.ID = Convert.ToInt16(dr["ID"].ToString());
                        u.UserName = dr["UserName"].ToString();
                        u.EmailAddress = dr["EmailAddress"].ToString();
                        u.UserType = Convert.ToInt16(dr["UserType"].ToString());
                        u.UserTypeDesc = dr["UserTypeDesc"].ToString();
                        if (dr["IsActive"] != DBNull.Value)
                        {
                            if (HasValue(dr["IsActive"].ToString()))
                            {
                                u.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                            }
                        }
                    }
                }
            }
            return u;
        }
    }

}