using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JsonConversion
{
	class WarehouseV2
	{
		[JsonProperty(PropertyName = "version")]
		public Version Version { get; set; }

		[JsonProperty(PropertyName = "products")]
		public Dictionary<string, ProductV3> Products { get; set; }

	}

	class WarehouseV3
	{
		[JsonProperty(PropertyName = "version")]
		public Version Version { get; set; }

		[JsonProperty(PropertyName = "products")]
		public List<ProductV3> Products { get; set; }
	}
}
