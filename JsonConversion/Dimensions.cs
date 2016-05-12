using Newtonsoft.Json;

namespace JsonConversion
{
    public class Dimensions
    {
        [JsonProperty(PropertyName = "l")]
        public string L { get; set; }
        [JsonProperty(PropertyName = "w")]
        public string W { get; set; }
        [JsonProperty(PropertyName = "h")]
        public string H { get; set; }
    }
}
