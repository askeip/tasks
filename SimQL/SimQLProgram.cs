using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
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
                    return string.Format("{0} = {1}", query, data.SumQuery(queryArgs));
                case Functions.min:
                    return string.Format("{0} = {1}", query, data.MinQuery(queryArgs));
                case Functions.max:
                    return string.Format("{0} = {1}", query, data.MaxQuery(queryArgs));
                default:
                    throw new ArgumentException();
            }
        }

        private static IEnumerable<double> GetNums(this JObject data, string[] queryArgs)
        {
            IEnumerable<JToken> currTokens = new[] {(JToken)data};
            foreach (var tokenName in queryArgs)
            {
                currTokens = currTokens.SelectMany(subToken =>
                {
                    if (subToken.Type != JTokenType.Array)
                        return subToken.SelectTokens(tokenName);
                    return subToken.SelectMany(arrElement => arrElement.SelectTokens(tokenName));
                });
            }

            var resultingNums = new List<double>();

            foreach (var resultToken in currTokens)
            {
                switch (resultToken.Type)
                {
                    case JTokenType.Float:
                        resultingNums.Add((double)resultToken);
                        break;
                    case JTokenType.Integer:
                        resultingNums.Add((int)resultToken);
                        break;
                    case JTokenType.Array:
                        foreach (var numToken in resultToken)
                        {
                            resultingNums.Add((double)numToken);
                        }
                        break;
                }
            }

            return resultingNums;
        }


        private static double SumQuery(this JObject data, string[] queryArgs)
        {
            var sum = 0.0;

            foreach (var num in data.GetNums(queryArgs))
                sum += num;

            return sum;
        }

        private static double MinQuery(this JObject data, string[] queryArgs)
        {
            var min = double.MaxValue;

            foreach (var num in data.GetNums(queryArgs))
                if (num < min)
                    min = num;

            return min;
        }

        private static double MaxQuery(this JObject data, string[] queryArgs)
        {
            var max = double.MinValue;

            foreach (var num in data.GetNums(queryArgs))
                if (num > max)
                    max = num;

            return max;
        }
    }
}
