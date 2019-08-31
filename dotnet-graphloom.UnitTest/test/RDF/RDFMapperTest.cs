using GraphLoom.Mapper.Api;
using GraphLoom.Mapper.Exceptions;
using GraphLoom.Mapper.RDF;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.UnitTest.Api
{
    [TestFixture]
    public class RDFMapperTest
    {
        private IGraphMapper _graphMapper;
        private IInputSource _mockInputSource;
        private IConfigMaps _mockConfigMaps;

        [SetUp]
        public void SetUp()
        {
            //Mock creation
            _mockInputSource = Mock.Of<IInputSource>();
            _mockConfigMaps = Mock.Of<IConfigMaps>();

            //SUT instance creation
            _graphMapper = new RDFMapper();

            //Default mock behaviour setup
            Mock.Get(_mockConfigMaps)
                .Setup(f => f.NamespaceMapper)
                .Returns(Mock.Of<INamespaceMapper>());

            Mock.Get(_mockConfigMaps)
                .Setup(f => f.ListEntityMaps())
                .Returns(new List<IEntityMap>().AsReadOnly());
        }

        [Test]
        public void WhenMappingSucceed_ShouldReturnGraph()
        {
            IGraph result = _graphMapper.MapToGraph(_mockInputSource, _mockConfigMaps);
            Assert.That(result, Is.Not.Null);
        }   

        [Test]
        public void WhenNoInputSource_ShouldThrowException()
        {
            string expected = "Cannot retrieve source data from null input source.";
            Assert.Throws(Is.TypeOf<MapperException>().And.Message.EqualTo(expected),
                () => _graphMapper.MapToGraph(null, _mockConfigMaps));
        }

        [Test]
        public void WhenNoConfigMaps_ShouldThrowException()
        {
            string expected = "Cannot map source from null config maps.";
            Assert.Throws(Is.TypeOf<MapperException>().And.Message.EqualTo(expected),
            () => _graphMapper.MapToGraph(_mockInputSource, null));
        }
    }
}
