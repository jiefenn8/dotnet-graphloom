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
        private IStatementAssembler StatementAssembler;
        private IInputSource InputSourceMock;
        private IStatementsConfig StatementsConfigMock;
        private IDictionary<string, string> FakeNamespaceMap;

        [SetUp]
        public void SetUp()
        {
            StatementAssembler = new T();
            InputSourceMock = Mock.Of<IInputSource>();
            StatementsConfigMock = Mock.Of<IStatementsConfig>();

            Mock.Get(InputSourceMock).Setup(f => f.GetEntityRecords(It.IsAny<string>()))
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

            FakeNamespaceMap = new Dictionary<string, string>()
            {
                { "rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#"},
                { "ex", "http://www.example.org/ns#" }
            };

            IRelationConfig relationConfigMock = Mock.Of<IRelationConfig>();
            Mock.Get(relationConfigMock).Setup(f => f.GetRelationName()).Returns("ex:name");
            IObjectsConfig objectsConfigMock = Mock.Of<IObjectsConfig>();
            Mock.Get(objectsConfigMock).Setup(f => f.GetSourceName()).Returns("ENAME");
            Mock.Get(StatementsConfigMock).Setup(f => f.GetClassName()).Returns("ex:Employee");
            Mock.Get(StatementsConfigMock).Setup(f => f.GetRelationObjectConfigs()).Returns(new Dictionary<IRelationConfig, IObjectsConfig>() { { relationConfigMock, objectsConfigMock } });
            Mock.Get(StatementsConfigMock).Setup(f => f.GetTemplate()).Returns("http://www.example.org/employee/{EMPNO}");
        }

        [Test]
        public void WhenAssembleStatementsInvoked_ShouldReturnGraph()
        {
            IGenericGraph result = StatementAssembler.AssembleEntityStatements(InputSourceMock, StatementsConfigMock, FakeNamespaceMap);
            Assert.That(result, Is.Not.Null);
        }

    }
}
