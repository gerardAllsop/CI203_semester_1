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
        private string ddlArtistSql,chbGenreSql, grvSql, OnChangeGenreStr,OnChangeArtistStr;
        private string connectionKey = "MusicStoreConn";
        private SqlParameter param;


        protected void Page_Load(object sender, EventArgs e)
        {
            //init sql strings
            ddlArtistSql = "SELECT ArtistID, Name FROM Artist ORDER BY Name";
            chbGenreSql = "SELECT GenreID, Name FROM Genre ORDER BY Name";
            grvSql = "SELECT AlbumID, GenreID,ArtistID,Title, Price FROM Album";
            OnChangeGenreStr = "SELECT AlbumID, GenreID,ArtistID,Title, Price FROM Album WHERE GenreID = @Genre_code";
            OnChangeArtistStr = "SELECT AlbumID, GenreID,ArtistID,Title, Price FROM Album WHERE ArtistID = @Artist_code";

            if (!IsPostBack)
            {
                //prepare webForm
                BindDdl(ddlArtist,ddlArtistSql);
                BindCheckBox(chbGenreSql);
                BindGridView(grvSql);
            }
        }

        protected SqlConnection getSqlConnection(string key)
        {
            return new SqlConnection(
                 ConfigurationManager.ConnectionStrings[key].ConnectionString);
        }

        protected void BindDdl(DropDownList ddl, string sqlStr)
        {
            DataTable dtb_ = new DataTable();
            using (SqlConnection conn = getSqlConnection(connectionKey))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                adapter.Fill(dtb_);
                ddl.DataSource = dtb_;
                ddl.DataTextField = "Name";
                ddl.DataValueField = "ArtistID";
                ddl.DataBind();
            }
        }

        protected void BindCheckBox(string sqlStr)
        {
            using (SqlConnection conn = getSqlConnection(connectionKey))
            {
                DataTable dtb_ = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                adapter.Fill(dtb_);
                foreach (DataRow dr in dtb_.Rows)
                {
                    ListItem newItem = new ListItem(dr["Name"].ToString(), dr["GenreID"].ToString());
                    rbGenre.Items.Add(newItem);
                }
               
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

        protected void rbChanged(object sender, EventArgs e)
        {
            param = new SqlParameter();
            param.ParameterName = "@Genre_code";
            param.Value = rbGenre.SelectedItem.Value.ToString();
           
            BindGridView(OnChangeGenreStr);
        }
            

        protected void ddl_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            param = new SqlParameter();
            param.ParameterName = "@Artist_code";
            param.Value = ddlArtist.SelectedValue.ToString();

            BindGridView(OnChangeArtistStr);
        }

        protected void gvAlbums_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvAlbums.SelectedRow;
            Session["albumID"] = row.Cells[1].Text;
            Response.Redirect("viewit.aspx");
        }
    }
}