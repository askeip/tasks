using System;
using Newtonsoft.Json;

namespace JsonConversion
{
    public class ProductV3  
    {
		[JsonProperty(PropertyName = "id")]
		public int? Id { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "price")]
		public decimal? Price { get; set; }

		[JsonProperty(PropertyName = "count")]
		public int? Count { get; set; }

        [JsonProperty(PropertyName = "dimensions")]
        public Dimensions Dimensions { get; set; }
    }

    public class ProductV2
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "price")]
		public string Price { get; set; }

		[JsonProperty(PropertyName = "count")]
		public int? Count { get; set; }

        [JsonProperty(PropertyName = "size")]
        public string[] Size { get; set; }
    }
}
