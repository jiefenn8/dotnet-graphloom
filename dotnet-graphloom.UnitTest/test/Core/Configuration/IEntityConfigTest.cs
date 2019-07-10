using GraphLoom.Mapper.Configuration;
using NUnit.Framework;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    public class IEntityConfigTest<T> where T : IEntityConfig, new()
    {
        private IEntityConfig EntityConfig;

        [SetUp]
        public void SetUp()
        {
            EntityConfig = new T();
        }

        [Test]
        public void WhenHaveTemplate_ShouldReturnPopulatedString()
        {
            string template = "template";
            EntityConfig.SetTemplate(template);
            string result = EntityConfig.GetTemplate();
            Assert.That(result, Is.EqualTo(template));
        }

        [Test]
        public void WhenHaveClassName_ShouldReturnPopulatedString()
        {
            string className = "class_name";
            EntityConfig.SetClassName(className);
            string result = EntityConfig.GetClassName();
            Assert.That(result, Is.EqualTo(className));
        }
    }
}
