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
    [TestFixture(typeof(ObjectMap))]
    public class IObjectsConfigTest<T> where T : IObjectsConfig, new()
    {
        private IObjectsConfig ObjectsConfig;

        [SetUp]
        public void SetUp()
        {
            ObjectsConfig = new T();
        }

        [Test]
        public void WhenHaveSourceName_ShouldReturnPopulatedString()
        {
            string objectName = "object_name";
            ObjectsConfig.SetSourceName(objectName);
            string result = ObjectsConfig.GetSourceName();
            Assert.That(result, Is.EqualTo(objectName));
        }
    }
}
