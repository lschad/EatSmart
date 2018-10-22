using System;
using System.Runtime.Serialization;

namespace SchadLucas.EatSmart.Data.Exceptions
{
    [Serializable]
    public class CantMoveException : Exception
    {
        public CantMoveException()
        {
        }

        public CantMoveException(string message) : base(message)
        {
        }

        public CantMoveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CantMoveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}