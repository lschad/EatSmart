using System;
using System.Runtime.Serialization;

namespace SchadLucas.EatSmart.Data.Exceptions
{
    [Serializable]
    public class NavigationException : Exception
    {
        protected NavigationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NavigationException()
        {
        }

        public NavigationException(string message) : base(message)
        {
        }

        public NavigationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}