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
using System.Drawing.Imaging;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace EvoqueMyStyle.Website
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            string uid = Request.Form["uid"];

            switch (ac)
            {
                #region 获取上传的图片
                case "getpics":
                    string page = Request.Form["p"];
                    string size = Request.Form["s"];
                    string cate = Request.Form["ca"];

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

                    es_getpics gp = new es_getpics();
                    gp.category = cate == "null" ? null : cate;
                    gp.page = pageidx;
                    gp.size = psize;
                    gp.uid = uid;
                    IList<share> pics;
                    try
                    {
                        pics = share.Instance.GetDataTransferObjectList(gp.ExecuteReader());
                    }
                    catch (Exception ex)
                    {
                        XMLOutput.ReturnValue(ex.Message, "0500");
                        return;
                    }
                    XMLOutput.ReturnPicsList(pics, gp.total);
                    break;
                #endregion

                #region 搜索图片
                case "search":
                    string _page = Request.Form["p"];
                    string _size = Request.Form["s"];
                    string _cate = Request.Form["ca"];
                    string _query = Request.Form["q"];

                    if (string.IsNullOrEmpty(_query) || string.IsNullOrEmpty(_page) || string.IsNullOrEmpty(_size))
                    {
                        XMLOutput.ReturnValue("参数不能为空", "0201");
                        return;
                    }

                    int _pageidx;
                    if (!int.TryParse(_page, out _pageidx))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }
                    int _psize;
                    if (!int.TryParse(_size, out _psize))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }

                    es_searchpics sp = new es_searchpics();
                    sp.key = _query;
                    sp.page = _pageidx;
                    sp.size = _psize;
                    IList<share> _pics;
                    try
                    {
                        _pics = share.Instance.GetDataTransferObjectList(sp.ExecuteReader());
                    }
                    catch (Exception ex)
                    {
                        XMLOutput.ReturnValue(ex.Message, "0500");
                        return;
                    }
                    XMLOutput.ReturnPicsList(_pics, sp.total);
                    break;
                #endregion

                #region 上传照片
                case "uploadpic":
                    if (Request.Files.Count == 0)
                    {
                        XMLOutput.ReturnValue("上传的文件不存在", "0205");
                        return;
                    }
                    //string pic = Request.Form["url"];
                    string _x = Request.Form["x"];
                    string _y = Request.Form["y"];
                    string _a = Request.Form["rotate"];
                    string _r = Request.Form["ratio"];
                    string _w = Request.Form["width"];
                    string _t = Request.Form["comment"];
                    string _c = Request.Form["category"];

                    if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(_x) || string.IsNullOrEmpty(_y) || string.IsNullOrEmpty(_a) || string.IsNullOrEmpty(_r) || string.IsNullOrEmpty(_w) || string.IsNullOrEmpty(_t) || string.IsNullOrEmpty(_c))
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
                    float angle;
                    if (!float.TryParse(_a, out angle))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }
                    float fx;
                    if (!float.TryParse(_x, out fx))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }
                    int x = (int)fx;
                    float fy;
                    if (!float.TryParse(_y, out fy))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }
                    int y = (int)fy;
                    float w;
                    if (!float.TryParse(_w, out w))
                    {
                        XMLOutput.ReturnValue("参数非法", "0202");
                        return;
                    }
                    int width = (int)w;

                    //裁剪图片
                    int bigwidth;
                    int bigx, bigy;
                    if (ratio < 1)
                    {
                        bigwidth = (int)Math.Round(width / ratio);
                        bigx = (int)Math.Round(x / ratio);
                        bigy = (int)Math.Round(y / ratio);
                    }
                    else
                    {
                        bigwidth = width;
                        bigx = x;
                        bigy = y;
                    }

                    string _path = string.Format("upload/{0:MMdd}/", DateTime.Now);
                    string serverpath;
                    if (string.IsNullOrEmpty(ConfigHelper.ImagePath))
                    {
                        serverpath = Server.MapPath(string.Format("~/{0}", _path));
                    }
                    else
                    {
                        serverpath = ConfigHelper.ImagePath + _path;
                    }
                    if (!Directory.Exists(serverpath))
                    {
                        Directory.CreateDirectory(serverpath);
                    }
                    string fname = Guid.NewGuid().ToString("N");
                    string filepath = string.Format("{0}{1}_", serverpath, fname);

                    HttpPostedFile file = Request.Files[0];
                    System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                    System.Drawing.Image descimg = new Bitmap(bigwidth, bigwidth);

                    ImageCodecInfo jpegCodeInfo = Utility.GetEncoderInfo("image/jpeg");
                    EncoderParameters jpegParams = new EncoderParameters(1);
                    jpegParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

                    using (Graphics g = Graphics.FromImage(descimg))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


                        Rectangle rect = new Rectangle(0, 0, bigwidth, bigwidth);
                        Rectangle from = new Rectangle(bigx, bigy, bigwidth, bigwidth);

                        g.Clear(Color.White);

                        if (angle > 0)
                        {
                            Bitmap timg = null;
                            if (angle / 90 % 2 == 0)
                                timg = new Bitmap(img.Width, img.Height);
                            else
                                timg = new Bitmap(img.Height, img.Width);
                            Graphics org = Graphics.FromImage(timg);
                            org.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            org.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            //计算偏移量
                            Point offset = new Point((timg.Width - img.Width) / 2, (timg.Height - img.Height) / 2);
                            //构造图像显示区域：让图像的中心与窗口的中心点一致
                            Rectangle trect = new Rectangle(offset.X, offset.Y, img.Width, img.Height);
                            Point center = new Point(trect.X + trect.Width / 2, trect.Y + trect.Height / 2);
                            org.TranslateTransform(center.X, center.Y);
                            org.RotateTransform(angle);
                            org.TranslateTransform(-center.X, -center.Y);
                            org.DrawImage(img, trect);

                            org.ResetTransform();
                            org.Save();
                            org.Dispose();
                            //timg.Save(string.Format("{0}r_.jpg", filepath), jpegCodeInfo, jpegParams);

                            g.DrawImage(timg, rect, from, GraphicsUnit.Pixel);
                            timg.Dispose();
                        }
                        else
                        {
                            g.DrawImage(img, rect, from, GraphicsUnit.Pixel);
                        }
                        g.Save();

                    }
                    descimg.Save(string.Format("{0}o_.jpg", filepath), jpegCodeInfo, jpegParams);
                    System.Drawing.Image big = descimg.GetThumbnailImage(width, width, new System.Drawing.Image.GetThumbnailImageAbort(ImageAbort), new IntPtr());
                    big.Save(string.Format("{0}big.jpg", filepath), jpegCodeInfo, jpegParams);
                    System.Drawing.Image thumb = big.GetThumbnailImage(100, 100, new System.Drawing.Image.GetThumbnailImageAbort(ImageAbort), new IntPtr());
                    thumb.Save(string.Format("{0}small.jpg", filepath), jpegCodeInfo, jpegParams);

                    thumb.Dispose();
                    big.Dispose();
                    img.Dispose();
                    //File.Delete(Server.MapPath(pic));

                    //add to DB
                    int tu;
                    if (!int.TryParse(uid, out tu))
                    {
                        es_addlocalpic add = new es_addlocalpic();
                        add.comment = _t;
                        add.img = string.Format("{0}{1}", _path.Replace("upload/", string.Empty), fname);
                        add.type = _c;
                        add.uid = uid;
                        add.ExecuteNonQuery();
                        XMLOutput.ReturnValue("ok", "0", "message");
                        return;
                    }

                    es_addsinapic adds = new es_addsinapic();
                    adds.comment = _t;
                    adds.img = string.Format("{0}{1}", _path.Replace("upload/", string.Empty), fname);
                    adds.type = _c;
                    adds.uid = uid;
                    adds.ExecuteNonQuery();

                    string accessToken = adds.token;
                    UriBuilder ub = new UriBuilder("https://upload.api.weibo.com/2/statuses/upload.json");

                    Dictionary<string, string> query = new Dictionary<string, string>();
                    query.Add("access_token", accessToken);
                    query.Add("status", HttpUtility.UrlEncode(_t));

                    HttpWebRequest req = null;
                    req = (HttpWebRequest)WebRequest.Create(ub.Uri);
                    req.Method = "POST";
                    string boundary = Utility.CreateFormDataBoundary();
                    req.ContentType = "multipart/form-data; boundary=" + boundary;
                    Stream sm = req.GetRequestStream();
                    foreach (string key in query.Keys)
                    {
                        string item = String.Format(Utility.FormDataTemplate, boundary, key, query[key]);
                        byte[] itemBytes = System.Text.Encoding.UTF8.GetBytes(item);
                        sm.Write(itemBytes, 0, itemBytes.Length);
                    }

                    FileInfo fi = new FileInfo(string.Format("{0}o_.jpg", filepath));
                    string header = String.Format(Utility.HeaderTemplate, boundary, "pic", fi.Name, "image/jpeg");
                    byte[] headerbytes = Encoding.UTF8.GetBytes(header);
                    sm.Write(headerbytes, 0, headerbytes.Length);
                    using (FileStream fileStream = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            sm.Write(buffer, 0, bytesRead);
                        }
                        fileStream.Close();
                    }
                    byte[] newlineBytes = Encoding.UTF8.GetBytes("\r\n");
                    sm.Write(newlineBytes, 0, newlineBytes.Length);
                    byte[] endBytes = System.Text.Encoding.UTF8.GetBytes("--" + boundary + "--");
                    sm.Write(endBytes, 0, endBytes.Length);
                    sm.Close();

                    using (WebResponse response = req.GetResponse())
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        JObject result = JObject.Parse(reader.ReadToEnd());
                        if (result["error"] == null)
                        {
                            XMLOutput.ReturnValue("ok", "0", "message");
                        }
                        else
                        {
                            XMLOutput.ReturnValue((string)result["error"], (string)result["error_code"]);
                        }
                    }
                    break;
                #endregion

                #region 获取图片总数
                case "getcount":
                    es_getcount gc = new es_getcount();
                    gc.ExecuteNonQuery();

                    XMLOutput.ReturnValue(gc.RETURN_VALUE.ToString(), "0", "count");
                    break;
                #endregion

                #region 试驾
                case "td":
                    string _user = Request.Form["user"];
                    string _phone = Request.Form["phone"];
                    string _mail = Request.Form["mail"];

                    if (string.IsNullOrEmpty(_user) || string.IsNullOrEmpty(_phone) || string.IsNullOrEmpty(_mail))
                    {
                        XMLOutput.ReturnValue("参数不能为空", "0201");
                        return;
                    }

                    es_addtd td = new es_addtd();
                    td.email = _mail;
                    td.ip = Request.UserHostAddress;
                    td.mobile = _phone;
                    td.name = _user;
                    td.ExecuteNonQuery();

                    XMLOutput.ReturnValue("ok", "0", "message");
                    break;
                #endregion

                default:
                    XMLOutput.ReturnValue("不支持的命令", "0100");
                    return;
            }
        }

        private bool ImageAbort()
        {
            return true;
        }
    }
}