using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoqueMyStyle.DataAccess;

namespace EvoqueMyStyle.Website
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] commands = { "getfriends" };
            string ac = Request["ac"];
            if (Request.HttpMethod != "POST")
            {
                XMLOutput.ReturnValue("错误的请求", "0101");
                return;
            }
            string hash = Request.Form["hash"];
            if (string.IsNullOrEmpty(hash))
            {
                XMLOutput.ReturnValue("需要验证身份", "0102");
                return;
            }
            if (!Utility.ValidateAuthorization(Request.Form, hash))
            {
                XMLOutput.ReturnValue("身份验证失败", "0103");
                return;
            }
            if (string.IsNullOrEmpty(Request.Form["u"]))
            {
                XMLOutput.ReturnValue("参数不能为空", "0201");
                return;
            }
            int uid;
            if (!int.TryParse(Request.Form["u"], out uid))
            {
                XMLOutput.ReturnValue("参数非法", "0202");
                return;
            }

            switch (ac)
            {
                case "getfriends":
                    es_getfriends friends = new es_getfriends();
                    friends.uid = uid;
                    friends.ExecuteNonQuery();
                    XMLOutput.ReturnValue(friends.friends, "0", "friends");
                    break;
                default:
                    XMLOutput.ReturnValue("不支持的命令", "0100");
                    return;
            }
        }
    }
}