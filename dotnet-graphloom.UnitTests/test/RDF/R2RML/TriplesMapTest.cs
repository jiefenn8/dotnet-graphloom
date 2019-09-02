using GraphLoom.Mappers.Api;
using GraphLoom.Mappers.Rdf.R2rml;
using Moq;
using NUnit.Framework;
using System.Linq;
using VDS.RDF;

namespace GraphLoom.UnitTests.Rdf.R2rml
{
    [TestFixture]
    public class TriplesMapTest
    {
        private TriplesMap _triplesMap;

        [Test]
        public void WhenPredicateAndObjectMapGiven_ThenReturnNonEmptyList()
        {
            _triplesMap = new TriplesMap(null, null);
            _triplesMap.AddRelationNodePair(Mock.Of<IRelationMap>(), Mock.Of<INodeMap>());
            bool result = _triplesMap.ListRelationMaps().Any();
            Assert.That(result, Is.True);
        }

        public void WhenPredicateAndObjectMapGiven_ThenReturnTrue()
        {
            _triplesMap = new TriplesMap(null, null);
            _triplesMap.AddRelationNodePair(Mock.Of<PredicateMap>(), Mock.Of<ObjectMap>());
            bool result = _triplesMap.HasRelationNodeMaps();
            Assert.That(result, Is.True);
        }

        public void WhenPredicateMapGiven_ThenReturnNodeMap()
        {
            PredicateMap predicateMap = new PredicateMap(Mock.Of<IUriNode>());
            _triplesMap = new TriplesMap(null, null);
            _triplesMap.AddRelationNodePair(predicateMap, Mock.Of<INodeMap>());
            INodeMap result = _triplesMap.GetNodeMapWithRelation(predicateMap);
            Assert.That(result, Is.Not.Null);
        }
    }
}
