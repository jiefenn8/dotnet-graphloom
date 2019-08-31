using GraphLoom.Mapper.RDF.R2RML;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    [TestFixture]
    public class SubjectMapTest
    {
        private SubjectMap _subjectMap;
        private NodeFactory _nodeFactory = new NodeFactory();
        private readonly string _template = "http://www.example.com/Template/{Col_1_Name}";
        private IReadOnlyDictionary<string, string> _mockRecord;

        [SetUp]
        public void SetUp()
        {
            _subjectMap = new SubjectMap(_template);
            _mockRecord = Mock.Of<IReadOnlyDictionary<string, string>>();
        }

        [Test]
        public void WhenGenerateEntityTerm_ShouldReturnUri()
        {
            IUriNode expectedResult = _nodeFactory.CreateUriNode(new Uri("http://www.example.com/Template/Col_1_Val"));
            string value = "Col_1_Val";
            Mock.Get(_mockRecord)
                .Setup(f => f[It.IsAny<string>()])
                .Returns(value);

            IUriNode result = _subjectMap.GenerateEntityTerm(_mockRecord);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

    }
}
