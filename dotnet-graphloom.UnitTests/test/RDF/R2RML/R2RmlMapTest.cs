using GraphLoom.Mappers.Api;
using GraphLoom.Mappers.Rdf.R2rml;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using VDS.RDF;

namespace GraphLoom.UnitTests.Rdf.R2rml
{
    [TestFixture]
    public class R2rmlMapTest
    {
        private R2rmlMap _r2rmlMap;

        [Test]
        public void WhenNoNsMapGiven_ThenReturnMap()
        {
            _r2rmlMap = new R2rmlMap(null, null);
            INamespaceMapper result = _r2rmlMap.NamespaceMapper;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void WhenPopulatedNsMapGiven_ThenReturnNonEmptyMap()
        {
            INamespaceMapper populatedNsMap = new NamespaceMapper();
            populatedNsMap.AddNamespace("rr", new Uri("http://www.w3.org/ns/r2rml#"));
            _r2rmlMap = new R2rmlMap(null, populatedNsMap);
            bool result = _r2rmlMap.NamespaceMapper.Prefixes.Any();
            Assert.That(result, Is.True);
        }

        [Test]
        public void WhenEntityMapGiven_ThenReturnNonEmptyList()
        {
            _r2rmlMap = new R2rmlMap(null, null);
            _r2rmlMap.AddEntityMap(Mock.Of<IEntityMap>());
            bool result = _r2rmlMap.ListEntityMaps().Any();
            Assert.That(result, Is.True);
        }

        [Test]
        public void WhenBaseUriGiven_ThenReturnUri()
        {
            Uri expectedNamespace = new Uri("http://www.example.com");
            _r2rmlMap = new R2rmlMap(expectedNamespace, null);
            Uri result = _r2rmlMap.BaseUri;
            Assert.That(result, Is.Not.Null);
        }
    }
}
