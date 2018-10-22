using System;

namespace SchadLucas.EatSmart.Data.Exceptions
{
    [Serializable]
    public sealed class CantMoveForwardException : CantMoveException
    {
        public CantMoveForwardException()
        {
        }

        public CantMoveForwardException(string message) : base(message)
        {
        }

        public CantMoveForwardException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}