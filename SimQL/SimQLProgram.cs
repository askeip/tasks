using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace SimQLTask
{
	class SimQLProgram
	{
	    private static JObject dataFromJson; // объект, над которым выполняются запросы

	    static void Main(string[] args)
		{
			string json = Console.In.ReadToEnd();
			foreach (var result in ExecuteQueries(json))
				Console.WriteLine(result);
		}

		public static IEnumerable<string> ExecuteQueries(string json)
		{
			var jObject = JObject.Parse(json);
			dataFromJson = (JObject)jObject["data"];
			string[] queries = jObject["queries"].ToObject<string[]>();

		    foreach (var query in queries)
		    {
		        yield return dataFromJson.ExecuteQuery(query);
		    }
		}

	    public static string[] GetQueryArgs(string query)
	    {
	        var functionName = query.Substring(0, 3);

	        var functionParams = query
                .Substring(query.IndexOf("(") + 1, query.IndexOf(")") - query.IndexOf("(") - 1)
                .Split(new [] {'.'});

	        return functionParams;
	    }
	}

    public enum Functions
    {
        sum,
        min,
        max
    };

    public static class JObjectExtensions
    {
        public static string ExecuteQuery(this JObject data, string query)
        {
            var queryArgs = SimQLProgram.GetQueryArgs(query);
            var funcName = query.Substring(0, 3);

            var function = (Functions)Enum.Parse(typeof(Functions), funcName);
            switch (function)
            {
                case Functions.sum:
                    return data.SumQuery(queryArgs);
                case Functions.min:
                    return data.MinQuery(queryArgs);
                case Functions.max:
                    return data.MaxQuery(queryArgs);
                default:
                    throw new ArgumentException();
            }

            return "";
        }

        private static string SumQuery(this JObject data, string[] queryArgs)
        {
            var objectToSum = (JToken)data;
            foreach (var name in queryArgs)
            {
                if (objectToSum.Type != JTokenType.Array)
                    objectToSum = objectToSum[name];
            }



            return "";
        }

        private static string MinQuery(this JObject data, string[] queryArgs)
        {
            throw new NotImplementedException();
        }

        private static string MaxQuery(this JObject data, string[] queryArgs)
        {
            throw new NotImplementedException();
        }
    }
}
