using System.IO;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace JsonConversion
{
	[TestFixture]
	public class JsonConverter_Test_Should
	{
		private readonly IJsonConverter jsonConverter;
		private string jsonV2Example;
		private string jsonV3Example;

		[SetUp]
		public void SetUp()
		{
			// TODO:
			jsonV2Example = File.ReadAllText("1.v2.json");
			jsonV3Example = File.ReadAllText("1.v3.json");
		}

		[Test]
		public void ConvertFromOldVersion_WhenDataIsCorrect()
		{
			var result = jsonConverter.Convert(jsonV2Example);
			Assert.AreEqual(jsonV3Example, result);
		} 
	}
}