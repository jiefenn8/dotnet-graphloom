using GraphLoom.Mappers.Exceptions;
using GraphLoom.Mappers.Rdf.Parsers;
using GraphLoom.Mappers.Rdf.R2rml;
using GraphLoom.Mappers.Rdf.Wrapper;
using Moq;
using NUnit.Framework;
using System.IO;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace GraphLoom.UnitTests.Rdf.Parsers
{
    [TestFixture]
    public class R2rmlParserTest
    {
        private R2rmlParser _r2rmlParser;
        private IGraph _mockGraph;
        private IRdfLoader _mockRdfLoader;
        private readonly string _fileName = "r2rml_file.ttl";

        [SetUp]
        public void SetUp()
        {
            //Mock creation
            _mockGraph = Mock.Of<IGraph>();
            _mockRdfLoader = Mock.Of<IRdfLoader>();

            //SUT instance creation
            _r2rmlParser = new R2rmlParser(_mockRdfLoader);

            //Default mock behaviour setup
            Mock.Get(_mockRdfLoader)
                .Setup(f => f.LoadFromFile(It.IsAny<IGraph>(), It.IsAny<string>(), It.IsAny<IRdfReader>()))
                .Returns(_mockGraph);

            Mock.Get(_mockGraph)
                .Setup(f => f.NamespaceMap)
                .Returns(Mock.Of<INamespaceMapper>());

            Mock.Get(_mockGraph)
                .Setup(f => f.NamespaceMap.HasNamespace(It.IsAny<string>()))
                .Returns(true);

            Mock.Get(_mockGraph)
                .Setup(f => f.GetUriNode(It.IsAny<string>()))
                .Returns(Mock.Of<IUriNode>());
        }

        [Test]
        public void WhenValidFileGiven_ThenReturnR2RMLMap()
        {
            R2rmlMap result = _r2rmlParser.Parse(_fileName, null);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void WhenInvalidFilePathGiven_ThenThrowException()
        {
            string invalid_path = "invalid_filepath.ttl";
            Mock.Get(_mockRdfLoader)
                .Setup(f => f.LoadFromFile(It.IsAny<IGraph>(), invalid_path, It.IsAny<TurtleParser>()))
                .Throws<FileNotFoundException>();

            Assert.Throws<FileNotFoundException>(() => _r2rmlParser.Parse(invalid_path, null));
        }

        [Test]
        public void WhenNoNamespaceGiven_ThenThrowException()
        {
            string expected = "'rr' prefix uri not found.";
            Mock.Get(_mockGraph)
                .Setup(f => f.NamespaceMap.HasNamespace(It.IsAny<string>()))
                .Returns(false);

            Assert.Throws(Is.TypeOf<ParserException>().And.Message.EqualTo(expected),
                () => _r2rmlParser.Parse(_fileName, null));
        }

        [Test]
        public void WhenNoTriplesMapGiven_ThenThrowException()
        {
            string expected = "No valid Triples Map with rr:logicalTable found.";
            Mock.Get(_mockGraph)
                .Setup(f => f.GetUriNode(It.Is<string>((s) => s.Equals("rr:logicalTable"))))
                .Returns<IUriNode>(null);

            Assert.Throws(Is.TypeOf<ParserException>().And.Message.EqualTo(expected),
                () => _r2rmlParser.Parse(_fileName, null));
        }
    }
}
