using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace JsonConversion
{
	[TestFixture]
	public class JsonConverter_Test_Should
	{
		private IJsonConverter jsonConverter;
		private string jsonV2Example;
		private string jsonV3Example;
		private string jsonV3Schema;

		[SetUp]
		public void SetUp()
		{
			// TODO:
			jsonConverter = new JsonConverter();
			
			jsonV2Example = File.ReadAllText("Samples/1.v2.json");
			jsonV3Example = File.ReadAllText("Samples/1.v3.json");
			jsonV3Schema = File.ReadAllText("Samples/warehouse-v3-schema.json");
		}

		[Test]
		public void ConvertFromOldVersion_WhenDataIsCorrect()
		{
			var result = jsonConverter.Convert(jsonV2Example);
			Assert.AreEqual(jsonV3Example, result);
		}

		[Test]
		public void ValidDeserialization_WhenConvertFrom()
		{
			var convertedResult = jsonConverter.Convert(jsonV2Example);
			var schema = JSchema.Parse(jsonV3Schema);
			var jObject = JObject.Parse(convertedResult);

			var valid = jObject.IsValid(schema);

			Assert.True(valid);
		}
	}
}