using System;
using System.Collections.Generic;
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
			return queries.Select(ProcessQuery);
		}

	    public static string ProcessQuery(string query)
	    {
	        var functionName = query.Substring(0, 3);
	        string answer = "";

	        var functionParams = query.Substring(query.IndexOf("("), query.IndexOf(")") - query.IndexOf("("));

	        var function = (Functions) Enum.Parse(typeof(Functions), functionName);
	        switch (function)
	        {
	            case Functions.Sum:
	                break;
	            case Functions.Min:
	                break;
	            case Functions.Max:
	                break;
	            default:
	                throw new ArgumentOutOfRangeException();
	        }


	        return string.Format("{0} = {1}", query, answer);
	    }
	}

    public enum Functions
    {
        Sum,
        Min,
        Max
    };
}
