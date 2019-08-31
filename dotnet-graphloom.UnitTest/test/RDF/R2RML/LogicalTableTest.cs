using GraphLoom.Mapper.RDF.R2RML;
using NUnit.Framework;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    [TestFixture]
    public class LogicalTableTest
    {
        private LogicalTable _logicalTable;
        private readonly string _expectedSource = "Source_1";

        [SetUp]
        public void SetUp()
        {
            _logicalTable = new LogicalTable(_expectedSource);
        }

        [Test]
        public void WhenGetSource_ReturnExpectedString()
        {
            string result = _logicalTable.SourceName;
            Assert.That(result, Is.EqualTo(_expectedSource));
        }
    }
}
