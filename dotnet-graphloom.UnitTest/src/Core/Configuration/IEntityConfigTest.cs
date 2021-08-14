using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF.Configuration;
using NUnit.Framework;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    [TestFixture(typeof(TriplesMap))]
    public class IEntityConfigTest<T> where T : IEntityConfig, new()
    {
        private IEntityConfig _entityConfig;

        [SetUp]
        public void SetUp()
        {
            _entityConfig = new T();
        }

        [Test]
        public void WhenHaveTemplate_ShouldReturnPopulatedString()
        {
            string template = "template";
            _entityConfig.SetTemplate(template);
            string result = _entityConfig.GetTemplate();
            Assert.That(result, Is.EqualTo(template));
        }

        [Test]
        public void WhenHaveClassName_ShouldReturnPopulatedString()
        {
            string className = "class_name";
            _entityConfig.SetClassName(className);
            string result = _entityConfig.GetClassName();
            Assert.That(result, Is.EqualTo(className));
        }
    }
}
