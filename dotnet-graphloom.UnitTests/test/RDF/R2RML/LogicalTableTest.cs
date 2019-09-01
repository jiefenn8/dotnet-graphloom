using GraphLoom.Mappers.Rdf.R2rml;
using NUnit.Framework;

namespace GraphLoom.UnitTests.Rdf.R2rml
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
        public void WhenSourceGiven_ThenReturnNonNullString()
        {
            string result = _logicalTable.SourceName;
            Assert.That(result, Is.Not.Null);
        }
    }
}
