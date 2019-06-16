using GraphLoom.Mapper;
using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.UnitTest.Mapper.RDF
{
    [TestFixture]
    public class RDFGraphMapperTest
    {
        private RDFGraphMapper GraphMapper;
        private RDFStatementsAssembler RdfAssembler;
        private IInputSource InputSourceMock;
        private IMapperConfig MapperConfigMock;
        private IGraph ExpGraph;

        [SetUp]
        public void SetUp()
        {
            //Instance creation
            RdfAssembler = Mock.Of<RDFStatementsAssembler>();
            GraphMapper = new RDFGraphMapper(RdfAssembler);
            ExpGraph = new Graph();

            //Expected graph setup
            ExpGraph.NamespaceMap.AddNamespace("ex", new Uri("http://www.example.org/ns#"));
            ExpGraph.NamespaceMap.AddNamespace("rdf", new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#"));
            IUriNode subject = ExpGraph.CreateUriNode(new Uri("http://www.example.org/employee/7369"));
            ExpGraph.Assert(subject, ExpGraph.CreateUriNode("rdf:type"), ExpGraph.CreateUriNode("ex:Employee"));
            ExpGraph.Assert(subject, ExpGraph.CreateUriNode("ex:name"), ExpGraph.CreateLiteralNode("SMITH"));

            //Mock creation
            InputSourceMock = Mock.Of<IInputSource>();
            MapperConfigMock = Mock.Of<IMapperConfig>();
            IStatementsConfig statementsConfig = Mock.Of<IStatementsConfig>();

            //Mock setups
            Mock.Get(MapperConfigMock)
                .Setup(f => f.ListNamespaces())
                .Returns(new Dictionary<string, string>()
                {
                    { "rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#"},
                    { "ex", "http://www.example.org/ns#" }
                });

            Mock.Get(MapperConfigMock)
                .Setup(f => f.ListStatementsConfigs())
                .Returns(new List<IStatementsConfig>() { statementsConfig });

            Mock.Get(RdfAssembler)
                .Setup(f => f.AssembleSubjectStatements(
                    It.IsAny<IInputSource>(),
                    It.IsAny<IStatementsConfig>(),
                    It.IsAny<INamespaceMapper>()))
                .Returns(ExpGraph);
        }

        [Test]
        public void WhenMappingSucceed_ShouldReturnPopulatedGraph()
        {
            IGraph result = GraphMapper.MapToGraph(InputSourceMock, MapperConfigMock);
            Assert.That(result, Is.EqualTo(ExpGraph));
        }
        
        [Test]
        public void WhenMappingWithNullInputSource_ShouldReturnEmptyGraph()
        {
            IGraph result = GraphMapper.MapToGraph(null, MapperConfigMock);
            Assert.That(result, Is.Not.Null);             
        }

        [Test]
        public void WhenMappingWithNullMapperConfig_ShouldReturnEmptyGraph()
        {
            IGraph result = GraphMapper.MapToGraph(InputSourceMock, null);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void WhenMappingWithBothNullInputs_ShouldReturnEmptyGraph()
        {
            IGraph result = GraphMapper.MapToGraph(null, null);
            Assert.That(result, Is.Not.Null);
        }
    }
}
