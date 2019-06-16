using GraphLoom.Mapper.Configuration;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    [TestFixture]
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
            StatementsConfig.AddRelationConfig(Mock.Of<IRelationConfig>());
            bool result = StatementsConfig.ListRelationConfigs().Any();
            Assert.That(result, Is.True);
        }
    }
}
