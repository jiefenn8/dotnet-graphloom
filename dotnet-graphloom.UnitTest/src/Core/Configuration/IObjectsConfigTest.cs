using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF.R2RML;
using NUnit.Framework;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    [TestFixture(typeof(ObjectMap))]
    public class IObjectsConfigTest<T> where T : IObjectsConfig, new()
    {
        private IObjectsConfig _objectsConfig;

        [SetUp]
        public void SetUp()
        {
            _objectsConfig = new T();
        }

        [Test]
        public void WhenHaveSourceName_ShouldReturnPopulatedString()
        {
            string objectName = "object_name";
            _objectsConfig.SetSourceName(objectName);
            string result = _objectsConfig.GetSourceName();
            Assert.That(result, Is.EqualTo(objectName));
        }
    }
}
