using System;

namespace SchadLucas.EatSmart.Data.Exceptions
{
    [Serializable]
    public sealed class StartupException : Exception
    {
        public StartupException()
        {
        }

        public StartupException(string message) : base(message)
        {
        }

        public StartupException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}