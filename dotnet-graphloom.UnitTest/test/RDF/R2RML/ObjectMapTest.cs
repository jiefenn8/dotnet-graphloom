using GraphLoom.RDF.R2RML;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    [TestFixture]
    public class ObjectMapTest
    {
        private ObjectMap _objectMap;
        private readonly string _columnName = "Col_1_Name";
        private IReadOnlyDictionary<string, string> _mockRecord;

        [SetUp]
        public void SetUp()
        {
            _objectMap = new ObjectMap(_columnName);
            _mockRecord = Mock.Of<IReadOnlyDictionary<string, string>>();
        }

        [Test]
        public void WhenGenerateNodeTerm_ShouldReturnExpectedNode()
        {
            string expectedValue = "Col_1_Val";
            Mock.Get(_mockRecord)
                .Setup(f => f[It.IsAny<string>()])
                .Returns(expectedValue);

            ILiteralNode term = (ILiteralNode)_objectMap.GenerateNodeTerm(_mockRecord);
            string result = term.Value;
            Assert.That(result, Is.EqualTo(expectedValue));
        }

    }
}
