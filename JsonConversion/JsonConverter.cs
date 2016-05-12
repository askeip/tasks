using System.Globalization;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace JsonConversion
{
    public class JsonConverter : IJsonConverter
    {
        public string Convert(string json)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            var warehouseV2 = JsonConvert.DeserializeObject<WarehouseV2>(json);
            var productsV3 =
                warehouseV2.Products.Select(
                    prod =>
                        new ProductV3
                        {
                            Id = int.Parse(prod.Key),
                            Count = prod.Value.Count,
                            Name = prod.Value.Name,
                            Price = prod.Value.Price == null ? (decimal?)null : decimal.Parse(prod.Value.Price?.Replace("'", "").Replace(",", ".")),
                            Dimensions = prod.Value.Size == null
                                ? null
                                : new Dimensions
                                {
                                    W = prod.Value.Size.Length >= 1 ? decimal.Parse(prod.Value.Size[0].Replace("'", "").Replace(",", ".")) : (decimal?) null,
                                    H = prod.Value.Size.Length >= 2 ? decimal.Parse(prod.Value.Size[1].Replace("'", "").Replace(",", ".")) : (decimal?) null,
                                    L = prod.Value.Size.Length >= 3 ? decimal.Parse(prod.Value.Size[2].Replace("'", "").Replace(",", ".")) : (decimal?) null,
                                }
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
