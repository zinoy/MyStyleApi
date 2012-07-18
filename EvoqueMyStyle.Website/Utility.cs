using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing.Imaging;

namespace EvoqueMyStyle.Website
{
    public static class Utility
    {
        /// <summary>
        /// Template for a multipart/form-data item.
        /// </summary>
        public const string FormDataTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n";       /// <summary>
        /// Template for a file item in multipart/form-data format.
        /// </summary>
        public const string HeaderTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n";
        /// <summary>
        /// 验证发送者的身份。
        /// </summary>
        /// <param name="args">传递的数据集合。</param>
        /// <param name="hash">验证指纹。</param>
        /// <param name="secret">密文。</param>
        /// <returns></returns>
        public static bool ValidateAuthorization(NameValueCollection args, string hash, string secret)
        {
            StringBuilder builder = new StringBuilder();
            string[] keylist = args.Keys.Cast<string>().Select(key => key.ToLower()).ToArray();
            Array.Sort(keylist);
            foreach (string key in keylist)
            {
                if (key == "hash" || key == "filename" || key == "upload")
                {
                    continue;
                }
                builder.Append(args[key]);
            }
            builder.Append(secret);
            string fp = SHA1(builder.ToString());
            if (fp == hash)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 使用默认的密文验证发送者的身份。
        /// </summary>
        /// <param name="args">传递的数据集合。</param>
        /// <param name="hash">验证指纹。</param>
        /// <returns></returns>
        public static bool ValidateAuthorization(NameValueCollection args, string hash)
        {
            return ValidateAuthorization(args, hash, ConfigHelper.APISecret);
        }
        /// <summary>
        /// 将传入的字符串按照MD5进行加密。
        /// </summary>
        /// <param name="orignal">待加密的字符串。</param>
        /// <returns>返回加密后的字符串（小写）。</returns>
        public static string MD5(string orignal)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(orignal, "md5").ToLower();
        }
        /// <summary>
        /// 将指定的字符串用SHA1进行加密。
        /// </summary>
        /// <param name="orignal">待加密的字符串。</param>
        /// <returns>返回加密后的字符串（小写）。</returns>
        public static string SHA1(string orignal)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(orignal, "sha1").ToLower();
        }
        /// <summary>
        /// 生成一个长度为8的随机字符串。
        /// </summary>
        /// <returns></returns>
        public static string GenStr()
        {
            return GenStr(8);
        }
        /// <summary>
        /// 生成一个随机字符串。
        /// </summary>
        /// <param name="length">字符串长度。</param>
        /// <returns></returns>
        public static string GenStr(int length)
        {
            string str = "0123456789abcdefghijklmnopqrstuvwxyz";
            char[] chars = str.ToCharArray();
            StringBuilder builder = new StringBuilder();

            Random rand = new Random();
            while (builder.Length < length)
            {
                char c = chars[rand.Next(chars.Length)];
                int up = rand.Next(2);
                if (up == 1)
                    builder.Append(c.ToString().ToUpper());
                else
                    builder.Append(c);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 拼接用于GET提交查询的字符串。
        /// </summary>
        /// <param name="query">查询键值对集合。</param>
        /// <returns></returns>
        public static string ConstructQueryString(NameValueCollection query)
        {
            List<string> pairs = new List<string>();
            foreach (string key in query)
            {
                pairs.Add(string.Format("{0}={1}", key, HttpUtility.UrlEncode(query[key])));
            }
            return string.Join("&", pairs.ToArray());
        }
        /// <summary>
        /// 将指定的查询字符串转换为键值对。
        /// </summary>
        /// <param name="query">查询字符串。</param>
        /// <returns></returns>
        public static NameValueCollection FromQueryString(string query)
        {
            string qs = query.Replace("?", string.Empty);
            string[] part = qs.Split('&');
            NameValueCollection r = new NameValueCollection();
            foreach (string it in part)
            {
                string[] pair = it.Split('=');
                r.Add(pair[0], pair[1]);
            }
            return r;
        }
        /// <summary>
        /// 发送一个HTTP请求并获取返回的字符串。
        /// </summary>
        /// <param name="requestPath">请求URL。</param>
        /// <param name="query">参数。</param>
        /// <param name="accessToken">OAuth 2.0的Access Token。</param>
        /// <param name="method">请求方式。</param>
        /// <returns>请求返回的字符串。</returns>
        public static string GetJsonString(string requestPath, NameValueCollection query, string accessToken, string method)
        {
            UriBuilder ub = new UriBuilder(requestPath);

            if (query == null)
            {
                query = new NameValueCollection();
            }

            HttpWebRequest req = null;
            if (!string.IsNullOrEmpty(accessToken))
            {
                if (string.IsNullOrEmpty(query["access_token"]))
                    query.Add("access_token", accessToken);
                ub.Query = ConstructQueryString(query);
                req = (HttpWebRequest)WebRequest.Create(ub.Uri);
                req.Method = "GET";
            }
            else
            {
                req = (HttpWebRequest)WebRequest.Create(ub.Uri);
                byte[] buffer = Encoding.ASCII.GetBytes(ConstructQueryString(query));
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = buffer.Length;
                Stream sm = req.GetRequestStream();
                sm.Write(buffer, 0, buffer.Length);
                sm.Close();
            }
            if (method == "POST" || method == "GET")
            {
                req.Method = method;
            }

            req.AllowAutoRedirect = false;

            using (HttpWebResponse data = (HttpWebResponse)req.GetResponse())
            {
                if (data.StatusCode != HttpStatusCode.OK)
                {
                    throw new WebException(data.StatusDescription);
                }
                StreamReader sr = new StreamReader(data.GetResponseStream(), Encoding.UTF8);
                string responseString = sr.ReadToEnd();
                sr.Close();
                return responseString;
            }
        }
        /// <summary>
        /// 发送一个HTTP请求并获取返回的字符串。
        /// </summary>
        /// <param name="requestPath">请求URL。</param>
        /// <param name="query">参数。</param>
        /// <param name="accessToken">OAuth 2.0的Access Token。</param>
        /// <returns>请求返回的字符串。</returns>
        public static string GetJsonString(string requestPath, NameValueCollection query, string accessToken)
        {
            return GetJsonString(requestPath, query, accessToken, null);
        }
        /// <summary>
        /// 发送一个HTTP请求并获取返回的字符串。
        /// </summary>
        /// <param name="requestPath">请求URL。</param>
        /// <param name="query">参数。</param>
        /// <returns>请求返回的字符串。</returns>
        public static string GetJsonString(string requestPath, NameValueCollection query)
        {
            return GetJsonString(requestPath, query, null, "POST");
        }
        /// <summary>
        /// 发送一个HTTP请求并获取返回的字符串。
        /// </summary>
        /// <param name="requestPath">请求URL。</param>
        /// <param name="accessToken">OAuth 2.0的Access Token。</param>
        /// <returns>请求返回的字符串。</returns>
        public static string GetJsonString(string requestPath, string accessToken)
        {
            return GetJsonString(requestPath, null, accessToken, null);
        }
        /// <summary>
        /// 根据指定的图片MIME类型返回相应的<code>ImageCodecInfo</code>实例。
        /// </summary>
        /// <param name="mimeType">图片的MIME类型字符串。</param>
        /// <returns></returns>
        public static ImageCodecInfo GetEncoderInfo(String mimeType)
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

        /// <summary>
        /// Creates a multipart/form-data boundary.
        /// </summary>
        /// <returns>
        /// A dynamically generated form boundary for use in posting multipart/form-data requests.
        /// </returns>
        public static string CreateFormDataBoundary()
        {
            return "---------------------------" + DateTime.Now.Ticks.ToString("x");
        }
        /// <summary>
        /// 从指定的URL下载图片。
        /// </summary>
        /// <param name="_URL">图片的URL。</param>
        /// <returns>Image</returns>
        public static System.Drawing.Image DownloadImage(string _URL)
        {
            System.Drawing.Image _tmpImage = null;

            try
            {
                // Open a connection
                System.Net.HttpWebRequest _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);

                _HttpWebRequest.AllowWriteStreamBuffering = true;

                // You can also specify additional header values like the user agent or the referer: (Optional)
                //_HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                //_HttpWebRequest.Referer = "http://www.google.com/";

                // set timeout for 20 seconds (Optional)
                _HttpWebRequest.Timeout = 20000;

                // Request response:
                System.Net.WebResponse _WebResponse = _HttpWebRequest.GetResponse();

                // Open data stream:
                System.IO.Stream _WebStream = _WebResponse.GetResponseStream();

                // convert webstream to image
                _tmpImage = System.Drawing.Image.FromStream(_WebStream);

                // Cleanup
                _WebResponse.Close();
                _WebResponse.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
                return null;
            }
            return _tmpImage;
        }

    }

}