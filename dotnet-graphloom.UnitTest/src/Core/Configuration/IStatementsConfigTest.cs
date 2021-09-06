using System.Linq;
using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF.Configuration;
using Moq;
using NUnit.Framework;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    [TestFixture(typeof(TriplesMap))]
    public class IStatementsConfigTest<T> where T : IStatementsConfig, new()
    {
        private IStatementsConfig _statementsConfig;

        [SetUp]
        public void SetUp()
        {
            _statementsConfig = new T();
        }

        [Test]
        public void WhenHaveStatementsConfigs_ShouldReturnPopulatedList()
        {
            _statementsConfig.AddRelationObjectConfig(Mock.Of<IRelationConfig>(), Mock.Of<IObjectsConfig>());
            bool result = _statementsConfig.GetRelationObjectConfigs().Any();
            Assert.That(result, Is.True);
        }
    }
}
