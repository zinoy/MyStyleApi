using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoqueMyStyle.DataAccess.Tables;
using EvoqueMyStyle.DataAccess;
using System.Drawing;

namespace EvoqueMyStyle.Website
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] commands = { "getpics", "upload", "savepic" };
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

                #region 上传照片
                case "upload":
                    if (Request.Files.Count == 0)
                    {
                        XMLOutput.ReturnValue("上传的文件不存在", "0205");
                        return;
                    }
                    HttpPostedFile file = Request.Files[0];
                    string path = Server.MapPath("~/upload/temp/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileInfo info = new FileInfo(file.FileName);
                    do
                    {
                        string filename = string.Format("{0}{1}", Utility.GenStr(32), info.Extension.ToLower());
                        path += filename;
                    } while (File.Exists(path));
                    file.SaveAs(path);

                    XMLOutput.ReturnValue(path, "0", "url");
                    break;
                #endregion

                #region 保存照片
                case "savepic":
                    string pic = Request.Form["url"];
                    string _x = Request.Form["x"];
                    string _y = Request.Form["y"];
                    string _r = Request.Form["ratio"];
                    string _w = Request.Form["width"];

                    if (string.IsNullOrEmpty(pic) || string.IsNullOrEmpty(_x) || string.IsNullOrEmpty(_y) || string.IsNullOrEmpty(_r) || string.IsNullOrEmpty(_w))
                    {
                        XMLOutput.ReturnValue("参数不能为空", "0201");
                        return;
                    }

                    float ratio;
                    if (!float.TryParse(_r, out ratio))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }
                    int x;
                    if (!int.TryParse(_x, out x))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }
                    int y;
                    if (!int.TryParse(_y, out y))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }
                    int width;
                    if (!int.TryParse(_w, out width))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }

                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(pic));

                    //Bitmap newimg = new Bitmap();
                    using (Graphics g = Graphics.FromImage(img))
                    {
                        g.ScaleTransform(ratio, ratio);
                        Rectangle rect = new Rectangle(x, y, width, width);
                        g.ExcludeClip(rect);
                        g.Save();
                        //img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }


                    break;
                #endregion

                default:
                    XMLOutput.ReturnValue("不支持的命令", "0100");
                    return;
            }
        }
    }
}