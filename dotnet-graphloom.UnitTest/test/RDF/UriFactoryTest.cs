using GraphLoom.Mapper.RDF;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLoom.UnitTest.Mapper.RDF
{
    [TestFixture]
    public class URIFactoryTest
    {
        private IDictionary<string, string> row;

        [SetUp]
        public void SetUp()
        {
            row = new Dictionary<string, string>()
            {
                { "EMPNO", "7369" },
                { "ENAME", "SMITH" },
                { "JOB", "CLERK" },
                { "DEPTNO", "10" },
            };
        }

        [Test]
        public void WhenCreateUriFromTemplateSucceed_ShouldReturnExpectedURI()
        {
            Uri expResult = new Uri("http://www.example.org/employee/7369");
            Uri result = URIFactory.FromTemplate("http://www.example.org/employee/{EMPNO}", row);
            Assert.That(result, Is.EqualTo(expResult));
        }

        [Test]
        public void WhenCreateUriFromStringSucceed_ShouldReturnExpectedURI()
        {
            Uri expResult = new Uri("http://www.example.org/");
            Uri result = URIFactory.FromString("http://www.example.org/");
            Assert.That(result, Is.EqualTo(expResult));
        }
    
    }
}
