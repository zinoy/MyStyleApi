using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EvoqueMyStyle.Website.Schema
{
    [JsonObject(MemberSerialization.OptIn)]
    public class User
    {
        // "1404376560"
        [JsonProperty]
        public long id { get; set; }

        // "zaku"
        [JsonProperty]
        public string screen_name { get; set; }

        // "zaku"
        [JsonProperty]
        public string name { get; set; }

        // "11"
        [JsonProperty]
        public int province { get; set; }

        // "5",
        [JsonProperty]
        public int city { get; set; }

        // "北京 朝阳区",
        [JsonProperty]
        public string location { get; set; }

        // "人生五十年，乃如梦如幻；有生斯有死，壮士复何憾。",
        [JsonProperty]
        public string description { get; set; }

        // "http://blog.sina.com.cn/zaku",
        [JsonProperty]
        public string url { get; set; }

        // "http://tp1.sinaimg.cn/1404376560/50/0/1",
        [JsonProperty]
        public string profile_image_url { get; set; }

        // "zaku",
        [JsonProperty]
        public string domain { get; set; }

        // "m",
        [JsonProperty]
        public string gender { get; set; }

        // 1204,
        [JsonProperty]
        public int followers_count { get; set; }

        // 447,
        [JsonProperty]
        public int friends_count { get; set; }

        // 2908,
        [JsonProperty]
        public int statuses_count { get; set; }

        // 0,
        [JsonProperty]
        public int favourites_count { get; set; }

        // "Fri Aug 28 00:00:00 +0800 2009",
        /*[JsonProperty]
        public DateTime created_at { get; set; }*/

        // flse,
        [JsonProperty]
        public bool following { get; set; }

        // false,
        [JsonProperty]
        public bool allow_all_act_msg { get; set; }

        // true,
        [JsonProperty]
        public bool geo_enabled { get; set; }

        // false,
        [JsonProperty]
        public bool verified { get; set; }

        /*"status": {
            created_at": "Tue May 24 18:04:53 +0800 2011",
            "id": 11142488790,
            "text": "我的相机到了。",
            "source": "<a href="http://weibo.com" rel="nofollow">新浪微博</a>",
            "favorited": false,
            "truncated": false,
            "in_reply_to_status_id": "",
            "in_reply_to_user_id": "",
            "in_reply_to_screen_name": "",
            "geo": null,
            "mid": "5610221544300749636",
            "annotations": [],
            "reposts_count": 5,
            "comments_count": 8
        },*/
        [JsonProperty]
        public Status status { get; set; }

        // true,
        [JsonProperty]
        public bool allow_all_comment { get; set; }

        // "http://tp1.sinaimg.cn/1404376560/180/0/1",
        [JsonProperty]
        public string avatar_large { get; set; }

        // "",
        [JsonProperty]
        public string verified_reason { get; set; }

        // false,
        [JsonProperty]
        public bool follow_me { get; set; }

        // 用户的在线状态，0：不在线、1：在线
        [JsonProperty]
        public int online_status { get; set; }

        // 用户的互粉数
        [JsonProperty]
        public int bi_followers_count { get; set; }
    }
}
