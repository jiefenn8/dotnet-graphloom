using GraphLoom.Mappers.Api;
using GraphLoom.Mappers.Exceptions;
using GraphLoom.Mappers.Rdf;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.UnitTests.Rdf
{
    [TestFixture]
    public class RdfMapperTest
    {
        private RdfMapper _rdfMapper;
        private IInputSource _mockInputSource;
        private IConfigMaps _mockConfigMaps;

        [SetUp]
        public void SetUp()
        {
            //Mock creation
            _mockInputSource = Mock.Of<IInputSource>();
            _mockConfigMaps = Mock.Of<IConfigMaps>();

            //SUT instance creation
            _rdfMapper = new RdfMapper();

            //Default mock behaviour setup
            Mock.Get(_mockConfigMaps)
                .Setup(f => f.NamespaceMapper)
                .Returns(Mock.Of<INamespaceMapper>());

            Mock.Get(_mockConfigMaps)
                .Setup(f => f.ListEntityMaps())
                .Returns(new List<IEntityMap>().AsReadOnly());
        }

        [Test]
        public void WhenSourceAndConfigGiven_ShouldReturnGraph()
        {
            IGraph result = _rdfMapper.MapToGraph(_mockInputSource, _mockConfigMaps);
            Assert.That(result, Is.Not.Null);
        }   

        [Test]
        public void WhenNoInputSourceGiven_ThenThrowException()
        {
            string expected = "Cannot retrieve source data from null input source.";
            Assert.Throws(Is.TypeOf<MapperException>().And.Message.EqualTo(expected),
                () => _rdfMapper.MapToGraph(null, _mockConfigMaps));
        }

        [Test]
        public void WhenNoConfigMapsGiven_ThenThrowException()
        {
            string expected = "Cannot map source from null config maps.";
            Assert.Throws(Is.TypeOf<MapperException>().And.Message.EqualTo(expected),
            () => _rdfMapper.MapToGraph(_mockInputSource, null));
        }
    }
}
