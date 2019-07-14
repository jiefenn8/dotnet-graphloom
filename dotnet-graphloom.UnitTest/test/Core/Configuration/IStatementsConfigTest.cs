using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF.Configuration;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    [TestFixture(typeof(TriplesMap))]
    public class IStatementsConfigTest<T> where T : IStatementsConfig, new()
    {
        private IStatementsConfig StatementsConfig;

        [SetUp]
        public void SetUp()
        {
            StatementsConfig = new T();
        }

        [Test]
        public void WhenHaveStatementsConfigs_ShouldReturnPopulatedList()
        {
            StatementsConfig.AddRelationObjectConfig(Mock.Of<IRelationConfig>(), Mock.Of<IObjectsConfig>());
            bool result = StatementsConfig.GetRelationObjectConfigs().Any();
            Assert.That(result, Is.True);
        }
    }
}
