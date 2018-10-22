using System;

namespace SchadLucas.EatSmart.Data.Exceptions
{
    [Serializable]
    public sealed class NotConvertibleException : Exception
    {
        public NotConvertibleException()
        {
        }

        public NotConvertibleException(string message) : base(message)
        {
        }

        public NotConvertibleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}