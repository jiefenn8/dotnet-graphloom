using GraphLoom.Mapper.RDF.R2RML;
using Moq;
using NUnit.Framework;
using System;
using VDS.RDF;

namespace GraphLoom.UnitTest.test.RDF.R2RML
{
    [TestFixture]
    public class R2RMLMapTest
    {
        private R2RMLMap _r2rmlMap;

        [SetUp]
        public void SetUp()
        {
            //SUT instance creation
            Uri expectedNamespace = new Uri("http://www.w3.org/ns/r2rml#");
            _r2rmlMap = new R2RMLMap(expectedNamespace, null);
        }

        [Test]
        public void WhenGetNamespaceMap_ShouldReturnNonNull()
        {
            INamespaceMapper result = _r2rmlMap.NamespaceMapper;
            Assert.That(result, Is.Not.Null);
        }
    }
}
