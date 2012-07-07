using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoqueMyStyle.DataAccess;
using System.Text.RegularExpressions;

namespace EvoqueMyStyle.Website
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] commands = { "adduser", "login", "getfriends" };
            string ac = Request["ac"];
            if (Request.HttpMethod != "POST")
            {
                XMLOutput.ReturnValue("错误的请求", "0101");
                return;
            }
            /*string hash = Request.Form["hash"];
            if (string.IsNullOrEmpty(hash))
            {
                XMLOutput.ReturnValue("需要验证身份", "0102");
                return;
            }
            if (!Utility.ValidateAuthorization(Request.Form, hash))
            {
                XMLOutput.ReturnValue("身份验证失败", "0103");
                return;
            }*/
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
                #region 添加本地用户
                case "adduser":
                    string _mail = Request.Form["email"];
                    string _pass = Request.Form["pass"];

                    if (string.IsNullOrEmpty(_mail) || string.IsNullOrEmpty(_pass))
                    {
                        XMLOutput.ReturnValue("参数不能为空", "0201");
                        return;
                    }
                    Regex _rmail = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                    if (!_rmail.IsMatch(_mail))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }

                    string _epass = Utility.SHA1(_pass + _mail);

                    //query db via procedure

                    XMLOutput.ReturnValue("ok", "0", "message");
                    break;
                #endregion

                #region 本地用户登录
                case "login":
                    string mail = Request.Form["email"];
                    string pass = Request.Form["pass"];

                    if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(pass))
                    {
                        XMLOutput.ReturnValue("参数不能为空", "0201");
                        return;
                    }
                    Regex rmail = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                    if (!rmail.IsMatch(mail))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }

                    string epass = Utility.SHA1(pass + mail);

                    //query db via procedure

                    XMLOutput.ReturnValue("ok", "0", "message");
                    break;
                #endregion

                #region 获取朋友列表
                case "getfriends":
                    es_getfriends friends = new es_getfriends();
                    friends.uid = uid;
                    friends.ExecuteNonQuery();
                    XMLOutput.ReturnValue(friends.friends, "0", "friends");
                    break;
                #endregion
                default:
                    XMLOutput.ReturnValue("不支持的命令", "0100");
                    return;
            }
        }
    }
}