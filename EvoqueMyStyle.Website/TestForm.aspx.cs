using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EvoqueMyStyle.Website
{
    public partial class TestForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            linkWeibo.NavigateUrl = string.Format("https://api.weibo.com/oauth2/authorize?client_id={0}&response_type=code&redirect_uri={1}&state=15152769", ConfigHelper.SinaAppKey, HttpUtility.UrlEncode("http://localhost:2103/OAuth.aspx"));
        }
    }
}