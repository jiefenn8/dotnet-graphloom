using System;
using System.Runtime.Serialization;

namespace GraphLoom.Mapper.Exceptions
{
    //
    // Summary:
    //     Superclass of exception arising from GraphLoom.Mapper code.
    public class MapperException : GraphLoomException
    {
        public MapperException()
        {
        }

        public MapperException(string message) : base(message)
        {
        }

        public MapperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MapperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
