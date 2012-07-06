using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoqueMyStyle.DataAccess.Tables;
using EvoqueMyStyle.DataAccess;

namespace EvoqueMyStyle.Website
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] commands = { "getpics" };
            string ac = Request["ac"];
            if (Request.HttpMethod != "POST")
            {
                XMLOutput.ReturnValue("错误的请求", "0101");
                return;
            }

            switch (ac)
            {
                #region 获取上传的图片
                case "getpics":
                    string page = Request.Form["p"];
                    string size = Request.Form["s"];

                    if (string.IsNullOrEmpty(page) || string.IsNullOrEmpty(size))
                    {
                        XMLOutput.ReturnValue("参数不能为空", "0201");
                        return;
                    }

                    int pageidx;
                    if (!int.TryParse(page, out pageidx))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }
                    int psize;
                    if (!int.TryParse(size, out psize))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }

                    es_getpics gp = new es_getpics(pageidx, psize);
                    IList<share> pics = share.Instance.GetDataTransferObjectList(gp.ExecuteReader());
                    XMLOutput.ReturnPicsList(pics, gp.total);
                    break;
                #endregion

                default:
                    XMLOutput.ReturnValue("不支持的命令", "0100");
                    return;
            }
        }
    }
}