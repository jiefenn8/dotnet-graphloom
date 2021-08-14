using GraphLoom.Mapper.RDF;
using Moq;
using NUnit.Framework;

namespace GraphLoom.UnitTest.Mapper.RDF
{
    [TestFixture]
    public class RDFMapperFactoryTest
    {
        [Test]
        public void WhenCreateDefauyltInstance_ShouldReturnInstance()
        {
            RDFGraphMapper result = RDFMapperFactory.CreateDefaultRDFMapper();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void WhenCreateDefaultWithAssembler_ShouldReturnInstance()
        {
            RDFGraphMapper result = RDFMapperFactory.CreateRDFMapperWithAssembler(Mock.Of<TriplesAssembler>());
            Assert.That(result, Is.Not.Null);
        }

    }
}
