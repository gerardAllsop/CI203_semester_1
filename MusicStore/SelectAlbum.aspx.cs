using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicStore
{
    public partial class SelectAlbum : System.Web.UI.Page
    {
        private string ddlArtistSql,ddlGenreSql, grvSql, ddlOnChangeStr;
        private string connectionKey = "MusicStoreConn";
        private SqlParameter param;

        protected void Page_Load(object sender, EventArgs e)
        {
            //init sql strings
            ddlArtistSql = "SELECT ArtistID, Name FROM Artist ORDER BY Name";
            ddlGenreSql = "SELECT GenreID, Name FROM Genre ORDER BY Name";
            grvSql = "SELECT AlbumID, GenreID,ArtistID,Title, Price FROM Album";
            ddlOnChangeStr = "SELECT AlbumID, GenreID,ArtistID,Title, Price FROM Album WHERE ArtistID = @Artist_code";

            if (!IsPostBack)
            {
                //prepare webForm
                BindDdl(ddlArtist,ddlArtistSql,"ArtistID","Name");
                BindGridView(grvSql);
            }
        }

        protected SqlConnection getSqlConnection(string key)
        {
            return new SqlConnection(
                 ConfigurationManager.ConnectionStrings[key].ConnectionString);
        }

        protected void BindDdl(DropDownList ddl, string sqlStr,string value,string text)
        {
            DataTable dtb_ = new DataTable();
            using (SqlConnection conn = getSqlConnection(connectionKey))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                adapter.Fill(dtb_);
                ddl.DataSource = dtb_;
                ddl.DataTextField = text;
                ddl.DataValueField = value;
                ddl.DataBind();
            }
        }

        protected void BindGridView(string sqlString)
        {
            DataTable dtb_album = new DataTable();
            using (SqlConnection conn = getSqlConnection(connectionKey))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlString, conn);
                if (IsPostBack)
                {          
                    adapter.SelectCommand.Parameters.Add(param);
                }

                adapter.Fill(dtb_album);
                gvAlbums.DataSource = dtb_album;
                gvAlbums.DataBind();
            }
        }


        protected void ddl_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            param = new SqlParameter();
            param.ParameterName = "@Artist_code";
            param.Value = ddlArtist.SelectedValue.ToString();
            BindGridView(ddlOnChangeStr);
        }

        protected void gvAlbums_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvAlbums.SelectedRow;
            Session["albumID"] = row.Cells[1].Text;
            Response.Redirect("viewit.aspx");
        }
    }
}