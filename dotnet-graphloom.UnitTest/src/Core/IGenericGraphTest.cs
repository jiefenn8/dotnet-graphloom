﻿using GraphLoom.Mapper;
using GraphLoom.Mapper.RDF;
using NUnit.Framework;
using VDS.RDF;

namespace GraphLoom.UnitTest.Mapper
{
    [TestFixture(typeof(VDSGraph))]
    public class IGenericGraphTest<T> where T : IGenericGraph, new()
    {
        private IGenericGraph _genericGraph;
        private IGenericGraph _fakeGraph;

        [SetUp]
        public void SetUp()
        {
            IGraph test = new Graph();
            _genericGraph = new T();
            _fakeGraph = new T();
        }

        [Test]
        public void WhenGraphMerge_ShouldNotThrowException()
        {
            _genericGraph.Merge(_fakeGraph);
            bool result = _genericGraph.IsEmpty;
            Assert.That(result, Is.True);
        }

    }
}
