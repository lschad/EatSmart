using System;

namespace SchadLucas.EatSmart.Utilities
{
    public static class Error
    {
        public static void Argument() => throw new ArgumentException();

        public static void ArgumentNull() => throw new ArgumentNullException();

        public static void ArgumentNull(string paramName, string message) => throw new ArgumentNullException(paramName, message);
    }
}