using GraphLoom.Mapper;
using GraphLoom.Mapper.RDF;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;

namespace GraphLoom.UnitTest.Mapper
{
    [TestFixture(typeof(VDSGraph))]
    public class IGenericGraphTest<T> where T : IGenericGraph, new()
    {
        private IGenericGraph GenericGraph;
        private IGenericGraph FakeGraph;

        [SetUp]
        public void SetUp()
        {
            IGraph test = new Graph();
            GenericGraph = new T();
            FakeGraph = new T();
        }

        [Test]
        public void WhenGraphMerge_ShouldNotThrowException()
        {
            GenericGraph.Merge(FakeGraph);
            bool result = GenericGraph.IsEmpty;
            Assert.That(result, Is.True);
        }

    }
}
