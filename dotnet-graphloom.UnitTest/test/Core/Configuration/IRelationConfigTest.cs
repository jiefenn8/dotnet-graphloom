using GraphLoom.Mapper.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    [TestFixture]
    public class IRelationConfigTest<T> where T : IRelationConfig, new()
    {
        private IRelationConfig RelationConfig;

        [SetUp]
        public void SetUp()
        {
            RelationConfig = new T();
        }

        [Test]
        public void WhenHaveRelationName_ShouldReturnPopulatedString()
        {
            string relationName = "relation_name";
            RelationConfig.SetRelationName(relationName);
            string result = RelationConfig.GetRelationName();
            Assert.That(result, Is.EqualTo(relationName));
        }
    }
}
