using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.IO;

namespace EvoqueMyStyle.Website
{
    public static class Utility
    {
        /// <summary>
        /// 验证发送者的身份。
        /// </summary>
        /// <param name="args">传递的数据集合。</param>
        /// <param name="hash">验证指纹。</param>
        /// <returns></returns>
        public static bool ValidateAuthorization(NameValueCollection args, string hash)
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
            builder.Append(ConfigHelper.APISecret);
            string fp = SHA1(builder.ToString());
            if (fp == hash)
                return true;
            else
                return false;
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

    }
}