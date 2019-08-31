using GraphLoom.Mapper.Exceptions;
using NUnit.Framework;
using System;

namespace GraphLoom.UnitTest.Exceptions
{
    [TestFixture(typeof(GraphLoomException))]
    [TestFixture(typeof(ParserException))]
    [TestFixture(typeof(MapperException))]
    public class BaseExceptionTest<T> where T : SystemException, new()
    {
        [Test]
        public void WhenThrowException_ShouldCatchExpected()
        {
            Assert.Throws<T>(() => throw new T());
        }
        
        [Test]
        public void WhenThrowExceptionMessage_ShouldCatchExpected()
        {
            string expected = "message_1";

            Assert.Throws(Is.TypeOf<T>()
                .And.Message.EqualTo(expected),
                () => throw (T)Activator.CreateInstance(typeof(T), new object[] { expected }));
        }

        [Test]
        public void WhenThrowExceptionInner_ShouldCatchExpected()
        {
            Exception expectedInner = new ArgumentNullException();

            Assert.Throws(Is.TypeOf<T>()
                .And.InnerException.EqualTo(expectedInner),
                () => throw (T)Activator.CreateInstance(typeof(T), new object[] { null, expectedInner })); 
        }

        [Test]
        public void WhenThrowExceptionMessageInner_ShouldCatchExpected()
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
