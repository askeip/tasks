using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace JsonConversion
{
    public class JsonConverter : IJsonConverter
    {
        public string Convert(string json)
        {
            var jObj = JObject.Parse(json);

            var productsV2 = new Dictionary<string, ProductV2>();
            foreach (var jToken in (JObject)jObj["products"])
            {
                productsV2.Add(jToken.Key, jToken.Value.ToObject<ProductV2>());
            }

            var productsV3 =
                productsV2.Select(
                    prod =>
                        new ProductV3
                        {
                            Id = int.Parse(prod.Key),
                            Count = prod.Value.Count,
                            Name = prod.Value.Name,
                            Price = prod.Value.Price
                        });

            jObj["products"] = JToken.FromObject(productsV3);
            jObj["version"] = JToken.FromObject(((int)Version.Three).ToString());
            return jObj.ToString();
        }
    }
}
