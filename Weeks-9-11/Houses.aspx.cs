using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weeks_9_11
{
    public partial class Houses : System.Web.UI.Page
    {
        private string ddlSql, grvSql, ddlAreaOnChangeStr;
        private string connectionKey = "estagentConn";
        private SqlParameter param;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //init sql strings
            ddlSql = "SELECT area_code, area_descr FROM t_area ORDER BY area_descr";
            grvSql = "SELECT houseid, hadd1,hadd2,hadd3 FROM t_house";
            ddlAreaOnChangeStr = "SELECT houseID, hadd1, hadd2, hadd3 FROM t_house WHERE harea_code = @Area_code";

            if (!IsPostBack)
            {     
                //prepare webForm
                BindDdlArea(ddlSql);
                BindGridView(grvSql);
            }
        }

        protected SqlConnection getSqlConnection(string key)
        {
            return new SqlConnection(
                 ConfigurationManager.ConnectionStrings[key].ConnectionString);
        }

        protected void BindDdlArea(string sqlStr)
        {
            DataTable dtb_area = new DataTable();
            using (SqlConnection conn = getSqlConnection(connectionKey))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(ddlSql, conn);
                adapter.Fill(dtb_area);
                ddlArea.DataSource = dtb_area;
                ddlArea.DataTextField = "area_descr";
                ddlArea.DataValueField = "area_code";
                ddlArea.DataBind();
            }
        }

        protected void BindGridView(string sqlString)
        {
            DataTable dtb_house = new DataTable();
            using (SqlConnection conn = getSqlConnection(connectionKey))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlString, conn);
                if (IsPostBack)
                {
                    adapter.SelectCommand.Parameters.Add(param);
                }  
                adapter.Fill(dtb_house);
                gvHouses.DataSource = dtb_house;
                gvHouses.DataBind();
            }
        }

        protected void ddlArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
            using (SqlConnection conn = getSqlConnection(connectionKey))
            {
                param = new SqlParameter();
                param.ParameterName = "@Area_code";
                param.Value = ddlArea.SelectedValue.ToString();
                BindGridView(ddlAreaOnChangeStr);
            }
            
        }

        protected void gvHouses_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvHouses.SelectedRow;
            Session["shouseID"] = row.Cells[1].Text;
            Response.Redirect("viewit.aspx");
        }
    }
}