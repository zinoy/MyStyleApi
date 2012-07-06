using System;
using Newtonsoft.Json;
using System.ComponentModel;

namespace EvoqueMyStyle.Website.Schema
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Status
    {
        // "Tue May 24 18:04:53 +0800 2011",
        /*[JsonProperty]
        public DateTime created_at { get; set; }*/

        // 11142488790,
        [JsonProperty]
        public long id { get; set; }

        // "我的相机到了。",
        [JsonProperty]
        public string text { get; set; }

        // "<a href="http://weibo.com" rel="nofollow">新浪微博</a>",
        [JsonProperty]
        public string source { get; set; }

        // false,
        [JsonProperty]
        public bool favorited { get; set; }

        // false,
        [JsonProperty]
        public bool truncated { get; set; }

        // "",
        [JsonProperty]
        [DefaultValue(0)]
        public long in_reply_to_status_id { get; set; }

        // "",
        [JsonProperty]
        [DefaultValue(0)]
        public long in_reply_to_user_id { get; set; }

        // "",
        [JsonProperty]
        [DefaultValue(0)]
        public string in_reply_to_screen_name { get; set; }

        // null,
        [JsonProperty]
        public object geo { get; set; }

        // "5610221544300749636",
        [JsonProperty]
        public long mid { get; set; }

        // [],
        [JsonProperty]
        public string[] annotations { get; set; }

        // 5,
        [JsonProperty]
        public int reposts_count { get; set; }

        // 8
        [JsonProperty]
        public int comments_count { get; set; }
    }
}
