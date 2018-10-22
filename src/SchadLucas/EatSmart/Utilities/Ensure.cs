using System;

namespace SchadLucas.EatSmart.Utilities
{
    public static class Ensure
    {
        public static void NotNull(string paramName, params object[] args) => NotNull(paramName, string.Empty, args);

        public static void NotNull(string paramName, string message, params object[] args)
        {
            try
            {
                NotNull(args);
            }
            catch (ArgumentNullException)
            {
                Error.ArgumentNull(paramName, message);
            }
        }

        public static void NotNull(params object[] args)
        {
            if (args == null)
            {
                Error.ArgumentNull();
            }

            foreach (var arg in args)
            {
                if (arg == null)
                {
                    Error.ArgumentNull();
                }
            }
        }
    }
}