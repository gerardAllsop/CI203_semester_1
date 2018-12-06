using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weeks_9_11
{
    public partial class viewit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "MusicStoreConn";

                string sql = "INSERT into Cart (CartID,AlbumID,Count,DateCreated) VALUES (@sessionID,@albumID,1,@date)";
                using (SqlConnection conn = getSqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@sessionID", HttpContext.Current.Session.SessionID);
                        cmd.Parameters.AddWithValue("@albumID", Session["albumID"]);
                        cmd.Parameters.AddWithValue("@date",DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
            }
            lblHcode.Text = Convert.ToString(Session["albumID"]);
        }

        protected SqlConnection getSqlConnection(string key)
        {
            return new SqlConnection(
                 ConfigurationManager.ConnectionStrings[key].ConnectionString);
        }
    }
}