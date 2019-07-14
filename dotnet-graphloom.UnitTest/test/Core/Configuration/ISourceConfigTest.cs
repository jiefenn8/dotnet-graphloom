using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF.Configuration;
using NUnit.Framework;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    [TestFixture(typeof(TriplesMap))]
    public class ISourceConfigTest<T> where T : ISourceConfig, new()
    {
        private ISourceConfig SourceConfig;

        [SetUp]
        public void SetUp()
        {
            SourceConfig = new T();
        }

        [Test]
        public void WhenHaveSourceName_ShouldReturnPopulatedString()
        {
            string sourceName = "source_name";
            SourceConfig.SetSourceName(sourceName);
            string result = SourceConfig.GetSourceName();
            Assert.That(result, Is.EqualTo(sourceName));
        }

    }
}
