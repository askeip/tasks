using System;
using Newtonsoft.Json.Linq;

namespace JsonConversion
{
    public class JsonConverter : IJsonConverter
    {
        public string Convert(string json)
        {
            var jObj = JObject.Parse(json);

            foreach (var jToken in jObj["products"])
            {
                Console.Out.Write(jToken);
            }
            return string.Empty;
        }
    }
}
