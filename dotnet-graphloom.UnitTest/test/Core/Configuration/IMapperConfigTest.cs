using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF.R2RML;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    [TestFixture(typeof(R2RMLMap))]
    public class IMapperConfigTest<T> where T : IMapperConfig, new()
    {
        private IMapperConfig MapperConfig;

        [SetUp]
        public void SetUp()
        {
            MapperConfig = new T();
        }

        [Test]
        public void WhenHaveNamespaces_ShouldReturnPopulatedMap()
        {
            MapperConfig.AddNamespace("prefix", "namespace");
            bool result = MapperConfig.ListNamespaces().Any();
            Assert.That(result, Is.True);
        }

        [Test]
        public void WhenHaveStatementsConfigs_ShouldReturnPopulatedList()
        {
            MapperConfig.AddStatementsConfig(Mock.Of<IStatementsConfig>());
            bool result = MapperConfig.ListStatementsConfigs().Any();
            Assert.That(result, Is.True);
        }

    }
}
