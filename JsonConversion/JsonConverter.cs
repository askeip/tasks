using System.Globalization;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonConversion
{
    public class JsonConverter : IJsonConverter
    {
        public string Convert(string jsonstr)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            var json = jsonstr.Replace("'", "");
            var warehouseV2 = JsonConvert.DeserializeObject<WarehouseV2>(json);
            var productsV3 =
                warehouseV2.Products.Select(
                    prod =>
                        new ProductV3
                        {
                            Id = int.Parse(prod.Key),
                            Count = prod.Value.Count,
                            Name = prod.Value.Name,
                            Price = prod.Value.Price,
                            Dimensions = prod.Value.Size == null
                                ? null
                                : new Dimensions
                                {
                                    W = prod.Value.Size.Length >= 1 ? prod.Value.Size[0] : null,
                                    H = prod.Value.Size.Length >= 2 ? prod.Value.Size[1] : null,
                                    L = prod.Value.Size.Length >= 3 ? prod.Value.Size[2] : null,
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
