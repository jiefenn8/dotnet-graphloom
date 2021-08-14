using GraphLoom.Mapper;
using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace GraphLoom.UnitTest.Mapper
{
    [TestFixture(typeof(TriplesAssembler))]
    public class IStatementAssemblerTest<T> where T : IStatementAssembler, new()
    {
        private IStatementAssembler _statementAssembler;
        private IInputSource _mockInputSource;
        private IStatementsConfig _mockStatementsConfig;
        private IDictionary<string, string> _fakeNamespaceMap;

        [SetUp]
        public void SetUp()
        {
            _statementAssembler = new T();
            _mockInputSource = Mock.Of<IInputSource>();
            _mockStatementsConfig = Mock.Of<IStatementsConfig>();

            Mock.Get(_mockInputSource).Setup(f => f.GetEntityRecords(It.IsAny<string>()))
                .Returns(
                    new List<IDictionary<string, string>>()
                    {
                                    {   new Dictionary<string, string>()
                                        {
                                            { "EMPNO", "7369" },
                                            { "ENAME", "SMITH" },
                                            { "JOB", "CLERK" },
                                            { "DEPTNO", "10" },
                                        }
                                    }
                    }
                );

            _fakeNamespaceMap = new Dictionary<string, string>()
            {
                { "rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#"},
                { "ex", "http://www.example.org/ns#" }
            };

            IRelationConfig relationConfigMock = Mock.Of<IRelationConfig>();
            Mock.Get(relationConfigMock).Setup(f => f.GetRelationName()).Returns("ex:name");
            IObjectsConfig objectsConfigMock = Mock.Of<IObjectsConfig>();
            Mock.Get(objectsConfigMock).Setup(f => f.GetSourceName()).Returns("ENAME");
            Mock.Get(_mockStatementsConfig).Setup(f => f.GetClassName()).Returns("ex:Employee");
            Mock.Get(_mockStatementsConfig).Setup(f => f.GetRelationObjectConfigs()).Returns(new Dictionary<IRelationConfig, IObjectsConfig>() { { relationConfigMock, objectsConfigMock } });
            Mock.Get(_mockStatementsConfig).Setup(f => f.GetTemplate()).Returns("http://www.example.org/employee/{EMPNO}");
        }

        [Test]
        public void WhenAssembleStatementsInvoked_ShouldReturnGraph()
        {
            IGenericGraph result = _statementAssembler.AssembleEntityStatements(_mockInputSource, _mockStatementsConfig, _fakeNamespaceMap);
            Assert.That(result, Is.Not.Null);
        }

    }
}
