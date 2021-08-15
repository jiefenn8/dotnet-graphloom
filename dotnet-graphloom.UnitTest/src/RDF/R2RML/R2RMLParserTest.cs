using GraphLoom.Mapper.RDF.R2RML;
using GraphLoom.Mapper.RDF.Wrapper;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    [TestFixture]
    public class R2RMLParserTest
    {
        private R2RMLParser _r2rmlParser;
        private IRdfLoader _rdfLoader;
        private TurtleParser _turtleParser;

        private readonly Uri _baseUri = new Uri("http://www.example.com");
        private readonly string _dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private readonly string _validFile = "/resources/data/valid_r2rml.ttl";
        private readonly string _emptyFile = "/resources/data/empty_r2rml.ttl";
        private readonly string _prefixOnlyFIle = "/resources/data/only_prefix_r2rml.ttl";
        private readonly string _examplePrefix = "ex";
        private readonly Uri _examplePrefixUri = UriFactory.Create("http://www.example.com/ns#");

        [SetUp]
        public void SetUp()
        {

            _rdfLoader = Mock.Of<IRdfLoader>();
            _turtleParser = Mock.Of<TurtleParser>();
            _r2rmlParser = new R2RMLParser(_rdfLoader, _turtleParser);

            Mock.Get(_rdfLoader).Setup(f => f.LoadFromFile(It.IsAny<Graph>(), It.Is<string>(s => !s.Equals(_dirPath + _emptyFile)), _turtleParser)).Throws<FileNotFoundException>();
            Mock.Get(_rdfLoader).Setup(f => f.LoadFromFile(It.IsAny<Graph>(), _dirPath + _prefixOnlyFIle, _turtleParser))
                .Callback<IGraph, string, IRdfReader>((graph, filename, rdfReader) =>
                {
                    graph.NamespaceMap.AddNamespace("rr", UriFactory.Create("http://www.w3.org/ns/r2rml#"));
                    graph.NamespaceMap.AddNamespace(_examplePrefix, _examplePrefixUri);
                });
            Mock.Get(_rdfLoader).Setup(f => f.LoadFromFile(It.IsAny<Graph>(), _dirPath + _validFile, _turtleParser))
                .Callback<IGraph, string, IRdfReader>((graph, filename, rdfReader) =>
                {
                    if (graph.IsEmpty && graph.BaseUri == null) graph.BaseUri = UriFactory.Create("file:///" + filename);
                    IUriNode triplesMap = graph.CreateUriNode(UriFactory.Create(graph.BaseUri.ToString() + "#TriplesMap1"));

                    graph.NamespaceMap.AddNamespace("rr", UriFactory.Create("http://www.w3.org/ns/r2rml#"));
                    graph.NamespaceMap.AddNamespace(_examplePrefix, _examplePrefixUri);
                    graph.Assert(triplesMap, graph.CreateUriNode("rr:logicalTable"), graph.CreateBlankNode("ltNode"));
                    graph.Assert(graph.GetBlankNode("ltNode"), graph.CreateUriNode("rr:tableName"), graph.CreateLiteralNode("EMP"));
                    graph.Assert(triplesMap, graph.CreateUriNode("rr:subjectMap"), graph.CreateBlankNode("smNode"));
                    graph.Assert(graph.GetBlankNode("smNode"), graph.CreateUriNode("rr:template"), graph.CreateLiteralNode("http://www.example.com/employee/{EMPNO}"));
                    graph.Assert(graph.GetBlankNode("smNode"), graph.CreateUriNode("rr:class"), graph.CreateUriNode("ex:Employee"));
                    graph.Assert(triplesMap, graph.CreateUriNode("rr:predicateObjectMap"), graph.CreateBlankNode("pomNode"));
                    graph.Assert(graph.GetBlankNode("pomNode"), graph.CreateUriNode("rr:predicate"), graph.CreateUriNode("ex:name"));
                    graph.Assert(graph.GetBlankNode("pomNode"), graph.CreateUriNode("rr:objectMap"), graph.CreateBlankNode("omNode"));
                    graph.Assert(graph.GetBlankNode("omNode"), graph.CreateUriNode("rr:column"), graph.CreateLiteralNode("ENAME"));
                });
        }

        [Test]
        public void WhenGivenValidFilePath_ShouldReturnR2RMLMap()
        {
            R2RMLMap result = _r2rmlParser.Parse(_dirPath + _validFile, _baseUri);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void WhenGivenInvalidFilePath_ShouldThrowException()
        {
            Assert.Throws<FileNotFoundException>(() => _r2rmlParser.Parse("invalid_filepath.ttl", _baseUri));
        }

        [Test]
        public void WhenGivenEmptyFileData_ShouldReturnNull()
        {
            R2RMLMap result = _r2rmlParser.Parse(_dirPath + _emptyFile, _baseUri);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void WhenGivenOnlyPrefixFileData_ShouldReturnNull()
        {
            R2RMLMap result = _r2rmlParser.Parse(_dirPath + _prefixOnlyFIle, _baseUri);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void WhenParseValidFile_ShouldHaveValidClassQName()
        {
            IGraph graph = new Graph();
            graph.NamespaceMap.AddNamespace(_examplePrefix, _examplePrefixUri);
            R2RMLMap r2rmlMap = _r2rmlParser.Parse(_dirPath + _validFile, _baseUri);
            string qname = r2rmlMap.ListStatementsConfigs().First().GetClassName();
            bool result = graph.ResolveQName(qname).IsAbsoluteUri;
            Assert.That(result, Is.True);
        }

        [Test]
        public void WhenParseValidFile_ShouldHaveValidPredicateQName()
        {
            IGraph graph = new Graph();
            graph.NamespaceMap.AddNamespace(_examplePrefix, _examplePrefixUri);
            R2RMLMap r2rmlMap = _r2rmlParser.Parse(_dirPath + _validFile, _baseUri);
            string qName = r2rmlMap.ListStatementsConfigs().First().GetRelationObjectConfigs().First().Key.GetRelationName();
            bool result = graph.ResolveQName(qName).IsAbsoluteUri;
            Assert.That(result, Is.True);
        }
    }
}
