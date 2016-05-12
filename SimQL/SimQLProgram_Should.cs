using System.Linq;
using NUnit.Framework;

namespace SimQLTask
{
	[TestFixture]
	public class SimQLProgram_Should
	{
		[Test]
		public void SumEmptyDataToZero()
		{
			var results = SimQLProgram.ExecuteQueries(
				"{" +
				"'data': [], " +
				"'queries': ['sum(item.cost)', 'sum(itemsCount)']}");
			Assert.AreEqual(new[] {0, 0}, results);
		}

		[Test]
		public void SumSingleItem()
		{
			var results = SimQLProgram.ExecuteQueries(
				"{" +
				"'data': [{'itemsCount':42}, {'foo':'bar'}], " +
				"'queries': ['sum(itemsCount)']}");
			Assert.AreEqual(new[] { 42 }, results);
		}

        [Test]
	    public void TestFromGithubTask()
        {
            var results =
                SimQLProgram.ExecuteQueries(
                    "{\r\n    \'data\': {\'a\':{\'x\':3.14, \'b\':[{\'c\':15}, {\'c\':9}]}, \'z\':[2.65, 35]},\r\n    \'queries\': [\r\n        \'sum(a.b.c)\',\r\n        \'min(z)\',\r\n        \'max(a.x)\'\r\n    ]\r\n}").ToList();
            Assert.AreEqual(new[] { 24, 2.65, 3.14 }, results);
        }

        [Test]
        public void TestForUnderstandingOfArraysInJson()
        {
            var results =
                SimQLProgram.ExecuteQueries(
                    "{\r\n    \'data\': {\'a\':{\'x\':3.14, \'b\':[{\'c\':15}, {\'c\':9}, {\'c\':1}, {\'d\':1}]}, \'z\':[2.65, 35]},\r\n    \'queries\': [\r\n        \'sum(a.b.c)\',\r\n        \'min(z)\',\r\n        \'max(a.x)\'\r\n    ]\r\n}");
            Assert.AreEqual(new[] { 24, 2.65, 3.14 }, results);
        }
    }
}