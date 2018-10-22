using System;

namespace SchadLucas.EatSmart.Data.Exceptions
{
    [Serializable]
    public sealed class DbConfigInitializationException : Exception
    {
        public DbConfigInitializationException()
        {
        }

        public DbConfigInitializationException(string message) : base(message)
        {
        }

        public DbConfigInitializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}