using GraphLoom.Mapper.Configuration;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
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
            StatementsConfig.AddRelationObjectConfigPair(Mock.Of<IRelationConfig>(), Mock.Of<IObjectsConfig>());
            bool result = StatementsConfig.GetRelationObjectConfigPairs().Any();
            Assert.That(result, Is.True);
        }
    }
}
