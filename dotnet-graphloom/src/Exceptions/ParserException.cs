using System;
using System.Runtime.Serialization;

//
// Summary:
//     Superclass of exception arising from GraphLoom.Parser code.
namespace GraphLoom.Mappers.Exceptions
{
    public class ParserException : GraphLoomException
    {
        public ParserException()
        {
        }

        public ParserException(string message) : base(message)
        {
        }

        public ParserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
