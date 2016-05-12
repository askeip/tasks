using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace JsonConversion
{
	[TestFixture]
	public class JsonConverter_Test_Should
	{
		private IJsonConverter jsonConverter;
		private string jsonV2Example2;
		private string jsonV3Example2;

		private string jsonV2Example1;
		private string jsonV3Example1;

		private string jsonV3Schema;

		[SetUp]
		public void SetUp()
		{
			// TODO:
			jsonConverter = new JsonConverter();
			
			jsonV2Example1 = File.ReadAllText("Samples/1.v2.json");
			jsonV3Example1 = File.ReadAllText("Samples/1.v3.json");

			jsonV2Example2 = File.ReadAllText("Samples/2.v2.json");
			jsonV3Example2 = File.ReadAllText("Samples/2.v3.json");

			jsonV3Schema = File.ReadAllText("Samples/warehouse-v3-schema.json");
		}

		[Test]
		public void ConvertFromOldVersion_WhenDataIsCorrect()
		{
			var result = jsonConverter.Convert(jsonV2Example1);
			CompareJsonStrings(jsonV3Example1, result);
		}

		[Test]
		public void ConvertFromOldVersion_WhenDataIsCorrect2()
		{
			var result = jsonConverter.Convert(jsonV2Example2);
			CompareJsonStrings(jsonV3Example2, result);
		}

		[Test]
		public void ValidDeserialization_WhenConvertFrom()
		{
			var convertedResult = jsonConverter.Convert(jsonV2Example1);
			var schema = JSchema.Parse(jsonV3Schema);
			var jObject = JObject.Parse(convertedResult);

			var valid = jObject.IsValid(schema);

			Assert.True(valid);
		}

		public bool CompareJsonStrings(string expected, string actual)
		{
			var fixedStringOne = Regex.Replace(expected, @"\s+", string.Empty);
			var fixedStringTwo = Regex.Replace(actual, @"\s+", string.Empty);

			return string.Equals(fixedStringOne, fixedStringTwo,
										  StringComparison.OrdinalIgnoreCase);
		}
	}
}