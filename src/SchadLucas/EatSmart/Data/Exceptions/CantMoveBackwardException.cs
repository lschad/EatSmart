using System;

namespace SchadLucas.EatSmart.Data.Exceptions
{
    [Serializable]
    public sealed class CantMoveBackwardException : CantMoveException
    {
        public CantMoveBackwardException()
        {
        }

        public CantMoveBackwardException(string message) : base(message)
        {
        }

        public CantMoveBackwardException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}