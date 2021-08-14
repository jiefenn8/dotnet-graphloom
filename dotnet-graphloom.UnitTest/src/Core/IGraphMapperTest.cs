using GraphLoom.Mapper;
using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GraphLoom.UnitTest.Mapper
{
    [TestFixture(typeof(RDFGraphMapper))]
    public class IGraphMapperTest<T> where T : IGraphMapper
    {
        private IGraphMapper _graphMapper;
        private IInputSource _mockInputSource;
        private IMapperConfig _mockMapperConfig;

        [SetUp]
        public void SetUp()
        {
            //Mock creation
            _mockInputSource = Mock.Of<IInputSource>();
            _mockMapperConfig = Mock.Of<IMapperConfig>();
            IStatementsConfig statementsConfig = Mock.Of<IStatementsConfig>();
            IStatementAssembler assemblerMock = Mock.Of<IStatementAssembler>();

            //Mock setups
            Mock.Get(_mockMapperConfig)
                .Setup(f => f.ListNamespaces())
                .Returns(new Dictionary<string, string>()
                {
                    { "prefix1", "ns1"},
                    { "prefix2", "ns2" }
                });

            Mock.Get(_mockMapperConfig)
                .Setup(f => f.ListStatementsConfigs())
                .Returns(new List<IStatementsConfig>() { statementsConfig });

            //Instance creation
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            _graphMapper = (T)Activator.CreateInstance(typeof(T), flags, null, new object[] {assemblerMock}, null);

            //Fake setup
            IGenericGraph fakeGraph = (IGenericGraph)Activator.CreateInstance(_graphMapper.GetGraphType());

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
            
            IGenericGraph result = _graphMapper.MapToGraph(_mockInputSource, _mockMapperConfig);
            Assert.That(result, Is.Not.Null);
        }
    }
}
