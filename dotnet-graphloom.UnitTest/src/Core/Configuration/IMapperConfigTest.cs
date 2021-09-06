using System.Linq;
using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF.R2RML;
using Moq;
using NUnit.Framework;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    [TestFixture(typeof(R2RMLMap))]
    public class IMapperConfigTest<T> where T : IMapperConfig, new()
    {
        private IMapperConfig _mapperConfig;

        [SetUp]
        public void SetUp()
        {
            _mapperConfig = new T();
        }

        [Test]
        public void WhenHaveNamespaces_ShouldReturnPopulatedMap()
        {
            _mapperConfig.AddNamespace("prefix", "namespace");
            bool result = _mapperConfig.ListNamespaces().Any();
            Assert.That(result, Is.True);
        }

        [Test]
        public void WhenHaveStatementsConfigs_ShouldReturnPopulatedList()
        {
            _mapperConfig.AddStatementsConfig(Mock.Of<IStatementsConfig>());
            bool result = _mapperConfig.ListStatementsConfigs().Any();
            Assert.That(result, Is.True);
        }

    }
}
