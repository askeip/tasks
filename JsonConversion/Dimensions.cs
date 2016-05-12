using Newtonsoft.Json;

namespace JsonConversion
{
    public class Dimensions
    {
        [JsonProperty(PropertyName = "l")]
        public decimal? L { get; set; }
        [JsonProperty(PropertyName = "w")]
        public decimal? W { get; set; }
        [JsonProperty(PropertyName = "h")]
        public decimal? H { get; set; }
    }
}
