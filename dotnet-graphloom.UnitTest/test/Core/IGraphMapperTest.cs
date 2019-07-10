using GraphLoom.Mapper;
using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace GraphLoom.UnitTest.Mapper
{
    [TestFixture(typeof(RDFGraphMapper))]
    public class IGraphMapperTest<T> where T : IGraphMapper
    {
        private IGraphMapper GraphMapper;
        private IInputSource InputSourceMock;
        private IMapperConfig MapperConfigMock;

        [SetUp]
        public void SetUp()
        {
            //Mock creation
            InputSourceMock = Mock.Of<IInputSource>();
            MapperConfigMock = Mock.Of<IMapperConfig>();
            IStatementsConfig statementsConfig = Mock.Of<IStatementsConfig>();
            IStatementAssembler assemblerMock = Mock.Of<IStatementAssembler>();

            //Mock setups
            Mock.Get(MapperConfigMock)
                .Setup(f => f.ListNamespaces())
                .Returns(new Dictionary<string, string>()
                {
                    { "prefix1", "ns1"},
                    { "prefix2", "ns2" }
                });

            Mock.Get(MapperConfigMock)
                .Setup(f => f.ListStatementsConfigs())
                .Returns(new List<IStatementsConfig>() { statementsConfig });

            //Instance creation
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            GraphMapper = (T)Activator.CreateInstance(typeof(T), flags, null, new object[] {assemblerMock}, null);

            //Fake setup
            IGenericGraph fakeGraph = (IGenericGraph)Activator.CreateInstance(GraphMapper.GetGraphType());

            //Mock setup
            Mock.Get(assemblerMock)
                .Setup(f => f.AssembleEntityStatements(
                    It.IsAny<IInputSource>(),
                    It.IsAny<IStatementsConfig>(),
                    It.IsAny<IDictionary<string, string>>()))
                .Returns(fakeGraph);
        }

        [Test]
        public void WhenMappingSucceed_ShouldReturnGraph()
        {
            
            IGenericGraph result = GraphMapper.MapToGraph(InputSourceMock, MapperConfigMock);
            Assert.That(result, Is.Not.Null);
        }
    }
}
