using System;
using System.Collections.Generic;
using GraphLoom.Mapper.RDF;
using NUnit.Framework;

namespace GraphLoom.UnitTest.Mapper.RDF
{
    [TestFixture]
    public class URIFactoryTest
    {
        [Test]
        public void WhenCreateUriFromTemplateSucceed_ShouldReturnExpectedURI()
        {
            IDictionary<string, string> fakeRow = new Dictionary<string, string>()
            {
                { "EMPNO", "7369" },
                { "ENAME", "SMITH" },
                { "JOB", "CLERK" },
                { "DEPTNO", "10" },
            };

            Uri expResult = new Uri("http://www.example.org/employee/7369");
            Uri result = URIFactory.FromTemplate("http://www.example.org/employee/{EMPNO}", fakeRow);
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
