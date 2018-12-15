using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InstituteProject.App_Code
{
    public class clsDAL
    {
        // Default connection string. a connection named MsSql should be defined in web.config file.
        public const string CONNECTION_STRING_NAME = "InstituteConnection";

        //This returns the connection string  
        private static string _connectionString = string.Empty;
        public static string ConnectionString
        {
            get
            {
                if (_connectionString == string.Empty)
                {
                    _connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
                }
                return _connectionString;
            }
        }
        public static DataSet GetDataSet(SqlCommand command)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet dts = new DataSet();
            try
            {
                command.Connection = conn;
                da.SelectCommand = command;
                conn.Open();
                da.Fill(dts);
                return dts;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                da.Dispose();
                conn.Dispose();
                command.Dispose();
            }
        }
        /// <summary>
        /// Datatable Döndür
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DataTable Execute(SqlCommand command)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            DataTable dt = new DataTable();
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                dt.Load(command.ExecuteReader());
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                command.Dispose();
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlCommand command)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            DataTable dt = new DataTable();
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                conn.Open();
                int result = command.ExecuteNonQuery();
                return result;
            }
            catch
            {
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                command.Dispose();
            }
            
        }

    }
}