using GraphLoom.Mappers.Exceptions;
using NUnit.Framework;
using System;

namespace GraphLoom.UnitTests.Exceptions
{
    [TestFixture(typeof(GraphLoomException))]
    [TestFixture(typeof(ParserException))]
    [TestFixture(typeof(MapperException))]
    public class BaseExceptionTest<T> where T : SystemException, new()
    {
        [Test]
        public void WhenThrowException_ThenCatch()
        {
            Assert.Throws<T>(() => throw new T());
        }
        
        [Test]
        public void WhenThrowException_ThenCatchMessage()
        {
            string expected = "message_1";

            Assert.Throws(Is.TypeOf<T>()
                .And.Message.EqualTo(expected),
                () => throw (T)Activator.CreateInstance(typeof(T), new object[] { expected }));
        }

        [Test]
        public void WhenThrowException_ThenCatchInner()
        {
            Exception expectedInner = new ArgumentNullException();

            Assert.Throws(Is.TypeOf<T>()
                .And.InnerException.EqualTo(expectedInner),
                () => throw (T)Activator.CreateInstance(typeof(T), new object[] { null, expectedInner })); 
        }

        [Test]
        public void WhenThrowException_ThenCatchInnerAndMessage()
        {
            string expectedMessage = "message_1";
            Exception expectedInner = new ArgumentNullException();

            Assert.Throws(Is.TypeOf<T>()
                .And.Message.EqualTo(expectedMessage)
                .And.InnerException.EqualTo(expectedInner),
                () => throw (T)Activator.CreateInstance(typeof(T), new object[] { expectedMessage, expectedInner }));
        }
    }
}
