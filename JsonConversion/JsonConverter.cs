using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonConversion
{
    public class JsonConverter : IJsonConverter
    {
        public string Convert(string json)
        {
            var warehouseV2 = JsonConvert.DeserializeObject<WarehouseV2>(json);
            var productsV3 =
				warehouseV2.Products.Select(
                    prod =>
                        new ProductV3
                        {
                            Id = int.Parse(prod.Key),
                            Count = prod.Value.Count,
                            Name = prod.Value.Name,
                            Price = prod.Value.Price
                        });

	        var warehouseV3 = new WarehouseV3
	        {
		        Version = "3",
		        Products = productsV3.ToList()
	        };

            return JsonConvert.SerializeObject(warehouseV3,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}
