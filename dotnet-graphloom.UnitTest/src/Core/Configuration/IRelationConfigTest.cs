using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.RDF.R2RML;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLoom.UnitTest.Mapper.Configuration
{
    [TestFixture(typeof(PredicateMap))]
    public class IRelationConfigTest<T> where T : IRelationConfig, new()
    {
        private IRelationConfig _relationConfig;

        [SetUp]
        public void SetUp()
        {
            _relationConfig = new T();
        }

        [Test]
        public void WhenHaveRelationName_ShouldReturnPopulatedString()
        {
            string relationName = "relation_name";
            _relationConfig.SetRelationName(relationName);
            string result = _relationConfig.GetRelationName();
            Assert.That(result, Is.EqualTo(relationName));
        }
    }
}
