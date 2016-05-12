using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonConversion
{
	class WarehouseV2
	{
		[JsonProperty(PropertyName = "version")]
		public Version Version { get; set; }

		[JsonProperty(PropertyName = "products")]
		public Dictionary<string, ProductV2> Products { get; set; }

	}

	class WarehouseV3
	{
		[JsonProperty(PropertyName = "version")]
		public Version Version { get; set; }

		[JsonProperty(PropertyName = "products")]
		public List<ProductV3> Products { get; set; }
	}
}
