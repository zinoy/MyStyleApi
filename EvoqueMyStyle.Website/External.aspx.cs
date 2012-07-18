using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using System.Collections.Specialized;
using EvoqueMyStyle.DataAccess;
using System.Net;
using Newtonsoft.Json.Linq;

namespace EvoqueMyStyle.Website
{
    public partial class External : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ac = Request["ac"];
            /*if (Request.HttpMethod != "POST")
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
            if (!Utility.ValidateAuthorization(Request.Form, hash, ConfigHelper.ExternalSecret))
            {
                XMLOutput.ReturnValue("身份验证失败", "0103");
                return;
            }*/

            switch (ac)
            {
                case "upload":
                    if (string.IsNullOrEmpty(Request.QueryString["pic"]) || string.IsNullOrEmpty(Request.QueryString["status"]) || string.IsNullOrEmpty(Request.QueryString["mobile"]) || string.IsNullOrEmpty(Request.QueryString["type"]))
                    {
                        XMLOutput.ReturnValue("参数不能为空", "0201");
                        return;
                    }

                    StringBuilder oauth = new StringBuilder("https://api.weibo.com/oauth2/authorize?");
                    NameValueCollection qs = new NameValueCollection();
                    qs.Add("client_id", ConfigHelper.SinaAppKey);
                    qs.Add("redirect_uri", string.Format("{0}/temp/OAuth.aspx", Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped)));
                    qs.Add("response_type", "code");
                    byte[] qstr = Encoding.UTF8.GetBytes(Request.Url.GetComponents(UriComponents.Query, UriFormat.Unescaped));
                    qs.Add("state", Convert.ToBase64String(qstr));
                    oauth.Append(Utility.ConstructQueryString(qs));
                    Response.Redirect(oauth.ToString());
                    break;
                case "submit":
                    string uid = Request.Form["uid"];
                    string pic = Request.Form["url"];
                    string _n = Request.Form["user"];
                    string _t = Request.Form["comment"];
                    string _c = Request.Form["category"];

                    if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(pic) || string.IsNullOrEmpty(_n) || string.IsNullOrEmpty(_t) || string.IsNullOrEmpty(_c))
                    {
                        XMLOutput.ReturnValue("参数不能为空", "0201");
                        return;
                    }

                    System.Drawing.Image img = Utility.DownloadImage(pic);
                    if (img == null)
                    {
                        XMLOutput.ReturnValue("指定的图片URL无效", "0205");
                        return;
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

                    ImageCodecInfo jpegCodeInfo = Utility.GetEncoderInfo("image/jpeg");
                    EncoderParameters jpegParams = new EncoderParameters(1);

                    img.Save(string.Format("{0}big.jpg", filepath), jpegCodeInfo, jpegParams);
                    System.Drawing.Image thumb = img.GetThumbnailImage(100, 100, new System.Drawing.Image.GetThumbnailImageAbort(ImageAbort), new IntPtr());
                    thumb.Save(string.Format("{0}small.jpg", filepath), jpegCodeInfo, jpegParams);
                    img.Dispose();
                    thumb.Dispose();

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