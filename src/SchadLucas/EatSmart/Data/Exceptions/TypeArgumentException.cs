using System;

namespace SchadLucas.EatSmart.Data.Exceptions
{
    [Serializable]
    public class TypeArgumentException : Exception
    {
        public TypeArgumentException()
        {
        }

        public TypeArgumentException(string message) : base(message)
        {
        }

        public TypeArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}