using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace EvoqueMyStyle.Website.Schema
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Friends
    {
        [JsonProperty]
        [JsonConverter(typeof(IList<User>))]
        public IList<User> users { get; set; }

        [JsonProperty]
        public int next_cursor { get; set; }

        [JsonProperty]
        public int previous_cursor { get; set; }

        [JsonProperty]
        public int total_number { get; set; }
    }
}