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

namespace EvoqueMyStyle.Website
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] commands = { "getpics", "search", "upload", "savepic", "getcount" };
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
                case "upload":
                    if (Request.Files.Count == 0)
                    {
                        XMLOutput.ReturnValue("上传的文件不存在", "0205");
                        return;
                    }
                    HttpPostedFile file = Request.Files[0];
                    string rpath = "upload/temp/";
                    string path = Server.MapPath(string.Format("~/{0}", rpath));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileInfo info = new FileInfo(file.FileName);
                    string fpath;
                    string filename;
                    do
                    {
                        filename = string.Format("{0}{1}", Utility.GenStr(21), info.Extension.ToLower());
                        fpath = string.Format("{0}{1}", path, filename);
                    } while (File.Exists(fpath));
                    file.SaveAs(fpath);

                    XMLOutput.ReturnValue(rpath + filename, "0", "url");
                    break;
                #endregion

                #region 保存照片
                case "savepic":
                    string pic = Request.Form["url"];
                    string _x = Request.Form["x"];
                    string _y = Request.Form["y"];
                    string _a = Request.Form["rotate"];
                    string _r = Request.Form["ratio"];
                    string _w = Request.Form["width"];
                    string _t = Request.Form["comment"];
                    string _c = Request.Form["category"];

                    if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(pic) || string.IsNullOrEmpty(_x) || string.IsNullOrEmpty(_y) || string.IsNullOrEmpty(_a) || string.IsNullOrEmpty(_r) || string.IsNullOrEmpty(_w) || string.IsNullOrEmpty(_t) || string.IsNullOrEmpty(_c))
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

                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(pic));
                    System.Drawing.Image descimg = new Bitmap(bigwidth, bigwidth);

                    ImageCodecInfo jpegCodeInfo = GetEncoderInfo("image/jpeg");
                    EncoderParameters jpegParams = new EncoderParameters(1);
                    jpegParams.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

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
                    File.Delete(Server.MapPath(pic));

                    //add to DB
                    int tu;
                    if (int.TryParse(uid, out tu))
                    {
                        es_addsinapic add = new es_addsinapic();
                        add.comment = _t;
                        add.img = string.Format("{0}{1}", _path.Replace("upload/", string.Empty), fname);
                        add.type = _c;
                        add.uid = uid;
                        add.ExecuteNonQuery();
                    }

                    XMLOutput.ReturnValue(string.Format("{0}{1}", _path.Replace("upload/", string.Empty), fname), "0", "img");
                    break;
                #endregion

                #region 获取图片总数
                case "getcount":
                    es_getcount gc = new es_getcount();
                    gc.ExecuteNonQuery();

                    XMLOutput.ReturnValue(gc.RETURN_VALUE.ToString(), "0", "count");
                    break;
                #endregion

                default:
                    XMLOutput.ReturnValue("不支持的命令", "0100");
                    return;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private bool ImageAbort()
        {
            return true;
        }
    }
}