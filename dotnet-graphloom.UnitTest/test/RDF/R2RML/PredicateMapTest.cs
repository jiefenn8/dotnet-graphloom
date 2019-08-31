using GraphLoom.Mapper.RDF.R2RML;
using NUnit.Framework;
using System;
using VDS.RDF;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    [TestFixture]
    public class PredicateMapTest
    {
        private PredicateMap _predicateMap;
        private NodeFactory _nodeFactory;
        private IUriNode _expectedPredicate;

        [SetUp]
        public void SetUp()
        {
            _nodeFactory = new NodeFactory();
            _expectedPredicate = _nodeFactory.CreateUriNode(new Uri("http://example.com/ns#Predicate_1"));
            _predicateMap = new PredicateMap(_expectedPredicate);
        }

        [Test]
        public void WhenGetRelationTerm_ShouldReturnExpectedUri()
        {
            IUriNode result = _predicateMap.GetRelationTerm();
            Assert.That(result, Is.EqualTo(_expectedPredicate));
        }
    }
}
