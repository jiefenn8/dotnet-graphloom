using GraphLoom.Mapper.RDF;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.UnitTest.Mapper.RDF
{
    [TestFixture]
    public class NamespaceHelperTest
    {
        private NamespaceHelper _namespaceHelper;

        [SetUp]
        public void SetUp()
        {
            _namespaceHelper = new NamespaceHelper();
        }

        [Test]
        public void WhenMapToVDS_ShouldReturnVDSNamespaceMap()
        {
            IDictionary<string, string> nsMap = new Dictionary<string, string>()
                {
                    { "ex", "http://www.example.org/ns#"},
                };

            INamespaceMapper result = NamespaceHelper.ToIGraphNamespaceMap(nsMap);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void WhenMapToDictionary_ShouldReturnDictionary()
        {
            INamespaceMapper nsMap = new NamespaceMapper();
            nsMap.AddNamespace("ex", new Uri("http://www.example.org/ns#"));

            IDictionary<string, string> result = NamespaceHelper.ToStringDictionary(nsMap);

            Assert.That(result, Is.Not.Null);
        }
    }
}
