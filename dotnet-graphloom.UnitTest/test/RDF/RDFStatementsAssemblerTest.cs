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
    public class RDFStatementsAssemblerTest
    {
        private RDFStatementsAssembler StatementsAssembler;
        private IInputSource InputSourceMock;
        private IStatementsConfig StatementsConfigMock;
        private IGraph ExpGraph;

        [SetUp]
        public void SetUp()
        {
            StatementsAssembler = new RDFStatementsAssembler();
            InputSourceMock = Mock.Of<IInputSource>();
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

            StatementsConfigMock = Mock.Of<IStatementsConfig>();
            IRelationConfig relationConfigMock = Mock.Of<IRelationConfig>();
            Mock.Get(relationConfigMock).Setup(f => f.GetRelationName()).Returns("ex:name");
            Mock.Get(relationConfigMock).Setup(f => f.GetSourceName()).Returns("ENAME");
            Mock.Get(StatementsConfigMock).Setup(f => f.GetClassName()).Returns("ex:Employee");
            Mock.Get(StatementsConfigMock).Setup(f => f.ListRelationConfigs()).Returns(new List<IRelationConfig>() { relationConfigMock });
            Mock.Get(StatementsConfigMock).Setup(f => f.GetTemplate()).Returns("http://www.example.org/employee/{EMPNO}");
            
            ExpGraph = new Graph();
            ExpGraph.NamespaceMap.AddNamespace("ex", new Uri("http://www.example.org/ns#"));
            ExpGraph.NamespaceMap.AddNamespace("rdf", new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#"));
            IUriNode subject = ExpGraph.CreateUriNode(new Uri("http://www.example.org/employee/7369"));
            ExpGraph.Assert(subject, ExpGraph.CreateUriNode("rdf:type"), ExpGraph.CreateUriNode("ex:Employee"));
            ExpGraph.Assert(subject, ExpGraph.CreateUriNode("ex:name"), ExpGraph.CreateLiteralNode("SMITH"));
        }

        [Test]
        public void WhenAssembleStmtSucceed_ShouldReturnExpectedGraph()
        {
            IGraph result = StatementsAssembler.AssembleSubjectStatements(InputSourceMock, StatementsConfigMock, ExpGraph.NamespaceMap);
            Assert.That(result, Is.EqualTo(ExpGraph));
        }

    }
}
