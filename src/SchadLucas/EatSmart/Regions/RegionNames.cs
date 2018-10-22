using System.Runtime.CompilerServices;

namespace SchadLucas.EatSmart.Regions
{
    public static class RegionNames
    {
        public static string ApplicationOverlay => Name();
        public static string Main => Name();
        public static string Navigation => Name();
        public static string PopupNotification => Name();

        private static string Name([CallerMemberName] string s = "") => $"Region.{s}";
    }
}