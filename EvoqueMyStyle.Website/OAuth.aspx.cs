using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Newtonsoft.Json;
using EvoqueMyStyle.Website.Schema;
using EvoqueMyStyle.DataAccess.Tables;
using EvoqueMyStyle.DataAccess;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace EvoqueMyStyle.Website
{
    public partial class OAuth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
            {
                lbResult.Text = "require 'code'";
                return;
            }
            string st = Request.QueryString["state"];
            if (string.IsNullOrEmpty(st))
            {
                lbResult.Text = "require 'state'";
                return;
            }
            NameValueCollection query = new NameValueCollection();
            query.Add("client_id", ConfigHelper.SinaAppKey);
            query.Add("client_secret", ConfigHelper.SinaSecret);
            query.Add("grant_type", "authorization_code");
            query.Add("redirect_uri", Request.Url.GetLeftPart(UriPartial.Path));
            query.Add("code", code);

            try
            {
                string responseString = Utility.GetJsonString("https://api.weibo.com/oauth2/access_token", query);
                if (!string.IsNullOrEmpty(responseString))
                {
                    Token json = JsonConvert.DeserializeObject<Token>(responseString);

                    NameValueCollection userid = new NameValueCollection();
                    userid.Add("uid", json.uid.ToString());
                    string userinfo = Utility.GetJsonString("https://api.weibo.com/2/users/show.json", userid, json.access_token);
                    JObject userjson = JObject.Parse(userinfo);

                    userid.Add("count", "200");
                    string friends = Utility.GetJsonString("https://api.weibo.com/2/friendships/friends.json", userid, json.access_token);
                    JObject following = JObject.Parse(friends);

                    es_addsinauser add = new es_addsinauser();
                    add.expire = DateTime.Now.AddSeconds((double)json.expires_in);
                    add.follow = string.Join(",", following["users"].Select(f => (string)f["screen_name"]).ToArray());
                    add.name = userjson["screen_name"].ToString();
                    add.state = st;
                    add.token = json.access_token;
                    add.uid = json.uid;
                    add.ExecuteNonQuery();

                    lbScript.Text = "<script type=\"text/javascript\">opener.setuser(\"" + json.uid.ToString() + "\");window.close();</script>";
                }
            }
            catch (WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    {
                        string text = new StreamReader(data).ReadToEnd();
                        lbResult.Text = text;
                    }
                }
            }
            catch (Exception ex)
            {
                lbResult.Text = ex.Message;
            }
        }
    }
}