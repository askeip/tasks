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

	        var warehouseV3 = new WarehouseV3()
	        {
		        Version = Version.Three,
		        Products = productsV3.ToList()
	        };

            var tokenV3 = JToken.FromObject(warehouseV3);
            return tokenV3.ToString();
        }
    }
}
