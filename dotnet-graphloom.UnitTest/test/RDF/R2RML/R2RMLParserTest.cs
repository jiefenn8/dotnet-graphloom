using GraphLoom.Mapper.RDF.R2RML;
using GraphLoom.Mapper.RDF.Wrapper;
using Moq;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    [TestFixture]
    public class R2RMLParserTest
    {
        private R2RMLParser Parser;
        private IRdfLoader _rdfLoader;
        private TurtleParser _turtleParser;

        private readonly Uri BaseUri = new Uri("http://www.example.com");
        private readonly string DirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private readonly string ValidFile = "/TestData/valid_r2rml.ttl";
        private readonly string EmptyFile = "/TestData/empty_r2rml.ttl";
        private readonly string PrefixOnlyFile = "/TestData/only_prefix_r2rml.ttl";

        [SetUp]
        public void SetUp()
        {

            _rdfLoader = Mock.Of<IRdfLoader>();
            _turtleParser = Mock.Of<TurtleParser>();
            Parser = new R2RMLParser(_rdfLoader, _turtleParser);

            Mock.Get(_rdfLoader).Setup(f => f.LoadFromFile(It.IsAny<Graph>(), It.Is<string>(s => !s.Equals(DirPath + EmptyFile)), _turtleParser)).Throws<FileNotFoundException>();
            Mock.Get(_rdfLoader).Setup(f => f.LoadFromFile(It.IsAny<Graph>(), DirPath + PrefixOnlyFile, _turtleParser))
                .Callback<IGraph, string, IRdfReader>((graph, filename, rdfReader) =>
                {
                    graph.NamespaceMap.AddNamespace("rr", UriFactory.Create("http://www.w3.org/ns/r2rml#"));
                    graph.NamespaceMap.AddNamespace("ex", UriFactory.Create("http://www.example.com/ns#"));
                });
            Mock.Get(_rdfLoader).Setup(f => f.LoadFromFile(It.IsAny<Graph>(), DirPath + ValidFile, _turtleParser))
                .Callback<IGraph, string, IRdfReader>((graph, filename, rdfReader) => 
                {
                    if(graph.IsEmpty && graph.BaseUri == null) graph.BaseUri = UriFactory.Create("file:///" + filename);
                    IUriNode triplesMap = graph.CreateUriNode(UriFactory.Create(graph.BaseUri.ToString() + "#TriplesMap1"));

                    graph.NamespaceMap.AddNamespace("rr", UriFactory.Create("http://www.w3.org/ns/r2rml#"));
                    graph.NamespaceMap.AddNamespace("ex", UriFactory.Create("http://www.example.com/ns#"));              
                    graph.Assert(triplesMap, graph.CreateUriNode("rr:logicalTable"), graph.CreateBlankNode("ltNode"));
                    graph.Assert(graph.GetBlankNode("ltNode"), graph.CreateUriNode("rr:tableName"), graph.CreateLiteralNode("EMP"));
                    graph.Assert(triplesMap, graph.CreateUriNode("rr:subjectMap"), graph.CreateBlankNode("smNode"));
                    graph.Assert(graph.GetBlankNode("smNode"), graph.CreateUriNode("rr:template"), graph.CreateLiteralNode("http://www.example.com/employee/{EMPNO}"));
                    graph.Assert(graph.GetBlankNode("smNode"), graph.CreateUriNode("rr:class"), graph.CreateUriNode("ex:Employee"));
                });
        }

        [Test]
        public void WhenGivenValidFilePath_ShouldReturnR2RMLMap()
        {
            R2RMLMap result = Parser.Parse(DirPath + ValidFile, BaseUri);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void WhenGivenInvalidFilePath_ShouldThrowException()
        {
            Assert.Throws<FileNotFoundException>(() => Parser.Parse("invalid_filepath.ttl", BaseUri));
        }

        [Test]
        public void WhenGivenEmptyFileData_ShouldReturnNull()
        {
            R2RMLMap result = Parser.Parse(DirPath + EmptyFile, BaseUri);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void WhenGivenOnlyPrefixFileData_ShouldReturnNull()
        {
            R2RMLMap result = Parser.Parse(DirPath + PrefixOnlyFile, BaseUri);
            Assert.That(result, Is.Null);
        }
    }
}
