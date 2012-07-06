using Newtonsoft.Json;

namespace EvoqueMyStyle.Website.Schema
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Token
    {
        // "2.00jG5MNCxyT5LBaa15db383cdNShXD"
        [JsonProperty]
        public string access_token { get; set; }

        // "2.00jG5MNCxyT5LBaa15db383cdNShXD"
        [JsonProperty]
        public string refresh_token { get; set; }

        // "86400"
        [JsonProperty]
        public long expires_in { get; set; }

        // "2027310641"
        [JsonProperty]
        public long uid { get; set; }
    }
}
