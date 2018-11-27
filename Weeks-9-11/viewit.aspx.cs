using System;
using System.Collections.Generic;
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
            lblHcode.Text = Convert.ToString(Session["shouseID"]);
        }
    }
}