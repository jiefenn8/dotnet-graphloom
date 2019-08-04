using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF.Configuration;
using NUnit.Framework;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    [TestFixture(typeof(TriplesMap))]
    public class ISourceConfigTest<T> where T : ISourceConfig, new()
    {
        private ISourceConfig _sourceConfig;

        [SetUp]
        public void SetUp()
        {
            _sourceConfig = new T();
        }

        [Test]
        public void WhenHaveSourceName_ShouldReturnPopulatedString()
        {
            string sourceName = "source_name";
            _sourceConfig.SetSourceName(sourceName);
            string result = _sourceConfig.GetSourceName();
            Assert.That(result, Is.EqualTo(sourceName));
        }

    }
}
