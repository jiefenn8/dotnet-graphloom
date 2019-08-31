using System;
using System.Runtime.Serialization;

namespace GraphLoom.Mapper.Exceptions
{
    //
    // Summary:
    //     Superclass of exception arising from GraphLoom code.
    public class GraphLoomException : SystemException
    {
        public GraphLoomException()
        {
        }

        public GraphLoomException(string message) : base(message)
        {
        }

        public GraphLoomException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GraphLoomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
