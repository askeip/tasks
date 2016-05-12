using Newtonsoft.Json.Linq;
using System;

namespace JsonConversion
{
	class JsonProgram
	{
		static void Main()
		{
			string json = Console.In.ReadToEnd();
            var converter = new JsonConverter();
		    var v3 = converter.Convert(json);
			Console.Write(v3);
		}
	}
}
